﻿using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace Explosion;

public class Program
{
    static void Main(string[] args)
    {
        NativeWindowSettings nativeWindowSettings = new()
        {
            Title = "Explosion",
            ClientSize = new Vector2i(512, 512),      
        };

        GameWindowSettings gameWindowSettings = new()
        {
            UpdateFrequency = 60.0
        };

        using (Window window = new(gameWindowSettings, nativeWindowSettings))
        {
            window.Run();
        }
    }
}
