using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Nickvision.MPVSharp;

public partial class MPVClient : IDisposable
{
    [LibraryImport("libc.so.6", StringMarshalling = StringMarshalling.Utf8)]
    private static partial nint setlocale(int category, string value);
    // Create client
    [LibraryImport("libmpv.so.2")]
    private static partial nint mpv_create();
    // Init client
    [LibraryImport("libmpv.so.2")]
    private static partial MPVError mpv_initialize(nint handle);
    // Execute command
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_command(nint handle, string?[] command);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_command_string(nint handle, string command);
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
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out int data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out long data);
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_get_property(nint handle, string name, MPVFormat format, out double data);
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
    // Events
    [LibraryImport("libmpv.so.2")]
    private static partial nint mpv_wait_event(nint handle, double timeout);
    // Destroy client
    [LibraryImport("libmpv.so.2")]
    private static partial void mpv_destroy(nint handle);
    
    private const int LC_NUMERIC = 1;
    
    private readonly nint _handle;
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
    public MPVClient()
    {
        _disposed = false;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            setlocale(LC_NUMERIC, "C");
        }
        _handle = mpv_create();
        EventTimeout = 0;
        Task.Run(HandleEvents);
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
    public MPVError Command(string command) => mpv_command_string(_handle, command);
    
    /// <summary>
    /// MPV events loop
    /// </summary>
    private void HandleEvents()
    {
        while (!_disposed)
        {
            var mpvEvent = Marshal.PtrToStructure<MPVEvent>(mpv_wait_event(_handle, EventTimeout));
            switch (mpvEvent.Id)
            {
                case MPVEventId.Shutdown:
                    Dispose();
                    Destroyed?.Invoke();
                    break;
                case MPVEventId.PropertyChange:
                    var prop = Marshal.PtrToStructure<MPVEventProperty>(mpvEvent.Data);
                    if (prop.Format != MPVFormat.Node)
                    {
                        Console.WriteLine($"Property {prop.Name} is not in MPVNode format, skipping.");
                        break;
                    }
                    var node = Marshal.PtrToStructure<MPVNode>(prop.Data);
                    switch (node.Format)
                    {
                        case MPVFormat.String:
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop.Name, node.Format, Marshal.PtrToStringUTF8(node.String) ?? ""));
                            break;
                        case MPVFormat.Flag:
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop.Name, node.Format, node.Flag));
                            break;
                        case MPVFormat.Int64:
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop.Name, node.Format, node.Int64));
                            break;
                        case MPVFormat.Double:
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop.Name, node.Format, node.Double));
                            break;
                    };
                    break;
            }
        }
    }
    
    /// <summary>
    /// Adds property to watch in event loop
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="replyUserdata">reply Id</param>
    /// <returns>MPVError</returns>
    public MPVError ObserveProperty(string name, ulong replyUserdata = 0) => mpv_observe_property(_handle, replyUserdata, name, MPVFormat.Node);

    /// <summary>
    /// Undo all ObserveProperty() for given reply Id
    /// </summary>
    /// <param name="replyUserdata">reply Id</param>
    /// <returns>Number of properties to unobserve or error code</returns>
    public int UnobserveProperty(ulong replyUserdata = 0) => mpv_unobserve_property(_handle, replyUserdata);
    
    /// <summary>
    /// Sets flag property
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Property value</param>
    /// <returns>MPVError</returns>
    public MPVError SetProperty(string name, bool data)
    {
        var flag = data ? 1 : 0;
        return mpv_set_property(_handle, name, MPVFormat.Flag, ref flag);
    }

    /// <summary>
    /// Sets long int property
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Property value</param>
    /// <returns>MPVError</returns>
    public MPVError SetProperty(string name, long data) => mpv_set_property(_handle, name, MPVFormat.Int64, ref data);

    /// <summary>
    /// Sets double property
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Property value</param>
    /// <returns>MPVError</returns>
    public MPVError SetProperty(string name, double data) => mpv_set_property(_handle, name, MPVFormat.Double, ref data);

    /// <summary>
    /// Sets string property
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Property value</param>
    /// <returns>MPVError</returns>
    public MPVError SetProperty(string name, string data) => mpv_set_property(_handle, name, MPVFormat.String, ref data);

    /// <summary>
    /// Sets property using specified format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="format">Property MPV format</param>
    /// <param name="data">Pointer to property value</param>
    /// <returns>MPVError</returns>
    public MPVError SetProperty(string name, MPVFormat format, nint data) => mpv_set_property(_handle, name, format, data);

    /// <summary>
    /// Gets property as bool
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Out bool</param>
    /// <returns>MPVError</returns>
    public MPVError GetProperty(string name, out bool data)
    {
        int flag;
        var res = mpv_get_property(_handle, name, MPVFormat.Flag, out flag);
        data = flag == 1;
        return res;
    }

    /// <summary>
    /// Gets property as int
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Out int</param>
    /// <returns>MPVError</returns>
    public MPVError GetProperty(string name, out int data) => mpv_get_property(_handle, name, MPVFormat.Flag, out data);

    /// <summary>
    /// Gets property as long int
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Out long</param>
    /// <returns>MPVError</returns>
    public MPVError GetProperty(string name, out long data) => mpv_get_property(_handle, name, MPVFormat.Int64, out data);

    /// <summary>
    /// Gets property as double
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Out double</param>
    /// <returns>MPVError</returns>
    public MPVError GetProperty(string name, out double data) => mpv_get_property(_handle, name, MPVFormat.Double, out data);

    /// <summary>
    /// Gets property as string
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Out string</param>
    /// <returns>MPVError</returns>
    public MPVError GetProperty(string name, out string data)
    {
        nint ptr;
        var res = mpv_get_property(_handle, name, MPVFormat.String, out ptr);
        data = res < MPVError.Success ? "" : (Marshal.PtrToStringUTF8(ptr) ?? "");
        return res;
    }

    /// <summary>
    /// Gets property as OSD formatted string
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="data">Out string</param>
    /// <returns>MPVError</returns>
    public MPVError GetPropertyOsdString(string name, out string data)
    {
        nint ptr;
        var res = mpv_get_property(_handle, name, MPVFormat.OsdString, out ptr);
        data = res < MPVError.Success ? "" : (Marshal.PtrToStringUTF8(ptr) ?? "");
        return res;
    }
    
    /// <summary>
    /// Gets property in specified format
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="format">Property MPV format</param>
    /// <param name="data">Out pointer to value</param>
    /// <returns>MPVError</returns>
    public MPVError GetProperty(string name, MPVFormat format, out nint data) => mpv_get_property(_handle, name, format, out data);

    /// <summary>
    /// Sets flag option
    /// </summary>
    /// <param name="name">Option name</param>
    /// <param name="data">Option value</param>
    /// <returns>MPVError</returns>
    public MPVError SetOption(string name, bool data)
    {
        var flag = data ? 1 : 0;
        return mpv_set_option(_handle, name, MPVFormat.Flag, ref flag);
    }

    /// <summary>
    /// Sets long int option
    /// </summary>
    /// <param name="name">Option name</param>
    /// <param name="data">Option value</param>
    /// <returns>MPVError</returns>
    public MPVError SetOption(string name, long data) => mpv_set_option(_handle, name, MPVFormat.Int64, ref data);

    /// <summary>
    /// Sets double option
    /// </summary>
    /// <param name="name">Option name</param>
    /// <param name="data">Option value</param>
    /// <returns>MPVError</returns>
    public MPVError SetOption(string name, double data) => mpv_set_option(_handle, name, MPVFormat.Double, ref data);

    /// <summary>
    /// Sets string option
    /// </summary>
    /// <param name="name">Option name</param>
    /// <param name="data">Option value</param>
    /// <returns>MPVError</returns>
    public MPVError SetOption(string name, string data) => mpv_set_option(_handle, name, MPVFormat.String, ref data);

    /// <summary>
    /// Sets option in specified format
    /// </summary>
    /// <param name="name">Option name</param>
    /// <param name="format">Option MPV format</param>
    /// <param name="data">Option value</param>
    /// <returns>MPVError</returns>
    public MPVError SetOption(string name, MPVFormat format, nint data) => mpv_set_option(_handle, name, format, data);
    
    /// <summary>
    /// Finalizes the MPVClient
    /// </summary>
    ~MPVClient() => Dispose(false);

    /// <summary>
    /// Frees resources used by the MPVClient object
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Frees resources used by the MPVClient object
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }
        if (disposing)
        {
            mpv_destroy(_handle);
        }
        _disposed = true;
    }
}