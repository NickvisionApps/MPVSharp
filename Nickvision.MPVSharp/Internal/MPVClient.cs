using System;
using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// MPV Client
/// </summary>
/// <remarks>
/// This object implements functions from libmpv's client.h that accept mpvHandle
/// </remarks>
public partial class MPVClient : ICloneable
{
    public delegate void WakeUpCallback(nint data);

    [LibraryImport("libc.so.6", StringMarshalling = StringMarshalling.Utf8)]
    private static partial nint setlocale(int category, string value);
    // Create client
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial nint mpv_create_client(nint handle, string? name);
    // Init client
    [LibraryImport("libmpv.so.2")]
    private static partial MPVError mpv_initialize(nint handle);
    // Execute command
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_command(nint handle, string?[] command);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_command_string(nint handle, string command);
    [LibraryImport("libmpv.so.2")]
    private static partial MPVError mpv_command_node(nint handle, ref MPVNode command, out MPVNode result);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_command_ret(nint handle, string?[] command, out MPVNode node);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_command_async(nint handle, ulong replyUserdata, string?[] command);
    [LibraryImport("libmpv.so.2")]
    private static partial MPVError mpv_command_node_async(nint handle, ulong replyUserdata, ref MPVNode command);
    [LibraryImport("libmpv.so.2")]
    private static partial void mpv_abort_async_command(nint handle, ulong replyUserdata);
    // Events
    [LibraryImport("libmpv.so.2")]
    private static partial nint mpv_wait_event(nint handle, double timeout);
    [LibraryImport("libmpv.so.2")]
    private static partial void mpv_wakeup(nint handle);
    [LibraryImport("libmpv.so.2")]
    private static partial void mpv_set_wakeup_callback(nint handle, WakeUpCallback callback, nint data);
    [LibraryImport("libmpv.so.2")]
    private static partial MPVError mpv_request_event(nint handle, MPVEventId eid, int enable);
    // Observe property
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_observe_property(nint handle, ulong replyUserdata, string name, MPVFormat format);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial int mpv_unobserve_property(nint handle, ulong replyUserdata);
    // Set property
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property(nint handle, string name, MPVFormat format, ref int data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property(nint handle, string name, MPVFormat format, ref long data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property(nint handle, string name, MPVFormat format, ref double data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property(nint handle, string name, MPVFormat format, ref string data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property(nint handle, string name, MPVFormat format, ref MPVNode data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property_async(nint handle, ulong replyUserdata, string name, MPVFormat format, ref int data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property_async(nint handle, ulong replyUserdata, string name, MPVFormat format, ref long data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property_async(nint handle, ulong replyUserdata, string name, MPVFormat format, ref double data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property_async(nint handle, ulong replyUserdata, string name, MPVFormat format, ref string data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property_async(nint handle, ulong replyUserdata, string name, MPVFormat format, ref MPVNode data);
    // Get property
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out int data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out long data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out double data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out string data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out MPVNode data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property_async(nint handle, ulong replyUserdata, string name, MPVFormat format);
    // Set option
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_option(nint handle, string name, MPVFormat format, ref MPVNode data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_option_string(nint handle, string name, string data);
    // Hook
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_hook_add(nint handle, ulong replyUserdata, string name, int priority);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_hook_continue(nint handle, ulong id);
    // Other
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial string mpv_client_name(nint handle);
    [LibraryImport("libmpv.so.2")]
    private static partial long mpv_client_id(nint handle);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_load_config_file(nint handle, string path);
    [LibraryImport("libmpv.so.2")]
    private static partial long mpv_get_time_us(nint handle);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_request_log_messages(nint handle, string logLevel);
    [LibraryImport("libmpv.so.2")]
    private static partial void mpv_wait_async_requests(nint handle);
    // Destroy client
    [LibraryImport("libmpv.so.2")]
    private static partial void mpv_destroy(nint handle);
    [LibraryImport("libmpv.so.2")]
    private static partial void mpv_terminate_destroy(nint handle);

    private const int LC_NUMERIC = 1;

    protected readonly nint _handle;

    /// <summary>
    /// The name of this client handle.
    /// Every client has its own unique name.
    /// </summary>
    public string Name => mpv_client_name(_handle);
    /// <summary>
    /// The ID of this client handle.
    /// Every client has its own unique ID.
    /// </summary>
    public long Id => mpv_client_id(_handle);

    /// <summary>
    /// Construct MPVClient
    /// </summary>
    /// <param name="handle">Optional handle to create client for the same player core</param>
    /// <param name="name">Optional unique client name</param>
    /// <exception cref="Exception">Thrown if failed to create mpvHandle</exception>
    public MPVClient(nint? handle = null, string? name = null)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            setlocale(LC_NUMERIC, "C");
        }
        _handle = mpv_create_client(handle ?? IntPtr.Zero, name);
        if (_handle == IntPtr.Zero)
        {
            throw new Exception("Failed to create MPV client");
        }
    }
    
    /// <summary>
    /// Clone object, creating new MPVClient for the same player core
    /// </summary>
    /// <returns>MPVClient as object</returns>
    public object Clone() => new MPVClient(_handle);

    /// <summary>
    /// Load a config file
    /// </summary>
    /// <param name="path">Absolute path to config</param>
    /// <returns>Error code</returns>
    public MPVError LoadConfigFile(string path) => mpv_load_config_file(_handle, path);

    /// <summary>
    /// Get internal time
    /// </summary>
    /// <returns>Time in microseconds</returns>
    public long GetTimeUs() => mpv_get_time_us(_handle);

    /// <summary>
    /// Initialize an uninitialized mpv instance.
    /// If the mpv instance is already running, an error is returned.
    /// </summary>
    /// <returns>Error code</returns>
    /// <remarks>
    /// Some options are required to set before Initialize:
    /// config, config-dir, input-conf, load-scripts, script,
    /// player-operation-mode, input-app-events (OSX), all encoding mode options
    /// </remarks>
    public MPVError Initialize() => mpv_initialize(_handle);

    /// <summary>
    /// Send a command to the player.
    /// </summary>
    /// <param name="command">A command to execute as array of strings</param>
    /// <returns>Error code</returns>
    public MPVError Command(string[] command)
    {
        var nullTermArray = new string?[command.Length + 1];
        Array.Copy(command, nullTermArray, command.Length);
        nullTermArray[^1] = null;
        return mpv_command(_handle, nullTermArray);
    }

    /// <summary>
    /// Same as CommandRet(), but allows passing structured data in any format.
    /// </summary>
    /// <param name="command">MPVNode with NodeList containing positional or named arguments.</param>
    /// <param name="result">MPVNode containing result on command success. Should be freed with MPVNode.FreeNodeContents().</param>
    /// <returns>Error code</returns>
    public MPVError CommandNode(MPVNode command, out MPVNode result) => mpv_command_node(_handle, ref command, out result);

    /// <summary>
    /// Send a command to the player and return the result.
    /// </summary>
    /// <param name="command">A command to execute as array of strings</param>
    /// <param name="node">MPVNode containing result on command success.</param>
    /// <returns>Error code</returns>
    public MPVError CommandRet(string[] command, out MPVNode node)
    {
        var nullTermArray = new string?[command.Length + 1];
        Array.Copy(command, nullTermArray, command.Length);
        nullTermArray[^1] = null;
        return mpv_command_ret(_handle, nullTermArray, out node);
    }

    /// <summary>
    /// Send a command to the player.
    /// </summary>
    /// <param name="command">A command string</param>
    /// <returns>Error code</returns>
    public MPVError CommandString(string command) => mpv_command_string(_handle, command);
    
    /// <summary>
    /// Same as Command(), but run the command asynchronously.
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="command">A command to execute as array of strings</param>
    /// <returns>Error code</returns>
    public MPVError CommandAsync(ulong replyUserdata, string?[] command)
    {
        var nullTermArray = new string?[command.Length + 1];
        Array.Copy(command, nullTermArray, command.Length);
        nullTermArray[^1] = null;
        return mpv_command_async(_handle, replyUserdata, nullTermArray);
    }

    /// <summary>
    /// Same as CommandNode(), but run the command asynchronously.
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="command">MPVNode with NodeList containing positional or named arguments</param>
    /// <returns>Error code</returns>
    public MPVError CommandNodeAsync(ulong replyUserdata, MPVNode command) => mpv_command_node_async(_handle, replyUserdata, ref command);

    /// <summary>
    /// Signal to all async requests with the matching Id to abort.
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    public void AbortAsyncCommand(ulong replyUserdata) => mpv_abort_async_command(_handle, replyUserdata);

    /// <summary>
    /// Wait for an event or until timeout
    /// </summary>
    /// <param name="eventTimeout">Timeout in seconds</param>
    /// <returns>Error code</returns>
    public MPVEvent WaitEvent(double eventTimeout) => Marshal.PtrToStructure<MPVEvent>(mpv_wait_event(_handle, eventTimeout));

    /// <summary>
    /// Interrupt the current WaitEvent() call
    /// </summary>
    public void WakeUp() => mpv_wakeup(_handle);

    /// <summary>
    /// Set a custom function that should be called when there are new events
    /// </summary>
    /// <param name="callback">Callback function</param>
    /// <param name="data">Pointer to arbitrary data to pass to callback</param>
    public void SetWakeUpCallback(WakeUpCallback callback, nint data) => mpv_set_wakeup_callback(_handle, callback, data);

    /// <summary>
    /// Add property to watch using events
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="format">Property MPVFormat</param>
    /// <param name="replyUserdata">Optional reply Id</param>
    /// <returns>Error code</returns>
    public MPVError ObserveProperty(string name, MPVFormat format, ulong replyUserdata) => mpv_observe_property(_handle, replyUserdata, name, format);
    
    /// <summary>
    /// Undo all ObserveProperty() for given reply Id
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <returns>Number of properties to unobserve or error code</returns>
    public int UnobserveProperty(ulong replyUserdata) => mpv_unobserve_property(_handle, replyUserdata);

    /// <summary>
    /// Set property using String format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    /// <returns>Error code</returns>
    public MPVError SetProperty(string name, string data) => mpv_set_property(_handle, name, MPVFormat.String, ref data);

    /// <summary>
    /// Set property using Flag format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Flag data (0 or 1)</param>
    /// <returns>Error code</returns>
    public MPVError SetProperty(string name, int data) => mpv_set_property(_handle, name, MPVFormat.Flag, ref data);

    /// <summary>
    /// Set property using Int64 format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Long int data</param>
    /// <returns>Error code</returns>
    public MPVError SetProperty(string name, long data) => mpv_set_property(_handle, name, MPVFormat.Int64, ref data);

    /// <summary>
    /// Set property using Double format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Double data</param>
    /// <returns>Error code</returns>
    public MPVError SetProperty(string name, double data) => mpv_set_property(_handle, name, MPVFormat.Double, ref data);

    /// <summary>
    /// Set property using Node format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">MPVNode with data</param>
    /// <returns>Error code</returns>
    public MPVError SetProperty(string name, MPVNode data) => mpv_set_property(_handle, name, MPVFormat.Node, ref data);

    /// <summary>
    /// Set property using String format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    /// <returns>Error code</returns>
    public MPVError SetPropertyString(string name, string data) => SetProperty(name, data);

    /// <summary>
    /// Set property using Flag format asynchronously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="data">Flag data (0 or 1)</param>
    /// <returns>Error code</returns>
    public MPVError SetPropertyAsync(ulong replyUserdata, string name, int data) => mpv_set_property_async(_handle, replyUserdata, name, MPVFormat.Flag, ref data);

    /// <summary>
    /// Set property using Int64 format asynchronously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="data">Long Int data</param>
    /// <returns>Error code</returns>
    public MPVError SetPropertyAsync(ulong replyUserdata, string name, long data) => mpv_set_property_async(_handle, replyUserdata, name, MPVFormat.Int64, ref data);

    /// <summary>
    /// Set property using Double format asynchronously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="data">Double data</param>
    /// <returns>Error code</returns>
    public MPVError SetPropertyAsync(ulong replyUserdata, string name, double data) => mpv_set_property_async(_handle, replyUserdata, name, MPVFormat.Double, ref data);

    /// <summary>
    /// Set property using String format asynchronously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    /// <returns>Error code</returns>
    public MPVError SetPropertyAsync(ulong replyUserdata, string name, string data) => mpv_set_property_async(_handle, replyUserdata, name, MPVFormat.String, ref data);

    /// <summary>
    /// Set property using MPVNode format asynchronously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="data">MPVNode containing data</param>
    /// <returns>Error code</returns>
    public MPVError SetPropertyAsync(ulong replyUserdata, string name, MPVNode data) => mpv_set_property_async(_handle, replyUserdata, name, MPVFormat.Node, ref data);

    /// <summary>
    /// Get property using Flag format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>Error code</returns>
    public MPVError GetProperty(string name, out int data) => mpv_get_property(_handle, name, MPVFormat.Flag, out data);

    /// <summary>
    /// Get property using Int64 format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>Error code</returns>
    public MPVError GetProperty(string name, out long data) => mpv_get_property(_handle, name, MPVFormat.Int64, out data);

    /// <summary>
    /// Get property using Double format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>Error code</returns>
    public MPVError GetProperty(string name, out double data) => mpv_get_property(_handle, name, MPVFormat.Double, out data);

    /// <summary>
    /// Get property using String format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>Error code</returns>
    public MPVError GetProperty(string name, out string data) => mpv_get_property(_handle, name, MPVFormat.String, out data);

    /// <summary>
    /// Get property using Node format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>Error code</returns>
    public MPVError GetProperty(string name, out MPVNode data) => mpv_get_property(_handle, name, MPVFormat.Node, out data);

    /// <summary>
    /// Get property using String format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>Error code</returns>
    public MPVError GetPropertyString(string name, out string data) => mpv_get_property(_handle, name, MPVFormat.String, out data);

    /// <summary>
    /// Get property using OSDString format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>Error code</returns>
    public MPVError GetPropertyOSDString(string name, out string data) => mpv_get_property(_handle, name, MPVFormat.OSDString, out data);

    /// <summary>
    /// Get property asynchroniously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="format">MPVFormat for returning data</param>
    /// <returns>Error code</returns>
    public MPVError GetPropertyAsync(ulong replyUserdata, string name, MPVFormat format) => mpv_get_property_async(_handle, replyUserdata, name, format);

    /// <summary>
    /// Set option using MPVNode format
    /// </summary>
    /// <param name="name">Option name</param>
    /// <param name="data">MPVNode with data</param>
    /// <returns>Error code</returns>
    /// <remarks>
    /// You can't normally set options during runtime.
    /// </remarks>
    public MPVError SetOption(string name, MPVNode data) => mpv_set_option(_handle, name, MPVFormat.Node, ref data);

    /// <summary>
    /// Set option using String format
    /// </summary>
    /// <param name="name">Option name</param>
    /// <param name="data">String data</param>
    /// <returns>Error code</returns>
    /// <remarks>
    /// You can't normally set options during runtime.
    /// </remarks>
    public MPVError SetOptionString(string name, string data) => mpv_set_option_string(_handle, name, data);

    /// <summary>
    /// Request log messages with specified minimum log level
    /// </summary>
    /// <param name="logLevel">Log level as string</param>
    /// <returns>Error code</returns>
    public MPVError RequestLogMessages(string logLevel) => mpv_request_log_messages(_handle, logLevel);

    /// <summary>
    /// Enable or disable the given event
    /// </summary>
    /// <remarks>
    /// All event are enabled by default
    /// </remarks>
    /// <param name="eid">Event Id</param>
    /// <param name="enabled">Whether the event should be enabled (1) or not (0)</param>
    /// <returns>Error code</returns>
    public MPVError RequestEvent(MPVEventId eid, int enabled) => mpv_request_event(_handle, eid, enabled);

    /// <summary>
    /// Block until all asynchronous requests are done
    /// </summary>
    public void WaitAsyncRequests() => mpv_wait_async_requests(_handle);

    /// <summary>
    /// Register a hook handler.
    /// A hook is like a synchronous event that blocks the player.
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Hook name. This should be one of the documented names (see mpv manual).</param>
    /// <param name="priority">Hook priority</param>
    /// <returns>Error code</returns>
    public MPVError HookAdd(ulong replyUserdata, string name, int priority) => mpv_hook_add(_handle, replyUserdata, name, priority);

    /// <summary>
    /// Respond to a MPVEventHook event. You must call this after you have handled the event.
    /// </summary>
    /// <param name="id">This must be the value of the MPVEventHook.Id field</param>
    /// <returns>Error code</returns>
    public MPVError HookContinue(ulong id) => mpv_hook_continue(_handle, id);

    /// <summary>
    /// Disconnect and destroy the client
    /// </summary>
    public void Destroy() => mpv_destroy(_handle);

    /// <summary>
    /// Bring down the player and all its clients
    /// </summary>
    public void TerminateDestroy() => mpv_terminate_destroy(_handle);
}