using Nickvision.MPVSharp.Internal;

namespace Nickvision.MPVSharp;

public partial class Client : IDisposable
{
    private readonly MPVClient _mpv;
    private bool _disposed;

    /// <summary>
    /// Timeout for mpv_wait_event in seconds (0 by default)
    /// </summary>
    public double EventTimeout { get; set; }
    
    public event EventHandler<PropertyChangedEventArgs>? PropertyChanged;
    public event Action? Destroyed;
    
    /// <summary>
    /// Creates MPV Client
    /// </summary>
    public Client()
    {
        _mpv = new MPVClient();
        _disposed = false;
        EventTimeout = 0;
        Task.Run(HandleEvents);
    }

    /// <summary>
    /// Init MPV client, should be called after setting options
    /// </summary>
    public void Initialize()
    {
        var success = _mpv.Initialize();
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Executes command array
    /// </summary>
    /// <param name="command">A command to execute as array of strings</param>
    public void Command(string[] command)
    {
        var success = _mpv.Command(command);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }
    
    /// <summary>
    /// Executes command list
    /// </summary>
    /// <param name="command">A command to execute as list of strings</param>
    public void Command(List<string> command) => Command(command.ToArray());

    /// <summary>
    /// Executes command string
    /// </summary>
    /// <param name="command">A command string</param>
    public void Command(string command)
    {
        var success = _mpv.CommandString(command);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }
    /// <summary>
    /// MPV events loop
    /// </summary>
    private void HandleEvents()
    {
        while (!_disposed)
        {
            try
            {
                var clientEvent = new ClientEvent(_mpv.WaitEvent(EventTimeout));
                switch (clientEvent.Id)
                {
                    case MPVEventId.Shutdown:
                        Dispose();
                        Destroyed?.Invoke();
                        break;
                    case MPVEventId.PropertyChange:
                        var prop = clientEvent.GetEventProperty();
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop.Name, prop.Node));
                        break;
                }
            }
            catch (ClientException e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public void ObserveProperty(string name, ulong replyUserdata = 0)
    {
        var success = _mpv.ObserveProperty(name, MPVFormat.Node, replyUserdata);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    public int UnobserveProperty(ulong replyUserdata = 0) => _mpv.UnobserveProperty(replyUserdata);

    public void SetProperty(string name, string data)
    {
        var success = _mpv.SetProperty(name, MPVFormat.String, data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    public void SetProperty(string name, bool data)
    {
        var success = _mpv.SetProperty(name, MPVFormat.Flag, data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    public void SetProperty(string name, long data)
    {
        var success = _mpv.SetProperty(name, MPVFormat.Int64, data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }
    

    public void SetProperty(string name, double data)
    {
        var success = _mpv.SetProperty(name, MPVFormat.Double, data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Finalizes the Client
    /// </summary>
    ~Client() => Dispose(false);

    /// <summary>
    /// Frees resources used by the Client object
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Frees resources used by the Client object
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }
        if (disposing)
        {
            _mpv.Destroy();
        }
        _disposed = true;
    }
}