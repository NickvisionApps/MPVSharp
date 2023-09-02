using Nickvision.MPVSharp;
using System;
using static SDL2.SDL;

namespace Nickvision.MPVSharp.Examples.SDL;

public class Program
{
    private readonly nint _window;
    private readonly nint _renderer;
    private readonly nint _glCtx;
    private readonly Client _player;
    private readonly RenderContext _playerCtx;
    private bool _running;
    
    public static void Main(string[] args) => new Program().Run();
    
    /// <summary>
    /// Construct program, initialize SDL and MPV
    /// </summary>
    public Program()
    {
        if (SDL_Init(SDL_INIT_VIDEO) < 0)
        {
            throw new Exception($"Failed to initialize SDL: {SDL_GetError()}");
        }
        _window = SDL_CreateWindow("SDL2 MPV Example", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, 640, 360, SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL_WindowFlags.SDL_WINDOW_RESIZABLE | SDL_WindowFlags.SDL_WINDOW_OPENGL);
        if (_window == IntPtr.Zero)
        {
            throw new Exception($"Failed to create SDL Window: {SDL_GetError()}");
        }
        _glCtx = SDL_GL_CreateContext(_window);
        _renderer = SDL_CreateRenderer(_window, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
        if (_renderer == IntPtr.Zero)
        {
            throw new Exception($"Failed to create renderer: {SDL_GetError()}");
        }
        // Create MPV Client
        _player = new Client();
        _player.SetProperty("ytdl", true);
        _player.RequestLogMessages("debug");
        _player.LogMessageReceived += (sender, e) =>
        {
            Console.Write($"[{e.Prefix}] {e.Text}"); // Log messages (e.Text) end with newline char
        };
        _player.Initialize();
        _playerCtx = _player.CreateRenderContext();
        // We will call rendering method manually, no need for callback
        _playerCtx.SetupGL(null);
    }
    
    /// <summary>
    /// Start playing, handle events and render. Quit when finished.
    /// </summary>
    private void Run()
    {
        _running = true;
        _player.LoadFile("https://www.youtube.com/watch?v=UXqq0ZvbOnk");
        while (_running)
        {
            PollEvents();
            Render();
        }
        _playerCtx.Dispose();
        _player.Dispose();
        SDL_DestroyRenderer(_renderer);
        SDL_GL_DeleteContext(_glCtx);
        SDL_DestroyWindow(_window);
        SDL_Quit();
    }
    /// <summary>
    /// Process SDL events
    /// </summary>
    private void PollEvents()
    {
        while (SDL_PollEvent(out SDL_Event e) == 1)
        {
            switch (e.type)
            {
                case SDL_EventType.SDL_QUIT:
                    _running = false;
                    break;
                case SDL_EventType.SDL_KEYDOWN:
                    if (e.key.keysym.sym == SDL_Keycode.SDLK_SPACE)
                    {
                        _player.CyclePause();
                    }
                    else if (e.key.keysym.sym == SDL_Keycode.SDLK_LEFT)
                    {
                        _player.Seek(-0.5);
                    }
                    else if (e.key.keysym.sym == SDL_Keycode.SDLK_RIGHT)
                    {
                        _player.Seek(0.5);
                    }
                    break;
            }
        }
    }
    
    /// <summary>
    /// Render frame
    /// </summary>
    private void Render()
    {
        SDL_RenderClear(_renderer);
        SDL_GetWindowSize(_window, out var width, out var height);
        _playerCtx.RenderGL(width, height);
        SDL_GL_SwapWindow(_window);
    }
}
