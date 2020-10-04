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
        private PhysicsComponent phys;

        public Player(string path, int xPos, int yPos, int refH, int refW) : base(xPos, yPos)
        {
            AddComponent<SpriteComponent>();
            AddComponent<ColliderComponent>();
            AddComponent<PhysicsComponent>();
            phys = GetComponent<PhysicsComponent>();
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

                phys.Transform.Position.x += h;
                phys.Transform.Position.y += v;
            }
        }
    }
}