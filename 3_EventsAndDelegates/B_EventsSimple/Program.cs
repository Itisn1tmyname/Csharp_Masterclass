namespace B_Events;

// This is the signature of the methods that should HANDLE the Event.
public delegate void Notify(string msg);

public class EventPublisher
{
    //The "On" prefix signifies that the method is not just a regular
    //method but one that is called when a specific event occurs.
    public event Notify OnNotify;

    public void RaiseEvent(string message)
    {
        OnNotify?.Invoke(message);
    }
}

public class EventSubscriber
{
    public void OnEventRaised(string message)
    {
        Console.WriteLine($"Event received: {message}");
    }
}

static class Program
{
    static void Main()
    {
        EventPublisher publisher = new EventPublisher();
        EventSubscriber subscriber = new EventSubscriber();
        
        publisher.OnNotify += subscriber.OnEventRaised;
        publisher.RaiseEvent("test");
    }
}