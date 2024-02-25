namespace WePromoLink.StatsWorker.Services;
public interface IProcessEvent<T>
{
    Task<bool> Process(T item);
}