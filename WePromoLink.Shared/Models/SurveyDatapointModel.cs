namespace WePromoLink.Models;

public class SurveyDatapointModel
{
    public Guid Id { get; set; }
    public Guid SurveyAnswerModelId { get; set; }
    public SurveyAnswerModel SurveyAnswerModel { get; set; }
    public Guid SurveyQuestionModelId { get; set; }
    public SurveyQuestionModel SurveyQuestionModel { get; set; }
    public DateTime CreatedAt { get; set; }

    public SurveyDatapointModel()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}