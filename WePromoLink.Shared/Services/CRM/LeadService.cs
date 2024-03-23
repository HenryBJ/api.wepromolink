
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WePromoLink.Data;
using WePromoLink.DTO.CRM;
using WePromoLink.Enums.CRM;
using WePromoLink.Models.CRM;

namespace WePromoLink.Services.CRM;

public class LeadService : ILeadService
{
    private readonly CRMDataContext _db;
    private readonly ILogger<LeadService> _logger;

    public LeadService(CRMDataContext db, ILogger<LeadService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task AddLead(AddLead data)
    {
        await _db.Leads.AddAsync(new LeadModel
        {
            Name = data.Name,
            CampaginOrigin = data.CampaginOrigin,
            Country = data.Country,
            Email = data.Email,
            EmailVerified = false,
            Industry = data.Industry,
            Languaje = data.Languaje,
            Sector = data.Sector,
            Website = data.Website,
            Status = string.IsNullOrEmpty(data.Email)? LeadStatusEnum.Prospect:LeadStatusEnum.NewLead
        });
        _db.SaveChanges();
        _logger.LogInformation($"Received lead:{data.Email}");
    }

    public async Task DeleteLead(string externalId)
    {
        using var trans = _db.Database.BeginTransaction();
        try
        {
            var lead = await _db.Leads.Where(e => e.ExternalId == externalId).SingleAsync();
            var runnerStates = await _db.CampaignRunnerStates.Where(e => e.LeadModelId == lead.Id).ToListAsync();
            _db.CampaignRunnerStates.RemoveRange(runnerStates);
            _db.Leads.Remove(lead);
            _db.SaveChanges();
            trans.Commit();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            trans.Rollback();
        }
    }

    public async Task EditLead(EditLead data)
    {
        var item = await _db.Leads.Where(e => e.ExternalId == data.ExternalId).SingleOrDefaultAsync();
        if (item == null)
        {
            _logger.LogError("Lead not found");
            return;
        }

        item.CampaginOrigin = data.CampaginOrigin;
        item.Country = data.Country;
        item.Email = data.Email;
        item.Industry = data.Industry;
        item.Languaje = data.Languaje;
        item.Name = data.Name;
        item.Note = data.Note;
        item.Sector = data.Sector;
        item.Website = data.Website;
        _db.Leads.Update(item);
        _db.SaveChanges();
        _logger.LogInformation("Lead edited");
    }

    public async Task<PaginationList<ReadLead>> GetAll(int? page, int? cant, string? filter)
    {
        PaginationList<ReadLead> list = new PaginationList<ReadLead>();
        page = page ?? 1;
        page = page <= 0 ? 1 : page;
        cant = cant ?? 25;

        var query = _db.Leads.Select(e => e);

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(e => e.Email.ToLower().Contains(filter.ToLower()));
        }

        var counter = await query.CountAsync();

        list.Items = await query
        .OrderByDescending(e => e.CreatedAt)
        .Skip((page.Value! - 1) * cant!.Value)
        .Take(cant!.Value)
        .Select(e => new ReadLead
        {
            Status = e.Status,
            CampaginOrigin = e.CampaginOrigin,
            Country = e.Country,
            Email = e.Email,
            ExternalId = e.ExternalId,
            Industry = e.Industry,
            Languaje = e.Languaje,
            Name = e.Name,
            Note = e.Note,
            Sector = e.Sector,
            Website = e.Website
        })
        .ToListAsync();
        list.Pagination.Page = page.Value!;
        list.Pagination.TotalPages = (int)Math.Ceiling((double)counter / (double)cant!.Value);
        list.Pagination.Cant = list.Items.Count;
        return list;
    }

    public async Task<ReadLead> GetDetails(string id)
    {
        return await _db.Leads.Where(e => e.ExternalId == id).Select(e => new ReadLead
        {
            CampaginOrigin = e.CampaginOrigin,
            Country = e.Country,
            Email = e.Email,
            ExternalId = e.ExternalId,
            Industry = e.Industry,
            Languaje = e.Languaje,
            Name = e.Name,
            Note = e.Note,
            Sector = e.Sector,
            Status = e.Status,
            Website = e.Website
        }).SingleAsync();
    }

    public async Task NextEvent(NextEvent data)
    {
        var item = await _db.CampaignRunnerStates
        .Include(e => e.CampaignRunner)
        .Include(e => e.LeadModel)
        .Where(e => e.ExternalId == data.RunnerId).SingleAsync();

        if (!item.LeadModel!.EmailVerified)
        {
            item.LeadModel.EmailVerified = true;
            item.LeadModel.Status = LeadStatusEnum.Contacted;
            _db.Leads.Update(item.LeadModel);
        }

        if (data.Event == 0)
        {
            item.Status = RunnerStatusEnum.Stopped;
            item.CampaignRunner.Converted += 1;
            item.LeadModel.Status = LeadStatusEnum.Converted;
            _db.Leads.Update(item.LeadModel);
            _db.CampaignRunners.Update(item.CampaignRunner);
        }
        else
        if (data.Event == -1)
        {
            item.Status = RunnerStatusEnum.Stopped;
            item.CampaignRunner.TotalUnsubscribe += 1;
            item.LeadModel.Status = LeadStatusEnum.Unsubscribed;
            _db.Leads.Update(item.LeadModel);
            _db.CampaignRunners.Update(item.CampaignRunner);
        }
        else
        {
            using var _client = new HttpClient();
            var config = await _client.GetFromJsonAsync<CampaignRunnerConfig>(item.CampaignRunner.ConfigurationJSONUrl);
            var transitions = config.Steps.Where(e => e.Step == item.Step).Single().Transitions;
            var trans = transitions.Where(e => e.Event == data.Event).Single();

            item.Step = trans.Next_step;
            item.StepExecuted = false;
            item.NoExecuteBefore = DateTime.UtcNow.AddDays(trans.Delay.Day).AddMinutes(trans.Delay.Min);
            
        }

        _db.CampaignRunnerStates.Update(item);
        _db.SaveChanges();
        _logger.LogInformation("Next Event");
    }
}