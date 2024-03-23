
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WePromoLink.Data;
using WePromoLink.DTO.CRM;
using WePromoLink.Enums.CRM;
using WePromoLink.Models.CRM;

namespace WePromoLink.Services.CRM;
public class CampaignRunnerService : ICampaignRunnerService
{
    private readonly CRMDataContext _db;
    private readonly ILogger<CampaignRunnerService> _logger;
    public CampaignRunnerService(CRMDataContext db, ILogger<CampaignRunnerService> logger)
    {
        _db = db;
        _logger = logger;
    }
    public async Task AddCampaignRunner(WePromoLink.DTO.CRM.CampaignRunner data)
    {
        await _db.CampaignRunners.AddAsync(new Models.CRM.CampaignRunner
        {
            ConfigurationJSONUrl = data.ConfigurationJSONUrl,
            Converted = 0,
            Name = data.Name,
            TotalLeads = 0,
            Description = data.Description,
            SenderEmail = data.SenderEmail
        });
        _db.SaveChanges();
    }

    public async Task DeleteCampaignRunner(string ExternalId)
    {
        var item = await _db.CampaignRunners.Where(e=>e.ExternalId == ExternalId).SingleOrDefaultAsync();
        if(item == null)
        {
            _logger.LogWarning("CampaignRunner not found for deleted");
            return;
        }
        _db.CampaignRunners.Remove(item);
        _db.SaveChanges();
    }

    public async Task<PaginationList<ReadCampaignRunner>> GetAll(int? page, int? cant, string? filter)
    {
        PaginationList<ReadCampaignRunner> list = new PaginationList<ReadCampaignRunner>();
        page = page ?? 1;
        page = page <= 0 ? 1 : page;
        cant = cant ?? 25;

        var query = _db.CampaignRunners.Select(e => e);

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(e => e.Name.ToLower().Contains(filter.ToLower()));
        }

        var counter = await query.CountAsync();

