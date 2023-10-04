namespace WePromoLink.DTO.Marketing;

public class SurveyData
{
    public string Question { get; set; }
    public Guid Id { get; set; }
    public List<SurveyItem> Answers { get; set; }
}