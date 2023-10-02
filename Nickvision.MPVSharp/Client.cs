using Nickvision.MPVSharp.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Nickvision.MPVSharp;

/// <summary>
/// MPV Client class with helpers and convenience functions
/// </summary>
public class Client : MPVClient, IDisposable
{
    private bool _disposed;

    public event EventHandler<LogMessageReceivedEventArgs>? LogMessageReceived;
    public event EventHandler<GetPropertyReplyReceivedEventArgs>? GetPropertyReplyReceived;
    public event EventHandler<SetPropertyReplyReceivedEventArgs>? SetPropertyReplyReceived;
    public event EventHandler<CommandReplyReceivedEventArgs>? CommandReplyReceived;
    public event EventHandler<FileStartedEventArgs>? FileStarted;
    public event EventHandler<FileEndedEventArgs>? FileEnded;
    public event Action? FileLoaded;
    public event EventHandler<ClientMessageReceivedEventArgs>? ClientMessageReceived;
    public event Action? VideoReconfigured;
    public event Action? AudioReconfigured;
    public event Action? SeekStarted;
    public event Action? PlaybackRestarted;
    public event EventHandler<PropertyChangedEventArgs>? PropertyChanged;
    public event Action? QueueOverflowed;
    public event EventHandler<HookTriggeredEventArgs>? HookTriggered;
    public event Action? Destroyed;