        list.Items = await query
        .OrderByDescending(e => e.TotalLeads)
        .Skip((page.Value! - 1) * cant!.Value)
        .Take(cant!.Value)
        .Select(e => new ReadCampaignRunner
        {
            ExternalId = e.ExternalId,
            Name = e.Name,
            ConfigurationJSONUrl = e.ConfigurationJSONUrl,
            SenderEmail = e.SenderEmail,
            Converted = e.Converted,
            TotalLead = e.TotalLeads,
            TotalUnSubscribe = e.TotalUnsubscribe
        })
        .ToListAsync();
        list.Pagination.Page = page.Value!;
        list.Pagination.TotalPages = (int)Math.Ceiling((double)counter / (double)cant!.Value);
        list.Pagination.Cant = list.Items.Count;
        return list;
    }

    public async Task<PaginationList<ReadCampaignRunnerState>> GetAllRunnerState(int? page, int? cant, string? filter)
    {
        PaginationList<ReadCampaignRunnerState> list = new PaginationList<ReadCampaignRunnerState>();
        page = page ?? 1;
        page = page <= 0 ? 1 : page;
        cant = cant ?? 25;

        var query = _db.CampaignRunnerStates.Include(e=>e.CampaignRunner).Include(e=>e.LeadModel).Select(e => e);

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(e => e.CampaignRunner.Name.ToLower().Contains(filter.ToLower()));
        }

        var counter = await query.CountAsync();

        list.Items = await query
        .OrderByDescending(e => e.LastTimeExecute)
        .Skip((page.Value! - 1) * cant!.Value)
        .Take(cant!.Value)
        .Select(e => new ReadCampaignRunnerState
        {
            ExternalId = e.ExternalId,
            Name = e.CampaignRunner.Name,
            NoExecuteBefore = e.NoExecuteBefore,
            LastTimeExecute = e.LastTimeExecute,
            LeadEmail = e.LeadModel!.Email,
            LeadExternalId = e.LeadModel.ExternalId,
            Status = e.Status,
            Step = e.Step,
            StepExecuted = e.StepExecuted
        })
        .ToListAsync();
        list.Pagination.Page = page.Value!;
        list.Pagination.TotalPages = (int)Math.Ceiling((double)counter / (double)cant!.Value);
        list.Pagination.Cant = list.Items.Count;
        return list;
    }

    public async Task<ReadCampaignRunner> GetDetails(string id)
    {
        return await _db.CampaignRunners.Where(e => e.ExternalId == id).Select(e => new ReadCampaignRunner
        {
            ExternalId = e.ExternalId,
            Name = e.Name,
            ConfigurationJSONUrl = e.ConfigurationJSONUrl,
            SenderEmail = e.SenderEmail,
            Converted = e.Converted,
            TotalLead = e.TotalLeads,
            TotalUnSubscribe = e.TotalUnsubscribe        

        }).SingleAsync();
    }

    public async Task<ReadCampaignRunnerState> GetDetailsRunnerState(string id)
    {
        return await _db.CampaignRunnerStates
        .Include(e=>e.LeadModel)
        .Include(e=>e.CampaignRunner)
        .Where(e => e.ExternalId == id).Select(e => new ReadCampaignRunnerState
        {
            ExternalId = e.ExternalId,
            Name = e.CampaignRunner.Name,
            LeadEmail = e.LeadModel!.Email,
            NoExecuteBefore = e.NoExecuteBefore,
            LastTimeExecute = e.LastTimeExecute,
            LeadExternalId = e.LeadModel.ExternalId,
            Status = e.Status,
            Step = e.Step,
            StepExecuted = e.StepExecuted
        }).SingleAsync();
    }

    public async Task Pause(string CampaignRunnerStateExternalId)
    {
        var item = await _db.CampaignRunnerStates
        .Where(e=>e.ExternalId == CampaignRunnerStateExternalId)
        .SingleOrDefaultAsync();

        if(item == null)
        {
            _logger.LogWarning("Runner does no exist");
            return;
        }

        item.Status = RunnerStatusEnum.Paused;
        _db.CampaignRunnerStates.Update(item);
        _db.SaveChanges();
    }

    public async Task Play(string CampaignRunnerStateExternalId)
    {
        var item = await _db.CampaignRunnerStates
        .Where(e=>e.ExternalId == CampaignRunnerStateExternalId)
        .SingleOrDefaultAsync();

        if(item == null)
        {
            _logger.LogWarning("Runner does no exist");
            return;
        }

        item.Status = RunnerStatusEnum.Running;
        _db.CampaignRunnerStates.Update(item);
        _db.SaveChanges();
    }

    public async Task Run(RunBundle data)
    {
        var cr = await _db.CampaignRunners.Where(e=>e.ExternalId == data.CampaignRunnerExternalId).SingleOrDefaultAsync();
        if(cr == null)
        {
            _logger.LogWarning("No CampaignRunner found");
            return;
        }

        using var _client = new HttpClient();
        var config = await _client.GetFromJsonAsync<CampaignRunnerConfig>(cr.ConfigurationJSONUrl);
        List<CampaignRunnerState> runners = new List<CampaignRunnerState>();
        int counter = 0;
        foreach (var leadExternalId in data.LeadExternalIds)
        {
            var leadId = await _db.Leads.Where(e=>e.ExternalId == leadExternalId).Select(e=>e.Id).SingleOrDefaultAsync();

            if(leadId == Guid.Empty) continue;

            if(_db.CampaignRunnerStates.Any(e=>e.CampaignRunnerId == cr.Id && e.LeadModelId == leadId)) continue;

            var runner = new CampaignRunnerState
            {
                CampaignRunnerId = cr.Id,
                LeadModelId = leadId,
                Step = config.Initial,
                StepExecuted = false,
                Status = RunnerStatusEnum.Running
            };
            runners.Add(runner);
            counter++;
        }
        _db.CampaignRunnerStates.AddRange(runners);
         if(counter>0)
         {
            cr.TotalLeads+=counter;
            _db.CampaignRunners.Update(cr);
         }
        _db.SaveChanges();
    }

    public async Task Stop(string CampaignRunnerStateExternalId)
    {
        var item = await _db.CampaignRunnerStates
        .Where(e=>e.ExternalId == CampaignRunnerStateExternalId)
        .SingleOrDefaultAsync();

        if(item == null)
        {
            _logger.LogWarning("Runner does no exist");
            return;
        }

        item.Status = RunnerStatusEnum.Stopped;
        _db.CampaignRunnerStates.Update(item);
        _db.SaveChanges();
    }
}