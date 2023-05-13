namespace WePromoLink.Interfaces;

public interface IHasValue<T>
{
    public T Value { get; set; }
}