    /// <summary>
    /// Constructs the Client
    /// </summary>
    public Client()
    {
        _disposed = false;
        Task.Run(HandleEvents);
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

    /// <summary>
    /// Initializes an uninitialized mpv instance.
    /// If the mpv instance is already running, an error is returned.
    /// </summary>
    /// <remarks>
    /// Some options are required to set before Initialize:
    /// config, config-dir, input-conf, load-scripts, script,
    /// player-operation-mode, input-app-events (OSX), all encoding mode options
    /// </remarks>
    /// <exception cref="ClientException">Thrown if initialization was not successful</exception>
    public new void Initialize()
    {
        var success = base.Initialize();
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// A placeholder to disallow setting wake up callback,
    /// because we're already running event loop.
    /// </summary>
    /// <param name="callback">Callback function</param>
    /// <param name="data">Pointer to arbitrary data to pass to callback</param>
    /// <exception cref="InvalidOperationException">Thrown if method is called as operation unsupported</exception>
    public new void SetWakeUpCallback(WakeUpCallback callback, nint data) => throw new InvalidOperationException("[MPVSharp] Setting wake up callback is not allowed when using MPVSharp.Client class, because event loop is already running.");

    /// <summary>
    /// Executes command array
    /// </summary>
    /// <param name="command">A command to execute as array of strings</param>
    /// <exception cref="ClientException">Thrown if command was not successful</exception>
    public new void Command(string[] command)
    {
        var success = base.Command(command);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }
    
    /// <summary>
    /// Executes command string
    /// </summary>
    /// <param name="command">A command string</param>
    /// <exception cref="ClientException">Thrown if command was not successful</exception>
    public new void CommandString(string command)
    {
        var success = base.CommandString(command);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Executes command list
    /// </summary>
    /// <param name="command">A command to execute as list of strings</param>
    /// <remarks>Alias for Command(string[])</remarks>
    public void Command(List<string> command) => Command(command.ToArray());

    /// <summary>
    /// Executes command string
    /// </summary>
    /// <param name="command">A command string</param>
    /// <remarks>Alias for CommandString(string)</remarks>
    public void Command(string command) => CommandString(command);

    /// <summary>
    /// MPV events loop
    /// </summary>
    private void HandleEvents()
    {
        while (!_disposed)
        {
            var clientEvent = WaitEvent(0);
            switch (clientEvent.Id)
            {
                case MPVEventId.Shutdown:
                    Dispose();
                    Destroyed?.Invoke();
                    break;
                case MPVEventId.LogMessage:
                    var msg = clientEvent.EventLogMessage;
                    LogMessageReceived?.Invoke(this, new LogMessageReceivedEventArgs(msg!.Value.Prefix, msg.Value.Text, msg.Value.LogLevel));
                    break;
                case MPVEventId.GetPropertyReply:
                    var getProp = clientEvent.EventProperty;
                    GetPropertyReplyReceived?.Invoke(this, new GetPropertyReplyReceivedEventArgs(clientEvent.ReplyUserdata, getProp?.Name ?? "", new Node((MPVNode?)getProp?.GetData())));
                    break;
                case MPVEventId.SetPropertyReply:
                    SetPropertyReplyReceived?.Invoke(this, new SetPropertyReplyReceivedEventArgs(clientEvent.ReplyUserdata, clientEvent.Error));
                    break;
                case MPVEventId.CommandReply:
                    var getResult = clientEvent.CommandResult;
                    CommandReplyReceived?.Invoke(this, new CommandReplyReceivedEventArgs(clientEvent.ReplyUserdata, clientEvent.Error, new Node(getResult!.Value.Result)));
                    break;
                case MPVEventId.StartFile:
                    var startData = clientEvent.StartFile;
                    FileStarted?.Invoke(this, new FileStartedEventArgs(startData!.Value.PlaylistEntryId));
                    break;
                case MPVEventId.EndFile:
                    var endData = clientEvent.EndFile;
                    FileEnded?.Invoke(this, new FileEndedEventArgs(endData!.Value.Reason, endData.Value.Error, endData.Value.PlaylistEntryId, endData.Value.PlaylistInsertId, endData.Value.PlaylistInsertNumEntries));
                    break;
                case MPVEventId.FileLoaded:
                    FileLoaded?.Invoke();
                    break;
                case MPVEventId.ClientMessage:
                    var clientMsg = clientEvent.ClientMessage;
                    ClientMessageReceived?.Invoke(this, new ClientMessageReceivedEventArgs(clientMsg!));
                    break;
                case MPVEventId.VideoReconfig:
                    VideoReconfigured?.Invoke();
                    break;
                case MPVEventId.AudioReconfig:
                    AudioReconfigured?.Invoke();
                    break;
                case MPVEventId.Seek:
                    SeekStarted?.Invoke();
                    break;
                case MPVEventId.PlaybackRestart:
                    PlaybackRestarted?.Invoke();
                    break;
                case MPVEventId.PropertyChange:
                    var changedProp = clientEvent.EventProperty;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(changedProp!.Value.Name, new Node((MPVNode?)changedProp.Value.GetData())));
                    break;
                case MPVEventId.QueueOverflow:
                    QueueOverflowed?.Invoke();
                    break;
                case MPVEventId.Hook:
                    var hook = clientEvent.Hook;
                    HookTriggered?.Invoke(this, new HookTriggeredEventArgs(hook!.Value.Name, hook.Value.Id));
                    break;
            }
        }
    }

    /// <summary>
    /// Adds property to watch in event loop
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="replyUserdata">Optional reply Id</param>
    /// <exception cref="ClientException">Thrown if can't observe property</exception>
    public void ObserveProperty(string name, ulong replyUserdata = 0)
    {
        var success = ObserveProperty(name, MPVFormat.Node, replyUserdata);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Sets property using <see cref="MPVFormat.String"/> format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    /// <exception cref="ClientException">Thrown if can't set property</exception>
    public new void SetProperty(string name, string data)
    {
        var success = base.SetProperty(name, data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Sets property using <see cref="MPVFormat.Flag"/> format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Bool data</param>
    /// <exception cref="ClientException">Thrown if can't set property</exception>
    public void SetProperty(string name, bool data)
    {
        var success = base.SetProperty(name, data ? 1 : 0);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Sets property using <see cref="MPVFormat.Int64"/> format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Long int data</param>
    /// <exception cref="ClientException">Thrown if can't set property</exception>
    public new void SetProperty(string name, long data)
    {
        var success = base.SetProperty(name, data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Sets property using <see cref="MPVFormat.Double"/> format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Double data</param>
    /// <exception cref="ClientException">Thrown if can't set property</exception>
    public new void SetProperty(string name, double data)
    {
        var success = base.SetProperty(name, data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Sets property using <see cref="MPVFormat.Node"/> format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="node"><see cref="Nickvision.MPVSharp.Node"/> with data</param>
    /// <exception cref="ClientException">Thrown if can't set property</exception>
    public void SetProperty(string name, Node node)
    {
        var success = base.SetProperty(name, node.Data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Sets property using <see cref="MPVFormat.Flag"/> format asynchroniously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="data">Bool data</param>
    /// <exception cref="ClientException">Thrown if can't set property</exception>
    public void SetPropertyAsync(ulong replyUserdata, string name, bool data)
    {
        var success = base.SetPropertyAsync(replyUserdata, name, data ? 1 : 0);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Sets property using <see cref="MPVFormat.Int64"/> format asynchroniously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="data">Long int data</param>
    /// <exception cref="ClientException">Thrown if can't set property</exception>
    public new void SetPropertyAsync(ulong replyUserdata, string name, long data)
    {
        var success = base.SetPropertyAsync(replyUserdata, name, data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Sets property using <see cref="MPVFormat.Double"/> format asynchroniously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="data">Double data</param>
    /// <exception cref="ClientException">Thrown if can't set property</exception>
    public new void SetPropertyAsync(ulong replyUserdata, string name, double data)
    {
        var success = base.SetPropertyAsync(replyUserdata, name, data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Sets property using <see cref="MPVFormat.String"/> format asynchroniously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    /// <exception cref="ClientException">Thrown if can't set property</exception>
    public new void SetPropertyAsync(ulong replyUserdata, string name, string data)
    {
        var success = base.SetPropertyAsync(replyUserdata, name, data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Sets property using <see cref="MPVFormat.Node"/> format asynchroniously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="node"><see cref="Nickvision.MPVSharp.Node"/> with data</param>
    /// <exception cref="ClientException">Thrown if can't set property</exception>
    public void SetPropertyAsync(ulong replyUserdata, string name, Node node)
    {
        var success = base.SetPropertyAsync(replyUserdata, name, node.Data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Gets property using <see cref="MPVFormat.String"/> format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    /// <exception cref="ClientException">Thrown if can't get property</exception>
    public new void GetProperty(string name, out string data)
    {
        var success = base.GetProperty(name, out data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Gets property using <see cref="MPVFormat.Flag"/> format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    /// <exception cref="ClientException">Thrown if can't get property</exception>
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
    /// Gets property using <see cref="MPVFormat.Int64"/> format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Long int data</param>
    /// <exception cref="ClientException">Thrown if can't get property</exception>
    public new void GetProperty(string name, out long data)
    {
        var success = base.GetProperty(name, out data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Gets property using <see cref="MPVFormat.Double"/> format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Double data</param>
    /// <exception cref="ClientException">Thrown if can't get property</exception>
    public new void GetProperty(string name, out double data)
    {
        var success = base.GetProperty(name, out data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Get property using <see cref="MPVFormat.Node"/> format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="node"><see cref="Nickvision.MPVSharp.Node"/> with data</param>
    /// <exception cref="ClientException">Thrown if can't get property</exception>
    public void GetProperty(string name, out Node node)
    {
        var success = base.GetProperty(name, out MPVNode data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
        node = new Node(data);
    }

    /// <summary>
    /// Gets property using <see cref="MPVFormat.Node"/> format asynchroniously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <exception cref="ClientException">Thrown if can't get property</exception>
    public void GetPropertyAsync(ulong replyUserdata, string name)
    {
        var success = base.GetPropertyAsync(replyUserdata, name, MPVFormat.Node);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }
    
    /// <summary>
    /// Deletes property
    /// </summary>
    /// <param name="name">Property name</param>
    /// <exception cref="ClientException">Thrown if can't delete property</exception>
    public new void DelProperty(string name)
    {
        var success = base.DelProperty(name);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Sets option using <see cref="MPVFormat.Node"/> format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="node"><see cref="Nickvision.MPVSharp.Node"/> with data</param>
    /// <remarks>
    /// You can't normally set options during runtime.
    /// </remarks>
    /// <exception cref="ClientException">Thrown if can't set option</exception>
    public void SetOption(string name, Node node)
    {
        var success = base.SetOption(name, node.Data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Sets option using <see cref="MPVFormat.String"/> format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    /// <remarks>
    /// You can't normally set options during runtime.
    /// </remarks>
    /// <exception cref="ClientException">Thrown if can't set option</exception>
    public void SetOption(string name, string data)
    {
        var success = SetOptionString(name, data);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Requests log messages with specified minimum log level
    /// </summary>
    /// <param name="logLevel">Log level as string</param>
    /// <exception cref="ClientException">Thrown if failed to request messages</exception>
    public new void RequestLogMessages(string logLevel)
    {
        var success = base.RequestLogMessages(logLevel);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }

    /// <summary>
    /// Requests log messages with specified minimum log level
    /// </summary>
    /// <param name="logLevel">Log level as <see cref="MPVLogLevel"/></param>
    public void RequestLogMessages(MPVLogLevel logLevel)
    {
        RequestLogMessages(logLevel switch
        {
            MPVLogLevel.Fatal => "fatal",
            MPVLogLevel.Error => "error",
            MPVLogLevel.Warn => "warn",
            MPVLogLevel.Info => "info",
            MPVLogLevel.V => "v",
            MPVLogLevel.Debug => "debug",
            MPVLogLevel.Trace => "trace",
            _ => "no"
        });
    }

    /// <summary>
    /// Creates OpenGL <see cref="RenderContext"/>
    /// </summary>
    /// <returns>New render context</returns>
    public RenderContext CreateRenderContext() => new RenderContext(Handle);

    /// <summary>
    /// Toggles paused state
    /// </summary>
    public void CyclePause() => Command("cycle pause");

    /// <summary>
    /// Sends seek command
    /// </summary>
    /// <param name="target">Time in seconds</param>
    /// <param name="flags">Seek <see cref="SeekFlags">flags</see></param>
    public void Seek(double target, SeekFlags flags = SeekFlags.Relative | SeekFlags.Keyframes)
    {
        if ((flags.HasFlag(SeekFlags.Relative) && flags.HasFlag(SeekFlags.Absolute)) || (flags.HasFlag(SeekFlags.Keyframes) && flags.HasFlag(SeekFlags.Exact)))
        {
            throw new ClientException(MPVError.InvalidParameter);
        }
        try
        {
            Command(new []{"seek", target.ToString(CultureInfo.InvariantCulture), flags.FlagsToString()});
        }
        catch (ClientException) { } // Seek fails if nothing is playing, we don't want a crash
    }

    /// <summary>
    /// Loads a file from path or URL
    /// </summary>
    /// <param name="url">File path or URL</param>
    /// <param name="flags">Load <see cref="LoadFlags">flags</see></param>
    public void LoadFile(string url, LoadFlags flags = LoadFlags.Replace) => Command(new []{"loadfile", url, flags.FlagsToString()});

    /// <summary>
    /// Loads a playlist from path or URL
    /// </summary>
    /// <param name="url">Playlist path or URL</param>
    /// <param name="flags">Load <see cref="LoadFlags">flags</see></param>
    public void LoadList(string url, LoadFlags flags = LoadFlags.Replace) => Command(new []{"loadlist", url, flags.FlagsToString()});

    /// <summary>
    /// Plays next file in playlist
    /// </summary>
    /// <param name="force">Whether to terminate playback if there are no more files</param>
    public void PlaylistNext(bool force = false) => Command(new []{"playlist-next", force ? "force" : "weak"});

    /// <summary>
    /// Plays previous file in playlist
    /// </summary>
    /// <param name="force">Whether to terminate playback if the first file is being playing</param>
    public void PlaylistPrev(bool force = false) => Command(new []{"playlist-prev", force ? "force" : "weak"});

    /// <summary>
    /// Starts (or restarts) playback of the given playlist index
    /// </summary>
    /// <param name="index">0-based index</param>
    public void PlaylistPlayIndex(uint index) => Command(new []{"playlist-play-index", index.ToString()});
}