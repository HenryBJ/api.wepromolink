namespace WePromoLink;

public class PaginationList<T>
{
    public List<T> Items { get; set; }

    public Pagination Pagination { get; set; }
    public PaginationList()
    {
        Pagination = new Pagination();
    }
}
