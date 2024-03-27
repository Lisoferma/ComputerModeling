using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;
using FoxCanvas;

namespace Explosion;

public class Window : GameWindow
{
    private const string TITLE = "Explosion";
    private const int CANVAS_WIDTH = 512;
    private const int CANVAS_HEIGHT = 512;

    private float _frameTime = 0.0f;
    private int _fps = 0;

    private Canvas _canvas;

    private Explosion _explosion;


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    protected override void OnLoad()
    {
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

        _canvas = new Canvas(CANVAS_WIDTH, CANVAS_HEIGHT);
        _explosion = new Explosion(CANVAS_WIDTH, CANVAS_HEIGHT, 100000, 7, Color.Orange, Color.Black);

        base.OnLoad();
    }


    protected override void OnRenderFrame(FrameEventArgs e)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);

        _explosion.Update();
        _canvas.SetImage(_explosion.GetImage());

        _canvas.Render();
        SwapBuffers();

        base.OnRenderFrame(e);
    }


    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        _frameTime += (float)e.Time;
        _fps++;

        if (_frameTime >= 1.0f)
        {
            Title = $"{TITLE} | {_fps} fps";
            _frameTime = 0.0f;
            _fps = 0;
        }

        if (KeyboardState.IsKeyDown(Keys.Escape))
            Close();

        if (MouseState.IsButtonDown(MouseButton.Left))
            _explosion.SetParticlesOnCenter();

        base.OnUpdateFrame(e);
    }


    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        GL.Viewport(0, 0, e.Width, e.Height);
        _canvas.Viewport(e.Width, e.Height);
            
        base.OnFramebufferResize(e);       
    }


    protected override void OnUnload()
    {
        _canvas.Dispose();
        base.OnUnload();
    }
}
