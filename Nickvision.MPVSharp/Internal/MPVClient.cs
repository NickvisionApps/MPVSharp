using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// MPV Client object
/// </summary>
/// <remarks>
/// This object implements most of functions from libmpv's client.h that accept mpvHandle
/// </remarks>
public partial class MPVClient : ICloneable
{
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
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_command_ret(nint handle, string?[] command, out MPVNode node);
    // Events
    [LibraryImport("libmpv.so.2")]
    private static partial nint mpv_wait_event(nint handle, double timeout);
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
    // Set option
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_option(nint handle, string name, MPVFormat format, ref MPVNode data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_option_string(nint handle, string name, string data);
    // Destroy client
    [LibraryImport("libmpv.so.2")]
    private static partial void mpv_destroy(nint handle);
    [LibraryImport("libmpv.so.2")]
    private static partial void mpv_terminate_destroy(nint handle);
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

    private const int LcNumeric = 1;
    protected readonly nint Handle;

    /// <summary>
    /// Construct MPV Client
    /// </summary>
    /// <param name="handle">Optional handle to create client for the same player core</param>
    /// <param name="name">Optional unique client name</param>
    public MPVClient(nint? handle = null, string? name = null)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            setlocale(LcNumeric, "C");
        }
        Handle = mpv_create_client(handle ?? IntPtr.Zero, name);
        if (Handle == IntPtr.Zero)
        {
            throw new Exception("Failed to create MPV client");
        }
    }
    
    /// <summary>
    /// Clone object, creating new MPV Client for the same player core
    /// </summary>
    /// <returns>MPVClient as object</returns>
    public object Clone()
    {
        return new MPVClient(Handle);
    }

    /// <summary>
    /// Unique client name
    /// </summary>
    public string Name => mpv_client_name(Handle);

    /// <summary>
    /// Unique client ID
    /// </summary>
    public long Id => mpv_client_id(Handle);

    /// <summary>
    /// Load config file
    /// </summary>
    /// <param name="path">Absolute path to config</param>
    /// <returns>MPVError</returns>
    public MPVError LoadConfigFile(string path) => mpv_load_config_file(Handle, path);

    /// <summary>
    /// Get internal time
    /// </summary>
    /// <returns>Time in milliseconds</returns>
    public long GetTimeUs() => mpv_get_time_us(Handle);

    /// <summary>
    /// Init MPV client, should be called after setting options
    /// </summary>
    /// <returns>MPVError</returns>
    public MPVError Initialize() => mpv_initialize(Handle);

    /// <summary>
    /// Execute command array
    /// </summary>
    /// <param name="command">A command to execute as array of strings</param>
    /// <returns>MPVError</returns>
    public MPVError Command(string[] command)
    {
        var nullTermArray = new string?[command.Length + 1];
        Array.Copy(command, nullTermArray, command.Length);
        nullTermArray[^1] = null;
        return mpv_command(Handle, nullTermArray);
    }

    /// <summary>
    /// Execute command array and return the result
    /// </summary>
    /// <param name="command">A command to execute as array of strings</param>
    /// <param name="node">MPVNode with the result of command</param>
    /// <returns>MPVError</returns>
    public MPVError CommandRet(string[] command, out MPVNode node)
    {
        var nullTermArray = new string?[command.Length + 1];
        Array.Copy(command, nullTermArray, command.Length);
        nullTermArray[^1] = null;
        return mpv_command_ret(Handle, nullTermArray, out node);
    }

    /// <summary>
    /// Execute command string
    /// </summary>
    /// <param name="command">A command string</param>
    /// <returns>MPVError</returns>
    public MPVError CommandString(string command) => mpv_command_string(Handle, command);
    
    /// <summary>
    /// Wait for an event or until timeout
    /// </summary>
    /// <param name="eventTimeout">Timeout in seconds</param>
    /// <returns>MPVError</returns>
    public MPVEvent WaitEvent(double eventTimeout) => Marshal.PtrToStructure<MPVEvent>(mpv_wait_event(Handle, eventTimeout));

    /// <summary>
    /// Add property to watch using events
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="format">Property MPVFormat</param>
    /// <param name="replyUserdata">Optional reply Id</param>
    /// <returns>MPVError</returns>
    public MPVError ObserveProperty(string name, MPVFormat format, ulong replyUserdata) => mpv_observe_property(Handle, replyUserdata, name, format);
    
    /// <summary>
    /// Undo all ObserveProperty() for given reply Id
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <returns>Number of properties to unobserve or error code</returns>
    public int UnobserveProperty(ulong replyUserdata) => mpv_unobserve_property(Handle, replyUserdata);

    /// <summary>
    /// Set property using String format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    /// <returns>MPVError</returns>
    public MPVError SetProperty(string name, string data) => mpv_set_property(Handle, name, MPVFormat.String, ref data);

    /// <summary>
    /// Set property using Flag format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Flag data (0 or 1)</param>
    /// <returns>MPVError</returns>
    public MPVError SetProperty(string name, int data) => mpv_set_property(Handle, name, MPVFormat.Flag, ref data);

    /// <summary>
    /// Set property using Int64 format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Long int data</param>
    /// <returns>MPVError</returns>
    public MPVError SetProperty(string name, long data) => mpv_set_property(Handle, name, MPVFormat.Int64, ref data);

    /// <summary>
    /// Set property using Double format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    /// <returns>MPVError</returns>
    public MPVError SetProperty(string name, double data) => mpv_set_property(Handle, name, MPVFormat.Double, ref data);

    /// <summary>
    /// Set property using Node format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">MPVNode with data</param>
    /// <returns>MPVError</returns>
    public MPVError SetProperty(string name, MPVNode data) => mpv_set_property(Handle, name, MPVFormat.Node, ref data);

    /// <summary>
    /// Set property using String format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    /// <returns>MPVError</returns>
    public MPVError SetPropertyString(string name, string data) => mpv_set_property(Handle, name, MPVFormat.String, ref data);

    /// <summary>
    /// Get property using Flag format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>MPVError</returns>
    public MPVError GetProperty(string name, out int data) => mpv_get_property(Handle, name, MPVFormat.Flag, out data);

    /// <summary>
    /// Get property using Int64 format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>MPVError</returns>
    public MPVError GetProperty(string name, out long data) => mpv_get_property(Handle, name, MPVFormat.Flag, out data);


    /// <summary>
    /// Get property using Double format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>MPVError</returns>
    public MPVError GetProperty(string name, out double data) => mpv_get_property(Handle, name, MPVFormat.Flag, out data);


    /// <summary>
    /// Get property using String format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>MPVError</returns>
    public MPVError GetProperty(string name, out string data) => mpv_get_property(Handle, name, MPVFormat.Flag, out data);


    /// <summary>
    /// Get property using Node format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>MPVError</returns>
    public MPVError GetProperty(string name, out MPVNode data) => mpv_get_property(Handle, name, MPVFormat.Node, out data);

    /// <summary>
    /// Set option using Node format
    /// </summary>
    /// <param name="name">Option name</param>
    /// <param name="data">MPVNode with data</param>
    /// <returns>MPVError</returns>
    public MPVError SetOption(string name, MPVNode data) => mpv_set_option(Handle, name, MPVFormat.Node, ref data);

    /// <summary>
    /// Set option using Node format
    /// </summary>
    /// <param name="name">Option name</param>
    /// <param name="data">String data</param>
    /// <returns>MPVError</returns>
    public MPVError SetOptionString(string name, string data) => mpv_set_option_string(Handle, name, data);

    /// <summary>
    /// Request log messages with specified minimum log level
    /// </summary>
    /// <param name="logLevel">Log level as string</logLevel>
    /// <returns>MPVError</returns>
    public MPVError RequestLogMessages(string logLevel) => mpv_request_log_messages(Handle, logLevel);

    /// <summary>
    /// Destroy the client
    /// </summary>
    public void Destroy() => mpv_destroy(Handle);

    /// <summary>
    /// Bring down the player and all its clients
    /// </summary>
    public void TerminateDestroy() => mpv_terminate_destroy(Handle);
}