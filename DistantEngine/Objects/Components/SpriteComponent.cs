using System;
using DistantEngine.Graphics;
using DistantEngine.Objects.TileMapping;
using SDL2;

namespace DistantEngine.Objects.Components
{
    public class SpriteComponent : IGoComponent, ITileComponent
    {
        public Vector2D Position;
        private IntPtr _tex;
        private SDL.SDL_Rect _src;
        public SDL.SDL_Rect Dst;
        public dynamic BaseObject;

        public SpriteComponent(){}

        /// <summary>
        /// Sprite Component constructor. Set ID. Set Parent.
        /// </summary>
        /// <param name="path"></param>
        public SpriteComponent(GameObject obj)
        {
            BaseObject = obj;
        }

        public SpriteComponent(Tile obj)
        {
            BaseObject = obj;
        }



        /// <summary>
        /// Initialise all values for basic single texture image.
        /// </summary>
        /// <param name="path">Image path</param>
        /// <param name="sh">Source image height</param>
        /// <param name="sw">Source image width</param>
        /// <param name="dh">Destination height</param>
        /// <param name="dw">Desintation width</param>
        public void Initialise(string path, int sh, int sw, int dh, int dw)
        {
            Position = BaseObject.GetComponent<TransformComponent>().Position;
            _src.x = _src.y = 0;
            _src.h = sh;
            _src.w = sw;
            Dst.h = dh;
            Dst.w = dw;
            Dst.x = Convert.ToInt32(BaseObject.GetComponent<TransformComponent>().Position.x);
            Dst.y = Convert.ToInt32(BaseObject.GetComponent<TransformComponent>().Position.y);
            _tex = TextureManager.Set(path);
        }
        
        /// <summary>
        /// Initialise all values for multi-texture image. Pass through texture.
        /// </summary>
        /// <param name="tex">Texture</param>
        /// <param name="sh">Source image height</param>
        /// <param name="sw">Source image width</param>
        /// <param name="sx">Source image x-pos</param>
        /// <param name="sy">Source image y-pos</param>
        /// <param name="dh">Destination height</param>
        /// <param name="dw">Destination width</param>
        public void Initialise(IntPtr tex, int sh, int sw, int sx, int sy, int dh, int dw)
        {
            Position = BaseObject.GetComponent<TransformComponent>().Position;
            _src.x = sx;
            _src.y = sy;
            _src.h = sh;
            _src.w = sw;
            Dst.h = dh;
            Dst.w = dw;
            Dst.x = Convert.ToInt32(BaseObject.GetComponent<TransformComponent>().Position.x);
            Dst.y = Convert.ToInt32(BaseObject.GetComponent<TransformComponent>().Position.y);
            _tex = tex;
        }
        /// <summary>
        /// Method. Update function to be called at each frame.
        /// </summary>
        public void Update()
        {
            Position = BaseObject.GetComponent<TransformComponent>().Position;
            Dst.x = Convert.ToInt32(BaseObject.GetComponent<TransformComponent>().Position.x);
            Dst.y = Convert.ToInt32(BaseObject.GetComponent<TransformComponent>().Position.y);
        }

        /// <summary>
        /// Draw function to be called at each frame.
        /// </summary>
        public void Draw()
        {
            TextureManager.Draw(_tex, _src, Dst);
        }
    }
}