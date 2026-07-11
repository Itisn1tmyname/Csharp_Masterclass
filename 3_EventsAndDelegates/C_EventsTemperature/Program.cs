namespace C_EventsTemperature;

public delegate void TemperatureChangeHandler(string message);

// Using Generic Delegate EventHandler<TEventArgs>
public class TemperatureChangedEventArgs : EventArgs
{
    public int Temperature { get; }
    
    public TemperatureChangedEventArgs(int temperature)
    {
        Temperature = temperature;
    }
}

public class TemperatureMonitor
{
    // Generic Delegate EventHandler<TEventArgs>
    public event EventHandler<TemperatureChangedEventArgs> OnTemperatureChanged;
    //public event TemperatureChangeHandler OnTemperatureChanged;
    
    private int _temperature;
    public int Temperature
    {
        get => _temperature;
        set
        {
            if (_temperature != value)
            {
                OnTemperatureChangedEvent(new TemperatureChangedEventArgs(value));
            }
            _temperature = value;
        }
    }

    protected virtual void OnTemperatureChangedEvent(TemperatureChangedEventArgs e)
    {
        // Letting every subscriber know!
        OnTemperatureChanged?.Invoke(this, e);
    }
}

public class TemperatureAlert
{
    public void OnTemperatureChanged(object? sender, TemperatureChangedEventArgs e)
    {
        Console.WriteLine($"{DateTime.Now}: Temperature is {e.Temperature}, sender is {sender}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        TemperatureMonitor monitor = new TemperatureMonitor();
        TemperatureAlert alert = new TemperatureAlert();
        monitor.OnTemperatureChanged += alert.OnTemperatureChanged;
        
        monitor.Temperature = 20;
        
        do
        {
            Console.Write("Enter Temperature: ");
            int temp;
            if (!int.TryParse(Console.ReadLine(), out temp)) break;
            monitor.Temperature = temp;
        } while (true);
    }
}