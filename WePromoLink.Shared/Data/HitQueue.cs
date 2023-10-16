namespace WePromoLink.Data;

public class HitQueue
{
    private readonly Queue<HitAffiliate> _queue = new Queue<HitAffiliate>();

    public HitAffiliate? Item
    {
        get
        {
            if (_queue.Count == 0) return null;
            return _queue.Dequeue();
        }
        set
        {
            if(value == null) return;
            _queue.Enqueue(value);
        }
    }

}
