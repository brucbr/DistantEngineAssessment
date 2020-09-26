using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using DistantEngine.Objects;
using DistantEngine.Objects.Components;
using SDL2;

namespace Game.Objects
{
    public class Player : GameObject
    {
        private float h, v;
        private float jump;
        public bool IsGrounded;
        private DateTime dateTime1, dateTime2;

        public Player(string path, int xPos, int yPos, int refH, int refW) : base(xPos, yPos)
        {
            AddComponent<SpriteComponent>();
            AddComponent<ColliderComponent>();
            AddComponent<Input>();
            GetComponent<SpriteComponent>().Initialise(path, 320, 320, 32, 32);
            IsGrounded = true;
        }
        [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
        public override void Update()
        {
            if (GetComponent<Input>() == null) return;
            
            base.Update();
            if (IsGrounded)
            {
                if (GetComponent<Input>().GetKey(SDL.SDL_Keycode.SDLK_UP) &&
                    GetComponent<Input>().GetKey(SDL.SDL_Keycode.SDLK_DOWN))
                {
                    v = 0;
                }
                else if (GetComponent<Input>().GetKey(SDL.SDL_Keycode.SDLK_DOWN))
                {
                    v = 1;
                }
                else if (GetComponent<Input>().GetKey(SDL.SDL_Keycode.SDLK_UP))
                {
                    v = -1;
                }
                else
                {
                    v = 0;
                }

                if (GetComponent<Input>().GetKey(SDL.SDL_Keycode.SDLK_LEFT) &&
                    GetComponent<Input>().GetKey(SDL.SDL_Keycode.SDLK_RIGHT))
                {
                    h = 0;
                }
                else if (GetComponent<Input>().GetKey(SDL.SDL_Keycode.SDLK_LEFT))
                {
                    h = -1;
                }
                else if (GetComponent<Input>().GetKey(SDL.SDL_Keycode.SDLK_RIGHT))
                {
                    h = 1;
                }
                else
                {
                    h = 0;
                }

                if (GetComponent<Input>().GetKey(SDL.SDL_Keycode.SDLK_SPACE))
                {
                    dateTime1 = DateTime.Now;
                    IsGrounded = false;
                }

                Position.x += h;
                Position.y += v;
            }
            else
            {
                dateTime2 = DateTime.Now;
                var diffInSeconds = -(dateTime1 - dateTime2).TotalSeconds;
                if (diffInSeconds < 1)
                {
                    Console.WriteLine(diffInSeconds);
                    Position.y -= 1;
                }

                if (diffInSeconds > 1)
                {
                    Position.y += 1;
                }
            }
        }
    }
}