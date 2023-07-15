using Nickvision.MPVSharp.Internal;

namespace Nickvision.MPVSharp;

public class Client : MPVClient, IDisposable
{
    private bool _disposed;

    /// <summary>
    /// Timeout for mpv_wait_event in seconds (0 by default)
    /// </summary>
    public double EventTimeout { get; set; }
    
    public event EventHandler<PropertyChangedEventArgs>? PropertyChanged;
    public event EventHandler<LogMessageReceivedEventArgs>? LogMessageReceived;
    public event Action? Destroyed;
    
    /// <summary>
    /// Creates MPV Client
    /// </summary>
    public Client()
    {
        _disposed = false;
        EventTimeout = 0;
        Task.Run(HandleEvents);
    }

    /// <summary>
    /// Init MPV client, some options can only be set before init
    /// </summary>
    public new void Initialize()
    {
        var success = base.Initialize();
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Execute command array
    /// </summary>
    /// <param name="command">A command to execute as array of strings</param>
    public new void Command(string[] command)
    {
        var success = base.Command(command);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }
    
    /// <summary>
    /// Execute command list
    /// </summary>
    /// <param name="command">A command to execute as list of strings</param>
    public void Command(List<string> command) => Command(command.ToArray());

    /// <summary>
    /// Execute command string
    /// </summary>
    /// <param name="command">A command string</param>
    public void Command(string command)
    {
        var success = CommandString(command);
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
                var clientEvent = WaitEvent(EventTimeout);
                switch (clientEvent.Id)
                {
                    case MPVEventId.Shutdown:
                        Dispose();
                        Destroyed?.Invoke();
                        break;
                    case MPVEventId.LogMessage:
                        var msg = clientEvent.GetEventLogMessage();
                        LogMessageReceived?.Invoke(this, new LogMessageReceivedEventArgs(msg!.Value.Prefix, msg.Value.Text, msg.Value.LogLevel));
                        break;
                    case MPVEventId.PropertyChange:
                        var prop = clientEvent.GetEventProperty();
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop!.Value.Name, (MPVNode?)prop.Value.GetData()));
                        break;
                }
            }
            catch (ClientException e)
            {
                Console.WriteLine(e);
            }
        }
    }

    /// <summary>
    /// Add property to watch in event loop
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="replyUserdata">Optional reply Id</param>
    public void ObserveProperty(string name, ulong replyUserdata = 0)
    {
        var success = ObserveProperty(name, MPVFormat.Node, replyUserdata);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Set property using String format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    public new void SetProperty(string name, string data)
    {
        var success = base.SetProperty(name, data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Set property using Flag format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Bool data</param>
    public void SetProperty(string name, bool data)
    {
        var success = base.SetProperty(name, data ? 1 : 0);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Set property using Int64 format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Long int data</param>
    public new void SetProperty(string name, long data)
    {
        var success = base.SetProperty(name, data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Set property using Double format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Double data</param>
    public new void SetProperty(string name, double data)
    {
        var success = base.SetProperty(name, data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Set property using Node format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">MPVNode with data</param>
    public new void SetProperty(string name, MPVNode data)
    {
        var success = base.SetProperty(name, data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Get property using String format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    public new void GetProperty(string name, out string data)
    {
        var success = base.GetProperty(name, out data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Get property using Flag format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    public void GetProperty(string name, out bool data)
    {
        var success = base.GetProperty(name, out int flag);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
        data = flag == 1;
    }

    /// <summary>
    /// Get property using Long format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Long int data</param>
    public new void GetProperty(string name, out long data)
    {
        var success = base.GetProperty(name, out data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Get property using Double format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Double data</param>
    public new void GetProperty(string name, out double data)
    {
        var success = base.GetProperty(name, out data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Get property using Node format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">MPVNode with data</param>
    public new void GetProperty(string name, out MPVNode data)
    {
        var success = base.GetProperty(name, out data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Set option using Node format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">MPVNode with data</param>
    public new void SetOption(string name, MPVNode data)
    {
        var success = base.SetOption(name, data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Set option using String format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    public void SetOption(string name, string data)
    {
        var success = base.SetOptionString(name, data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Request log messages with specified minimum log level
    /// </summary>
    /// <param name="logLevel">Log level as string</param>
    public new void RequestLogMessages(string logLevel)
    {
        var success = base.RequestLogMessages(logLevel);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Request log messages with specified minimum log level
    /// </summary>
    /// <param name="logLevel">Log level as MPVLogLevel</param>
    public void RequestLogMessages(MPVLogLevel logLevel)
    {
        var level = logLevel switch
        {
            MPVLogLevel.Fatal => "fatal",
            MPVLogLevel.Error => "error",
            MPVLogLevel.Warn => "warn",
            MPVLogLevel.Info => "info",
            MPVLogLevel.V => "v",
            MPVLogLevel.Debug => "debug",
            MPVLogLevel.Trace => "trace",
            _ => "no"
        };
        RequestLogMessages(level);
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
            Destroy();
        }
        _disposed = true;
    }
}