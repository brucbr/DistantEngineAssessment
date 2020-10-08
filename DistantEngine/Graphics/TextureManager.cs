#region Using Statements
using System;
using System.Xml.Serialization;
using SDL2;
using DistantEngine.Objects;
#endregion

namespace DistantEngine.Graphics
{
    #region Texture
    
    /// <summary>
    ///     Collection of all methods related to texture management
    /// </summary>
    public static class TextureManager
    {
        
        public static IntPtr Set(string path)
        {
            IntPtr texture, surface;
            surface = SDL_image.IMG_Load(path);
            texture = SDL.SDL_CreateTextureFromSurface(Shared.Renderer, surface);
            return texture;
        }

        public static void Draw(IntPtr texture, SDL.SDL_Rect src, SDL.SDL_Rect dst)
        {
            SDL.SDL_RenderCopy(Shared.Renderer, texture, ref src, ref dst);
        }
    }
    #endregion
}
