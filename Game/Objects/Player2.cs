using System;
using System.Collections.Generic;
using System.IO;
using DistantEngine.Objects;
using DistantEngine.Objects.Components;
using SDL2;

namespace Game.Objects
{
    public class Player2 : GameObject
    {
        private int h, v;
        private Input input;
        private SpriteComponent sprite;
        public Player2(string path, int xPos, int yPos, int refH, int refW) : base(xPos, yPos)
        {
            AddComponent<SpriteComponent>();
            AddComponent<ColliderComponent>();
            AddComponent<Input>();
            input = GetComponent<Input>();
            sprite = GetComponent<SpriteComponent>();
            sprite.Initialise(path, 320, 320, 32, 32);
        }
        public override void Update()
        {
            if (input != null)
            {
                base.Update();
                if (GetComponent<Input>().GetKey(SDL.SDL_Keycode.SDLK_w) &&
                    GetComponent<Input>().GetKey(SDL.SDL_Keycode.SDLK_s))
                {
                    v = 0;
                }
                else if (GetComponent<Input>().GetKey(SDL.SDL_Keycode.SDLK_s))
                {
                    v = 1;
                }
                else if (GetComponent<Input>().GetKey(SDL.SDL_Keycode.SDLK_w))
                {
                    v = -1;
                }
                else
                {
                    v = 0;
                }

                if (GetComponent<Input>().GetKey(SDL.SDL_Keycode.SDLK_a) &&
                    GetComponent<Input>().GetKey(SDL.SDL_Keycode.SDLK_d))
                {
                    h = 0;
                }
                else if (GetComponent<Input>().GetKey(SDL.SDL_Keycode.SDLK_a))
                {
                    h = -1;
                }
                else if (GetComponent<Input>().GetKey(SDL.SDL_Keycode.SDLK_d))
                {
                    h = 1;
                }
                else
                {
                    h = 0;
                }

                Position.x += h;
                Position.y += v;
            }
        }
    }
}