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
            AddComponent<InputComponent>();
            IsGrounded = true;
        }
        [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
        public override void Update()
        {
            if (GetComponent<InputComponent>() == null) return;
            
            base.Update();
            if (IsGrounded)
            {
                if (GetComponent<InputComponent>().GetKey(SDL.SDL_Keycode.SDLK_UP) &&
                    GetComponent<InputComponent>().GetKey(SDL.SDL_Keycode.SDLK_DOWN))
                {
                    v = 0;
                }
                else if (GetComponent<InputComponent>().GetKey(SDL.SDL_Keycode.SDLK_DOWN))
                {
                    v = 1;
                }
                else if (GetComponent<InputComponent>().GetKey(SDL.SDL_Keycode.SDLK_UP))
                {
                    v = -1;
                }
                else
                {
                    v = 0;
                }

                if (GetComponent<InputComponent>().GetKey(SDL.SDL_Keycode.SDLK_LEFT) &&
                    GetComponent<InputComponent>().GetKey(SDL.SDL_Keycode.SDLK_RIGHT))
                {
                    h = 0;
                }
                else if (GetComponent<InputComponent>().GetKey(SDL.SDL_Keycode.SDLK_LEFT))
                {
                    h = -1;
                }
                else if (GetComponent<InputComponent>().GetKey(SDL.SDL_Keycode.SDLK_RIGHT))
                {
                    h = 1;
                }
                else
                {
                    h = 0;
                }

                if (GetComponent<InputComponent>().GetKey(SDL.SDL_Keycode.SDLK_SPACE))
                {
                    dateTime1 = DateTime.Now;
                    IsGrounded = false;
                }

                this.GetComponent<TransformComponent>().Position.x += h;
                this.GetComponent<TransformComponent>().Position.y += v;
            }
        }
    }
}