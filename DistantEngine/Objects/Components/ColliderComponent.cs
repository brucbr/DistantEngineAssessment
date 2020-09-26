using System;
using DistantEngine.Graphics;
using SDL2;

namespace DistantEngine.Objects.Components
{
    public class ColliderComponent : IGoComponent, ITileComponent
    {
        public string direction;
        private SDL.SDL_Rect _rect;
        private GameObject BaseObject;
        public SDL.SDL_Rect Rect => _rect;

        public bool Collided(SDL.SDL_Rect obj1, SDL.SDL_Rect obj2)
        {
            if (obj1.x + obj1.w >= obj2.x &&
                obj2.x + obj2.w >= obj1.x &&
                obj1.y + obj1.h >= obj2.y &&
                obj2.y + obj2.h >= obj1.y)
            {
                return true;
            }

            return false;
        }

        public bool Collided(ColliderComponent col2)
        {
            return Collided(this.Rect, col2.Rect);
        }
        
        public ColliderComponent(GameObject obj)
        {
            BaseObject = obj;
            _rect = BaseObject.GetComponent<SpriteComponent>().Dst;

        }
        public void Initialise()
        {
            
        }

        public void Update()
        {
            _rect = BaseObject.GetComponent<SpriteComponent>().Dst;
        }

        public void Draw()
        {
            
        }
    }
}