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
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial nint mpv_create_client(nint handle, string? name);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial nint mpv_create_weak_client(nint handle, string? name);
    // Init client
    [LibraryImport("mpv")]
    private static partial MPVError mpv_initialize(nint handle);
    // Execute command
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_command(nint handle, string?[] command);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_command_string(nint handle, string command);
    [LibraryImport("mpv")]
    private static partial MPVError mpv_command_node(nint handle, ref MPVNode command, out MPVNode result);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_command_ret(nint handle, string?[] command, out MPVNode node);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_command_async(nint handle, ulong replyUserdata, string?[] command);
    [LibraryImport("mpv")]
    private static partial MPVError mpv_command_node_async(nint handle, ulong replyUserdata, ref MPVNode command);
    [LibraryImport("mpv")]
    private static partial void mpv_abort_async_command(nint handle, ulong replyUserdata);
    // Events
    [LibraryImport("mpv")]
    private static partial nint mpv_wait_event(nint handle, double timeout);
    [LibraryImport("mpv")]
    private static partial void mpv_wakeup(nint handle);
    [LibraryImport("mpv")]
    private static partial void mpv_set_wakeup_callback(nint handle, WakeUpCallback callback, nint data);
    [LibraryImport("mpv")]
    private static partial MPVError mpv_request_event(nint handle, MPVEventId eid, int enable);
    // Observe property
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_observe_property(nint handle, ulong replyUserdata, string name, MPVFormat format);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial int mpv_unobserve_property(nint handle, ulong replyUserdata);
    // Set property
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property(nint handle, string name, MPVFormat format, ref int data);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property(nint handle, string name, MPVFormat format, ref long data);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property(nint handle, string name, MPVFormat format, ref double data);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property(nint handle, string name, MPVFormat format, ref string data);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property(nint handle, string name, MPVFormat format, ref MPVNode data);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property_async(nint handle, ulong replyUserdata, string name, MPVFormat format, ref int data);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property_async(nint handle, ulong replyUserdata, string name, MPVFormat format, ref long data);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property_async(nint handle, ulong replyUserdata, string name, MPVFormat format, ref double data);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property_async(nint handle, ulong replyUserdata, string name, MPVFormat format, ref string data);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_property_async(nint handle, ulong replyUserdata, string name, MPVFormat format, ref MPVNode data);
    // Get property
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out int data);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out long data);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out double data);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out string data);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out MPVNode data);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property_async(nint handle, ulong replyUserdata, string name, MPVFormat format);
    // Delete property
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_del_property(nint handle, string name);
    // Set option
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_option(nint handle, string name, MPVFormat format, ref MPVNode data);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_option_string(nint handle, string name, string data);
    // Hook
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_hook_add(nint handle, ulong replyUserdata, string name, int priority);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_hook_continue(nint handle, ulong id);
    // Other
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial string mpv_client_name(nint handle);
    [LibraryImport("mpv")]
    private static partial long mpv_client_id(nint handle);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_load_config_file(nint handle, string path);
    [LibraryImport("mpv")]
    private static partial long mpv_get_time_us(nint handle);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_request_log_messages(nint handle, string logLevel);
    [LibraryImport("mpv")]
    private static partial void mpv_wait_async_requests(nint handle);
    // Destroy client
    [LibraryImport("mpv")]
    private static partial void mpv_destroy(nint handle);
    [LibraryImport("mpv")]
    private static partial void mpv_terminate_destroy(nint handle);

    private const int LC_NUMERIC = 1;

    /// <summary>
    /// MPV client handle
    /// </summary>
    public nint Handle { get; init; }
    /// <summary>
    /// The name of this client handle.
    /// Every client has its own unique name.
    /// </summary>
    public string Name => mpv_client_name(Handle);
    /// <summary>
    /// The ID of this client handle.
    /// Every client has its own unique ID.
    /// </summary>
    public long Id => mpv_client_id(Handle);

    /// <summary>
    /// Constructs MPVClient
    /// </summary>
    /// <param name="handle">Optional handle to create client for the same player core</param>
    /// <param name="name">Optional unique client name</param>
    /// <param name="weak">Whether the new client should be weak or not.
    /// If true, the created handle is treated as a weak reference. If all handles
    /// referencing a core are weak references, the core is automatically destroyed.</param>
    /// <exception cref="Exception">Thrown if failed to create mpvHandle</exception>
    public MPVClient(nint? handle = null, string? name = null, bool weak = false)
    {
        Resolver.SetResolver();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            setlocale(LC_NUMERIC, "C");
        }
        Handle = weak ? mpv_create_weak_client(handle!.Value, name) : mpv_create_client(handle ?? IntPtr.Zero, name);
        if (Handle == IntPtr.Zero)
        {
            throw new Exception("Failed to create MPV client");
        }
    }
    
    /// <summary>
    /// Clones object, creating new MPVClient for the same player core
    /// </summary>
    /// <returns>MPVClient as object</returns>
    public object Clone() => new MPVClient(Handle);

    /// <summary>
    /// Loads a config file
    /// </summary>
    /// <param name="path">Absolute path to config</param>
    /// <returns>Error code</returns>
    public MPVError LoadConfigFile(string path) => mpv_load_config_file(Handle, path);

    /// <summary>
    /// Gets internal time
    /// </summary>
    /// <returns>Time in microseconds</returns>
    public long GetTimeUs() => mpv_get_time_us(Handle);

    /// <summary>
    /// Initializes an uninitialized mpv instance.
    /// If the mpv instance is already running, an error is returned.
    /// </summary>
    /// <returns>Error code</returns>
    /// <remarks>
    /// Some options are required to set before Initialize:
    /// config, config-dir, input-conf, load-scripts, script,
    /// player-operation-mode, input-app-events (OSX), all encoding mode options
    /// </remarks>
    public MPVError Initialize() => mpv_initialize(Handle);

    /// <summary>
    /// Sends a command to the player.
    /// </summary>
    /// <param name="command">A command to execute as array of strings</param>
    /// <returns>Error code</returns>
    public MPVError Command(string[] command)
    {
        var nullTermArray = new string?[command.Length + 1];
        Array.Copy(command, nullTermArray, command.Length);
        nullTermArray[^1] = null;
        return mpv_command(Handle, nullTermArray);
    }

    /// <summary>
    /// Same as <see cref="CommandRet"/>, but allows passing structured data in any format.
    /// </summary>
    /// <param name="command"><see cref="MPVNode"/> with <see cref="MPVNodeList"/> containing positional or named arguments.</param>
    /// <param name="result"><see cref="MPVNode"/> containing result on command success. Should be freed with MPVNode.FreeNodeContents().</param>
    /// <returns>Error code</returns>
    public MPVError CommandNode(MPVNode command, out MPVNode result) => mpv_command_node(Handle, ref command, out result);

    /// <summary>
    /// Sends a command to the player and returns the result.
    /// </summary>
    /// <param name="command">A command to execute as array of strings</param>
    /// <param name="node"><see cref="MPVNode"/> containing result on command success.</param>
    /// <returns>Error code</returns>
    public MPVError CommandRet(string[] command, out MPVNode node)
    {
        var nullTermArray = new string?[command.Length + 1];
        Array.Copy(command, nullTermArray, command.Length);
        nullTermArray[^1] = null;
        return mpv_command_ret(Handle, nullTermArray, out node);
    }

    /// <summary>
    /// Sends a command to the player.
    /// </summary>
    /// <param name="command">A command string</param>
    /// <returns>Error code</returns>
    public MPVError CommandString(string command) => mpv_command_string(Handle, command);
    
    /// <summary>
    /// Same as <see cref="Command"/>, but runs the command asynchronously.
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="command">A command to execute as array of strings</param>
    /// <returns>Error code</returns>
    public MPVError CommandAsync(ulong replyUserdata, string?[] command)
    {
        var nullTermArray = new string?[command.Length + 1];
        Array.Copy(command, nullTermArray, command.Length);
        nullTermArray[^1] = null;
        return mpv_command_async(Handle, replyUserdata, nullTermArray);
    }

    /// <summary>
    /// Same as <see cref="CommandNode"/>, but runs the command asynchronously.
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="command"><see cref="MPVNode"/> with <see cref="MPVNodeList"/> containing positional or named arguments</param>
    /// <returns>Error code</returns>
    public MPVError CommandNodeAsync(ulong replyUserdata, MPVNode command) => mpv_command_node_async(Handle, replyUserdata, ref command);

    /// <summary>
    /// Sends a signal to all async requests with the matching Id to abort.
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    public void AbortAsyncCommand(ulong replyUserdata) => mpv_abort_async_command(Handle, replyUserdata);

    /// <summary>
    /// Waits for an event or until timeout
    /// </summary>
    /// <param name="eventTimeout">Timeout in seconds</param>
    /// <returns>Error code</returns>
    public MPVEvent WaitEvent(double eventTimeout) => Marshal.PtrToStructure<MPVEvent>(mpv_wait_event(Handle, eventTimeout));

    /// <summary>
    /// Interrupts the current <see cref="WaitEvent"/> call
    /// </summary>
    public void WakeUp() => mpv_wakeup(Handle);

    /// <summary>
    /// Sets a custom function that should be called when there are new events
    /// </summary>
    /// <param name="callback">Callback function</param>
    /// <param name="data">Pointer to arbitrary data to pass to callback</param>
    public void SetWakeUpCallback(WakeUpCallback callback, nint data) => mpv_set_wakeup_callback(Handle, callback, data);

    /// <summary>
    /// Adds property to watch using events
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="format">Property <see cref="MPVFormat"/></param>
    /// <param name="replyUserdata">Optional reply Id</param>
    /// <returns>Error code</returns>
    public MPVError ObserveProperty(string name, MPVFormat format, ulong replyUserdata) => mpv_observe_property(Handle, replyUserdata, name, format);
    
    /// <summary>
    /// Undo all <see cref="ObserveProperty"/> for the given reply Id
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <returns>Number of properties to unobserve or error code</returns>
    public int UnobserveProperty(ulong replyUserdata) => mpv_unobserve_property(Handle, replyUserdata);

    /// <summary>
    /// Sets property using <see cref="MPVFormat.String">String format</see>
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    /// <returns>Error code</returns>
    public MPVError SetProperty(string name, string data) => mpv_set_property(Handle, name, MPVFormat.String, ref data);

    /// <summary>
    /// Sets property using <see cref="MPVFormat.Flag">Flag format</see>
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Flag data (0 or 1)</param>
    /// <returns>Error code</returns>
    public MPVError SetProperty(string name, int data) => mpv_set_property(Handle, name, MPVFormat.Flag, ref data);

    /// <summary>
    /// Sets property using <see cref="MPVFormat.Int64">Int64 format</see>
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Long int data</param>
    /// <returns>Error code</returns>
    public MPVError SetProperty(string name, long data) => mpv_set_property(Handle, name, MPVFormat.Int64, ref data);

    /// <summary>
    /// Sets property using <see cref="MPVFormat.Double">Double format</see>
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Double data</param>
    /// <returns>Error code</returns>
    public MPVError SetProperty(string name, double data) => mpv_set_property(Handle, name, MPVFormat.Double, ref data);

    /// <summary>
    /// Sets property using <see cref="MPVFormat.Node">Node format</see>
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data"><see cref="MPVNode"/> with data</param>
    /// <returns>Error code</returns>
    public MPVError SetProperty(string name, MPVNode data) => mpv_set_property(Handle, name, MPVFormat.Node, ref data);

    /// <summary>
    /// Sets property using <see cref="MPVFormat.String">String format</see>
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    /// <returns>Error code</returns>
    public MPVError SetPropertyString(string name, string data) => SetProperty(name, data);

    /// <summary>
    /// Sets property using <see cref="MPVFormat.Flag">Flag format</see> asynchronously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="data">Flag data (0 or 1)</param>
    /// <returns>Error code</returns>
    public MPVError SetPropertyAsync(ulong replyUserdata, string name, int data) => mpv_set_property_async(Handle, replyUserdata, name, MPVFormat.Flag, ref data);

    /// <summary>
    /// Sets property using <see cref="MPVFormat.Int64">Int64 format</see> asynchronously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="data">Long Int data</param>
    /// <returns>Error code</returns>
    public MPVError SetPropertyAsync(ulong replyUserdata, string name, long data) => mpv_set_property_async(Handle, replyUserdata, name, MPVFormat.Int64, ref data);

    /// <summary>
    /// Sets property using <see cref="MPVFormat.Double">Double format</see> asynchronously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="data">Double data</param>
    /// <returns>Error code</returns>
    public MPVError SetPropertyAsync(ulong replyUserdata, string name, double data) => mpv_set_property_async(Handle, replyUserdata, name, MPVFormat.Double, ref data);

    /// <summary>
    /// Sets property using <see cref="MPVFormat.String">String format</see> asynchronously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    /// <returns>Error code</returns>
    public MPVError SetPropertyAsync(ulong replyUserdata, string name, string data) => mpv_set_property_async(Handle, replyUserdata, name, MPVFormat.String, ref data);

    /// <summary>
    /// Sets property using <see cref="MPVFormat.Node">Node format</see> asynchronously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="data"><see cref="MPVNode"/> containing data</param>
    /// <returns>Error code</returns>
    public MPVError SetPropertyAsync(ulong replyUserdata, string name, MPVNode data) => mpv_set_property_async(Handle, replyUserdata, name, MPVFormat.Node, ref data);

    /// <summary>
    /// Gets property using <see cref="MPVFormat.Flag">Flag format</see>
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>Error code</returns>
    public MPVError GetProperty(string name, out int data) => mpv_get_property(Handle, name, MPVFormat.Flag, out data);

    /// <summary>
    /// Gets property using <see cref="MPVFormat.Int64">Int64 format</see>
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>Error code</returns>
    public MPVError GetProperty(string name, out long data) => mpv_get_property(Handle, name, MPVFormat.Int64, out data);

    /// <summary>
    /// Gets property using <see cref="MPVFormat.Double">Double format</see>
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>Error code</returns>
    public MPVError GetProperty(string name, out double data) => mpv_get_property(Handle, name, MPVFormat.Double, out data);

    /// <summary>
    /// Get property using <see cref="MPVFormat.String">String format</see>
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>Error code</returns>
    public MPVError GetProperty(string name, out string data) => mpv_get_property(Handle, name, MPVFormat.String, out data);

    /// <summary>
    /// Get property using <see cref="MPVFormat.Node">Node format</see>
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>Error code</returns>
    public MPVError GetProperty(string name, out MPVNode data) => mpv_get_property(Handle, name, MPVFormat.Node, out data);

    /// <summary>
    /// Get property using <see cref="MPVFormat.String">String format</see>
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>Error code</returns>
    public MPVError GetPropertyString(string name, out string data) => mpv_get_property(Handle, name, MPVFormat.String, out data);

    /// <summary>
    /// Get property using <see cref="MPVFormat.OSDString">OSDString format</see>
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>Error code</returns>
    public MPVError GetPropertyOSDString(string name, out string data) => mpv_get_property(Handle, name, MPVFormat.OSDString, out data);

    /// <summary>
    /// Gets property asynchronously
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="format"><see cref="MPVFormat"/> for returning data</param>
    /// <returns>Error code</returns>
    public MPVError GetPropertyAsync(ulong replyUserdata, string name, MPVFormat format) => mpv_get_property_async(Handle, replyUserdata, name, format);

    /// <summary>
    /// Deletes property
    /// </summary>
    /// <param name="name">Property name</param>
    /// <returns>Error code</returns>
    public MPVError DelProperty(string name) => mpv_del_property(Handle, name);
    
    /// <summary>
    /// Sets option using <see cref="MPVFormat.Node">Node format</see>
    /// </summary>
    /// <param name="name">Option name</param>
    /// <param name="data"><see cref="MPVNode"/> with data</param>
    /// <returns>Error code</returns>
    /// <remarks>
    /// You can't normally set options during runtime.
    /// </remarks>
    public MPVError SetOption(string name, MPVNode data) => mpv_set_option(Handle, name, MPVFormat.Node, ref data);

    /// <summary>
    /// Sets option using <see cref="MPVFormat.String">String format</see>
    /// </summary>
    /// <param name="name">Option name</param>
    /// <param name="data">String data</param>
    /// <returns>Error code</returns>
    /// <remarks>
    /// You can't normally set options during runtime.
    /// </remarks>
    public MPVError SetOptionString(string name, string data) => mpv_set_option_string(Handle, name, data);

    /// <summary>
    /// Requests log messages with specified minimum log level
    /// </summary>
    /// <param name="logLevel">Log level as string</param>
    /// <returns>Error code</returns>
    public MPVError RequestLogMessages(string logLevel) => mpv_request_log_messages(Handle, logLevel);

    /// <summary>
    /// Enables or disables the given event
    /// </summary>
    /// <remarks>
    /// All events are enabled by default
    /// </remarks>
    /// <param name="eid">Event Id</param>
    /// <param name="enabled">Whether the event should be enabled (1) or not (0)</param>
    /// <returns>Error code</returns>
    public MPVError RequestEvent(MPVEventId eid, int enabled) => mpv_request_event(Handle, eid, enabled);

    /// <summary>
    /// Blocks until all asynchronous requests are done
    /// </summary>
    public void WaitAsyncRequests() => mpv_wait_async_requests(Handle);

    /// <summary>
    /// Registers a hook handler.
    /// A hook is like a synchronous event that blocks the player.
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Hook name. This should be one of the documented names (see mpv manual).</param>
    /// <param name="priority">Hook priority</param>
    /// <returns>Error code</returns>
    public MPVError HookAdd(ulong replyUserdata, string name, int priority) => mpv_hook_add(Handle, replyUserdata, name, priority);

    /// <summary>
    /// Responds to a <see cref="MPVEventHook"/> event. You must call this after you have handled the event.
    /// </summary>
    /// <param name="id">This must be the value of the <see cref="MPVEventHook.Id"/> field</param>
    /// <returns>Error code</returns>
    public MPVError HookContinue(ulong id) => mpv_hook_continue(Handle, id);

    /// <summary>
    /// Disconnects and destroys the client
    /// </summary>
    public void Destroy() => mpv_destroy(Handle);

    /// <summary>
    /// Brings down the player and all its clients
    /// </summary>
    public void TerminateDestroy() => mpv_terminate_destroy(Handle);
}