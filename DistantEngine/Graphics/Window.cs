#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using SDL2;
using DistantEngine.Objects;
#endregion

namespace DistantEngine.Graphics
{
#region Window Manager
    public class Window
    {
        private readonly IntPtr _win;
        private readonly IntPtr _renderer;
        private const int Fps = 20;
        private const int FrameDelay = 100 / Fps;
        private uint _frameStart;
        private int _frameTime;
        public readonly bool Running;
        private readonly SDL.SDL_WindowFlags _flags;
        
        #region Window Constructor DEFAULT
        /// <summary>
        /// Default constructor for window.
        /// </summary>
        /// <param name="width">Width of window</param>
        /// <param name="height">Height of window</param>
        /// <param name="title"></param>
        public Window(int width, int height, string title)
        {
            Shared.Window = this;
            _flags = SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE;
            Running = false;
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) == 0)
            {
                _win = SDL.SDL_CreateWindow(title,
                    SDL.SDL_WINDOWPOS_CENTERED,
                    SDL.SDL_WINDOWPOS_CENTERED,
                    width,
                    height,
                    _flags
                    );
                _renderer = SDL.SDL_CreateRenderer(_win, -1, 0);
                Shared.Renderer = _renderer;
                // Check if window and render are still equal to null (IntPtr.Zero)
                if (_win != IntPtr.Zero)
                {
                    // Window true, check render 
                    if (_renderer != IntPtr.Zero)
                    { Running = true; SDL.SDL_SetRenderDrawColor(_renderer, 0, 0, 0, 255); }
                    else { Running = false; }
                }
                else { Running = false; }

            }
            else { Running = false; }
        }
        #endregion
        
        #region Window Constructor Elaborate
        public Window(int width, int height, int xPos, int yPos, bool fullscreen, string title)
        {
            Shared.Window = this;
            if (fullscreen) { _flags = SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE; }
            Running = false;
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) == 0)
            {
                _win = SDL.SDL_CreateWindow(title,
                    xPos,
                    yPos,
                    width,
                    height,
                    _flags
                );
                _renderer = SDL.SDL_CreateRenderer(_win, -1, 0);
                Shared.Renderer = _renderer;
                // Check if window and render are still equal to null (IntPtr.Zero)
                if (_win != IntPtr.Zero)
                {
                    // Window true, check render
                    if (_renderer != IntPtr.Zero)
                    { Running = true; SDL.SDL_SetRenderDrawColor(_renderer, 0, 0, 0, 255); }
                    else { Running = false; }
                }
                else { Running = false; }

            }
            else { Running = false; }
        }
        #endregion
        
        /// <summary>
        /// Method. Keep framerate constant
        /// </summary>
        public void FrameCheck()
        {
            _frameStart = SDL.SDL_GetTicks();
            _frameTime = Convert.ToInt32(SDL.SDL_GetTicks()) - Convert.ToInt32(_frameStart);
            if (FrameDelay > _frameTime) { SDL.SDL_Delay(Convert.ToUInt32(FrameDelay) - Convert.ToUInt32(_frameTime)); }
        }
        
        /// <summary>
        /// Method. Refresh global event listener and check global events.
        /// </summary>
        public void HandleEvents()
        {
            SDL.SDL_PollEvent(out SDL.SDL_Event e);
            Shared.E = e;
            QuitCheck();
        }
        
        /// <summary>
        /// Method. Update all game objects at each frame.
        /// </summary>
        public void Update()
        {
            foreach (GameObject obj in Shared.Objects)
            {
                obj.Update();
            }
        }

        /// <summary>
        /// Method. Render everything at each frame.
        /// </summary>
        public void Render()
        {
            SDL.SDL_RenderClear(_renderer);
            Shared.CurrentLevel.Draw();
            foreach (GameObject obj in Shared.Objects)
            {
                obj.Draw();
            }
            SDL.SDL_RenderPresent(_renderer);
        }
        
        /// <summary>
        /// Method. Clean window and get ready to quit.
        /// </summary>
        public void Clean()
        {
            SDL.SDL_DestroyWindow(_win);
            SDL.SDL_DestroyRenderer(_renderer);
            SDL.SDL_Quit();
        }
        
        /// <summary>
        /// Method. Check for quit events such as the 'close' window button.
        /// </summary>
        public void QuitCheck()
        {
            switch (Shared.E.type)
            {
                case SDL.SDL_EventType.SDL_QUIT:
                    SDL.SDL_Quit();
                    System.Environment.Exit(0);
                    break;
            }
        }
        
        /// <summary>
        /// Method. Reorder all game objects depending on their index value (The smaller the number, the earlier the render.) This allows for a z-index.
        /// </summary>
        public void Reorder()
        {
            Shared.Objects = Shared.Objects.OrderBy(obj => obj.ZIndex).ToList<GameObject>();
        }
    }
    #endregion

    /// <summary>
    /// Shared variables for window related matters.
    /// </summary>
    public static class Shared
    {
        public static IntPtr Renderer { get; set; } = IntPtr.Zero;
        public static Window Window { get; set; } = null;
        public static List<GameObject> Objects { get; set; } = new List<GameObject>();

        public static TextureMap CurrentLevel;
        public static SDL.SDL_Event E { get; set; }
    }
}