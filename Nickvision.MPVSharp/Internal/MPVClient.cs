using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// MPV Client object
/// </summary>
/// <remarks>
/// This object implements most of functions from libmpv's client.h that accept mpv_handle
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
    // Get property
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out int data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out long data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out double data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out string data);
    // Set option
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_option(nint handle, string name, MPVFormat format, ref int data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_option(nint handle, string name, MPVFormat format, ref long data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_option(nint handle, string name, MPVFormat format, ref double data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_option(nint handle, string name, MPVFormat format, ref string data);
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

    private const int LC_NUMERIC = 1;
    private nint _handle { get; init; }

    /// <summary>
    /// Construct MPV Client
    /// </summary>
    /// <param name="handle">Optional handle to create client for the same player core</param>
    /// <param name="name">Optional unique client name</param>
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
    /// Clone object, creating new MPV Client for the same player core
    /// </summary>
    /// <returns>MPVClient as object</returns>
    public object Clone()
    {
        return new MPVClient(_handle);
    }

    /// <summary>
    /// Unique client name
    /// </summary>
    public string Name => mpv_client_name(_handle);

    /// <summary>
    /// Unique client ID
    /// </summary>
    public long Id => mpv_client_id(_handle);

    /// <summary>
    /// Load config file
    /// </summary>
    /// <param name="path">Absolute path to config</param>
    /// <returns>MPVError</returns>
    public MPVError LoadConfigFile(string path) => mpv_load_config_file(_handle, path);

    /// <summary>
    /// Init MPV client, should be called after setting options
    /// </summary>
    /// <returns>MPVError</returns>
    public MPVError Initialize() => mpv_initialize(_handle);

    /// <summary>
    /// Executes command array
    /// </summary>
    /// <param name="command">A command to execute as array of strings</param>
    /// <returns>MPVError</returns>
    public MPVError Command(string[] command)
    {
        var nullTermArray = new string?[command.Length + 1];
        Array.Copy(command, nullTermArray, command.Length);
        nullTermArray[^1] = null;
        return mpv_command(_handle, nullTermArray);
    }
    
    /// <summary>
    /// Executes command string
    /// </summary>
    /// <param name="command">A command string</param>
    /// <returns>MPVError</returns>
    public MPVError CommandString(string command) => mpv_command_string(_handle, command);
    
    /// <summary>
    /// Wait for an event or until timeout
    /// </summary>
    /// <param name="eventTimeout">Timeout in seconds</param>
    /// <returns>MPVError</returns>
    public MPVEvent WaitEvent(double eventTimeout) => Marshal.PtrToStructure<MPVEvent>(mpv_wait_event(_handle, eventTimeout));

    /// <summary>
    /// Add property to watch using events
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="format">Property MPVFormat</param>
    /// <param name="replyUserdata">Optional reply Id</param>
    /// <returns>MPVError</returns>
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
    /// <returns>MPVError</returns>
    public MPVError SetProperty(string name, string data) => mpv_set_property(_handle, name, MPVFormat.String, ref data);

    /// <summary>
    /// Set property using Flag format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Flag data (0 or 1)</param>
    /// <returns>MPVError</returns>
    public MPVError SetProperty(string name, int data) => mpv_set_property(_handle, name, MPVFormat.Flag, ref data);

    /// <summary>
    /// Set property using Int64 format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Long int data</param>
    /// <returns>MPVError</returns>
    public MPVError SetProperty(string name, long data) => mpv_set_property(_handle, name, MPVFormat.Int64, ref data);

    /// <summary>
    /// Set property using Double format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">String data</param>
    /// <returns>MPVError</returns>
    public MPVError SetProperty(string name, double data) => mpv_set_property(_handle, name, MPVFormat.Double, ref data);

    /// <summary>
    /// Get property using Flag format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>MPVError</returns>
    public MPVError GetProperty(string name, out int data) => mpv_get_property(_handle, name, MPVFormat.Flag, out data);

    /// <summary>
    /// Get property using Int64 format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>MPVError</returns>
    public MPVError GetProperty(string name, out long data) => mpv_get_property(_handle, name, MPVFormat.Flag, out data);


    /// <summary>
    /// Get property using Double format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>MPVError</returns>
    public MPVError GetProperty(string name, out double data) => mpv_get_property(_handle, name, MPVFormat.Flag, out data);


    /// <summary>
    /// Get property using String format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Object to write data to</param>
    /// <returns>MPVError</returns>
    public MPVError GetProperty(string name, out string data) => mpv_get_property(_handle, name, MPVFormat.Flag, out data);


    /// <summary>
    /// Destroy the client
    /// </summary>
    public void Destroy() => mpv_destroy(_handle);

    /// <summary>
    /// Bring down the player and all its clients
    /// </summary>
    public void TerminateDestroy() => mpv_terminate_destroy(_handle);
}