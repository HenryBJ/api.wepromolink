using Stripe;

namespace WePromoLink.Data;

public class WebHookEventQueue
{
    private readonly Queue<Event> _queue = new Queue<Event>();

    public Event? Item
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
