using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

public partial class MPVClient : ICloneable
{
    [LibraryImport("libc.so.6", StringMarshalling = StringMarshalling.Utf8)]
    private static partial nint setlocale(int category, string value);
    // Create client
    [LibraryImport("libmpv.so.2")]
    private static partial nint mpv_create();
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
    private static partial MPVError mpv_set_property(nint handle, string name, MPVFormat format, nint data);
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
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out nint data);
    // Set option
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_set_option(nint handle, string name, MPVFormat format, nint data);
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

    private const int LC_NUMERIC = 1;
    private nint _handle { get; init; }

    public MPVClient(nint? handle = null)
    {
        if (handle != null)
        {
            _handle = mpv_create_client(handle.Value, null);
        }
        else
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                setlocale(LC_NUMERIC, "C");
            }
            _handle = mpv_create();
        }
    }
    
    public object Clone()
    {
        return new MPVClient(_handle);
    }

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
    
    public MPVEvent WaitEvent(double eventTimeout) => Marshal.PtrToStructure<MPVEvent>(mpv_wait_event(_handle, eventTimeout));

    /// <summary>
    /// Adds property to watch in event loop
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="replyUserdata">reply Id</param>
    /// <returns>MPVError</returns>
    public MPVError ObserveProperty(string name, MPVFormat format, ulong replyUserdata) => mpv_observe_property(_handle, replyUserdata, name, format);
    
    /// <summary>
    /// Undo all ObserveProperty() for given reply Id
    /// </summary>
    /// <param name="replyUserdata">reply Id</param>
    /// <returns>Number of properties to unobserve or error code</returns>
    public int UnobserveProperty(ulong replyUserdata) => mpv_unobserve_property(_handle, replyUserdata);

    /// <summary>
    /// Sets property using specified format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="format">Property MPV format</param>
    /// <param name="data">Property value or pointer</param>
    /// <returns>MPVError</returns>
    public MPVError SetProperty(string name, MPVFormat format, object? data)
    {
        if (data == null)
        {
            return MPVError.PropertyError;
        }
        var success = MPVError.Success;
        switch (format)
        {
            case MPVFormat.String:
            case MPVFormat.OsdString:
                var sData = (string)data;
                success = mpv_set_property(_handle, name, format, ref sData);
                break;
            case MPVFormat.Flag:
                var fData = (bool)data ? 1 : 0;
                success = mpv_set_property(_handle, name, format, ref fData);
                break;
            case MPVFormat.Int64:
                var lData = (long)data;
                success = mpv_set_property(_handle, name, format, ref lData);
                break;
            case MPVFormat.Double:
                var dData = (double)data;
                success = mpv_set_property(_handle, name, format, ref dData);
                break;
            default:
                success = mpv_set_property(_handle, name, format, (nint)data);
                break;
        }
        return success;
    }
    
    public void Destroy() => mpv_destroy(_handle);
}