namespace MauiTestApp;

/// <summary>
/// Custom Label that tracks handler connection/disconnection
/// </summary>
public class HandlerTrackingLabel : Label
{
    private static int _instanceCounter = 0;
    private readonly int _instanceId;
    private static readonly List<WeakReference<HandlerTrackingLabel>> _allInstances = new();

    public HandlerTrackingLabel()
    {
        _instanceId = ++_instanceCounter;
        _allInstances.Add(new WeakReference<HandlerTrackingLabel>(this));
        Console.WriteLine($"[HandlerTracking] Label #{_instanceId} created");
        
        DeviceDisplay.MainDisplayInfoChanged += DeviceDisplayOnMainDisplayInfoChanged;
    }

    public int InstanceId => _instanceId;

    private void DeviceDisplayOnMainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        Console.WriteLine("I'm still alive baby, instance id: " + _instanceId);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if (Handler is null)
        {
            DeviceDisplay.MainDisplayInfoChanged -= DeviceDisplayOnMainDisplayInfoChanged;
            Console.WriteLine($"[HandlerTracking] Label #{_instanceId} - Handler DISCONNECTED (Text: {Text})");
        }
        else
        {
            Console.WriteLine($"[HandlerTracking] Label #{_instanceId} - Handler CONNECTED (Text: {Text})");
        }
    }

    ~HandlerTrackingLabel()
    {
        Console.WriteLine($"[HandlerTracking] Label #{_instanceId} - FINALIZED/DISPOSED");
    }

    public static List<HandlerTrackingLabel> GetLabelsWithConnectedHandlers()
    {
        var result = new List<HandlerTrackingLabel>();
        
        // Clean up dead weak references and find labels with handlers
        _allInstances.RemoveAll(wr =>
        {
            if (wr.TryGetTarget(out var label))
            {
                if (label.Handler != null)
                {
                    result.Add(label);
                }
                return false; // Keep alive references
            }
            return true; // Remove dead references
        });

        return result;
    }

    public static void CleanupAllConnectedHandlers()
    {
        var labelsWithHandlers = GetLabelsWithConnectedHandlers();
        
        foreach (var label in labelsWithHandlers)
        {
            label.DisconnectHandlers();
            Console.WriteLine($"[HandlerTracking] Label #{label.InstanceId} - Handler FORCE DISCONNECTED");
        }
    }
}
