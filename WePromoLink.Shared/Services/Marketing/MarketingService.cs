using Microsoft.EntityFrameworkCore;
using WePromoLink.Data;
using WePromoLink.DTO.Marketing;
using WePromoLink.Models;
using WePromoLink.Services.Email;

namespace WePromoLink.Services.Marketing;

public class MarketingService : IMarketingService
{
    private readonly DataContext _db;
    private readonly IEmailSender _emailService;

    public MarketingService(DataContext db, IEmailSender emailService)
    {
        _db = db;
        _emailService = emailService;
    }

    public async Task AddSurveyEntry(Guid question, Guid response)
    {
        _db.SurveyDatapoints.Add(new SurveyDatapointModel { SurveyQuestionModelId = question, SurveyAnswerModelId = response });
        await _db.SaveChangesAsync();
    }


    public async Task<SurveySummary> GetSurveySummary()
    {
        SurveySummary result = new();
        result.Data = new List<SurveyData>();
        var questions = await _db.SurveyQuestions.Include(e => e.Datapoints).Include(e => e.Answers).ToListAsync();

        foreach (var question in questions)
        {
            var item = new SurveyData
            {
                Id = question.Id,
                Question = question.Value,
                Answers = question.Answers.Select(e =>
                {
                    if (e.Datapoints != null)
                    {
                        var dps = e.Datapoints.Where(i => i.SurveyQuestionModelId == question.Id && i.SurveyAnswerModelId == e.Id);
                        var total_answers = question.Answers.Count();
                        var value = dps.Count() > 0 ? dps.Count() : 0;
                        var percent = dps.Count() > 0 ? (dps.Count() * 100) / total_answers : 0;
                        return new SurveyItem
                        {
                            Response = e.Value,
                            Percent = percent,
                            Value = value
                        };
                    }
                    return new SurveyItem
                    {
                        Id = e.Id,
                        Response = e.Value,
                        Percent = 0,
                        Value = 0
                    };

                }).ToList()
            };

            result.Data.Add(item);
        }
        return result;
    }

    public async Task<string[]> GetWaitingList()
    {
        return await _db.JoinWaitingLists.Select(e => e.Email).ToArrayAsync();
    }

    public async Task JoinWaitingList(string email)
    {
        await _db.JoinWaitingLists.AddAsync(new JoinWaitingListModel { Email = email });
        _db.SaveChanges();
        await _emailService.Send("Dear friend", email, "Welcome to WePromoLink - Thank You for Joining Us!", Templates.JoinWaitingList(new { }));
    }
}