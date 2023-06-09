namespace WePromoLink.Data;

public class HitQueue
{
    private readonly Queue<Hit> _queue = new Queue<Hit>();

    public Hit? Item
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
