namespace WePromoLink.Models;

public class SurveyQuestionModel
{
    public Guid Id { get; set; }
    public string Value { get; set; }
    public int Group { get; set; }
    public List<SurveyAnswerModel> Answers { get; set; }
    public List<SurveyDatapointModel> Datapoints { get; set; }
}