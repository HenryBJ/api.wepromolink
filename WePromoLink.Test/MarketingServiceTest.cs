using Microsoft.Extensions.DependencyInjection;
using WePromoLink.Services.Email;
using WePromoLink.Services.Marketing;

namespace WePromoLink.Test;

public class MarketingServiceTest : BaseTest
{

    private readonly IMarketingService? _service;

    public MarketingServiceTest()
    {
        _service = _serviceProvider?.GetRequiredService<IMarketingService>();
    }

    [Fact]
    public async Task JoinWaitingList_ShouldReturnTrue()
    {
        if (_db == null) throw new Exception("Data context null");
        if (_service == null) throw new Exception("MarketingService null");

        string email = "jose.devops@gmail.com";
        await _service.JoinWaitingList(email);
        var result = _db.JoinWaitingLists.Where(e => e.Email == email).FirstOrDefault();
        Assert.NotNull(result);

        if (result != null) _db.JoinWaitingLists.Remove(result);
        _db.SaveChanges();
    }

    [Fact]
    public async Task GetJoinWaitingList_ShouldReturnTrue()
    {
        if (_db == null) throw new Exception("Data context null");
        if (_service == null) throw new Exception("MarketingService null");

        string email = "jose.devops@gmail.com";
        await _service.JoinWaitingList(email);
        var result = await _service.GetWaitingList();
        Assert.NotNull(result);
        Assert.True(result.Length > 0);

        if (result != null) _db.JoinWaitingLists.Remove(_db.JoinWaitingLists.Where(e => e.Email == email).First());
        _db.SaveChanges();
    }

    [Fact]
    public async Task AddSurveyEntry_ShouldReturnTrue()
    {
        if (_db == null) throw new Exception("Data context null");
        if (_service == null) throw new Exception("MarketingService null");

        Guid question = Guid.Parse("C5586C12-E2D2-4831-BDB9-259C0EF83298");
        Guid answer = Guid.Parse("FCDAB328-8201-43C8-A52D-17805C49372B");

        await _service.AddSurveyEntry(question, answer);
        var result = _db.SurveyDatapoints.Where(e => e.SurveyQuestionModelId == question && e.SurveyAnswerModelId == answer).FirstOrDefault();
        Assert.NotNull(result);

        if (result != null) _db.SurveyDatapoints.Remove(result);
        _db.SaveChanges();
    }

    [Fact]
    public async Task GetSurveySummary_ShouldReturnTrue()
    {
        if (_db == null) throw new Exception("Data context null");
        if (_service == null) throw new Exception("MarketingService null");

        await _service.AddSurveyEntry(Guid.Parse("C5586C12-E2D2-4831-BDB9-259C0EF83298"), Guid.Parse("FCDAB328-8201-43C8-A52D-17805C49372B"));
        await _service.AddSurveyEntry(Guid.Parse("C5586C12-E2D2-4831-BDB9-259C0EF83298"), Guid.Parse("E865A2BC-D576-45BC-B23A-56DE71073748"));
        await _service.AddSurveyEntry(Guid.Parse("C5586C12-E2D2-4831-BDB9-259C0EF83298"), Guid.Parse("D5364342-05E2-43E7-B816-588047BD9899"));
        await _service.AddSurveyEntry(Guid.Parse("C5586C12-E2D2-4831-BDB9-259C0EF83298"), Guid.Parse("8567D9AC-52BA-4CF4-BA34-D5983DE804B4"));

        try
        {
            var summary = await _service.GetSurveySummary();

            Assert.NotNull(summary);
            Assert.NotNull(summary.Data);
            Assert.True(summary.Data.Count > 0);
            Assert.True(summary.Data[0].Question == "What motivates you to use a platform like WePromoLink?");
            Assert.NotNull(summary.Data[0].Answers);
            Assert.True(summary.Data[0].Answers.Count > 0);

            Assert.True(summary.Data[0].Answers[0].Value == 1);
            Assert.True(summary.Data[0].Answers[0].Percent == 25);
            Assert.False(String.IsNullOrEmpty(summary.Data[0].Answers[0].Response));

            Assert.True(summary.Data[0].Answers[1].Value == 1);
            Assert.True(summary.Data[0].Answers[1].Percent == 25);
            Assert.False(String.IsNullOrEmpty(summary.Data[0].Answers[1].Response));

            Assert.True(summary.Data[0].Answers[2].Value == 1);
            Assert.True(summary.Data[0].Answers[2].Percent == 25);
            Assert.False(String.IsNullOrEmpty(summary.Data[0].Answers[2].Response));

            Assert.True(summary.Data[0].Answers[3].Value == 1);
            Assert.True(summary.Data[0].Answers[3].Percent == 25);
            Assert.False(String.IsNullOrEmpty(summary.Data[0].Answers[3].Response));
        }
        finally
        {
            _db.SurveyDatapoints.RemoveRange(_db.SurveyDatapoints.ToArray());
            _db.SaveChanges();
        }
    }


}