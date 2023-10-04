namespace WePromoLink.Models;

public class SurveyAnswerModel
{
    public Guid Id { get; set; }
    public string Value { get; set; }
    public Guid SurveyQuestionModelId { get; set; }
    public SurveyQuestionModel SurveyQuestionModel { get; set; }
    public List<SurveyDatapointModel> Datapoints { get; set; }

}