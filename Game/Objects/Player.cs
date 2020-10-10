
using System.Diagnostics.CodeAnalysis;
using DistantEngine.Graphics;
using DistantEngine.Objects;
using DistantEngine.Objects.Components;
using SDL2;

namespace Game.Objects
{
    public class Player : GameObject
    {
        private float _h, _v;
        private float _jump;
        public bool IsGrounded;
        private InputComponent _input;
        private AnimationComponent _animate;
        private TransformComponent _transform;
        private char flip = 'n';
        public Player(string path, int xPos, int yPos, int refH, int refW) : base(xPos, yPos)
        {
            AddComponent<InputComponent>();
            AddComponent<AnimationComponent>();
            _transform = GetComponent<TransformComponent>();
            _animate = GetComponent<AnimationComponent>();
            _input = GetComponent<InputComponent>();

            var texture = TextureManager.Set("assets/playersheet.png");
            // Create Animations
            _animate.NewAnimation("idle", 2, 0, 200, 16, 16, texture);
            _animate.NewAnimation("running", 4, 2, 200, 16, 16, texture);
            _animate.SetAnimation("idle");
            
            IsGrounded = true;
        }
        [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
        public override void Update()
        {
            if (_input == null) return;
            
            base.Update();
            if (IsGrounded)
            {
                if (_input.GetKey(SDL.SDL_Keycode.SDLK_UP) &&
                    _input.GetKey(SDL.SDL_Keycode.SDLK_DOWN))
                {
                    _v = 0;
                }
                else if (_input.GetKey(SDL.SDL_Keycode.SDLK_DOWN))
                {
                    _v = 1;
                }
                else if (_input.GetKey(SDL.SDL_Keycode.SDLK_UP))
                {
                    _v = -1;
                }
                else
                {
                    _v = 0;
                }

                if (_input.GetKey(SDL.SDL_Keycode.SDLK_LEFT) &&
                    _input.GetKey(SDL.SDL_Keycode.SDLK_RIGHT))
                {
                    _h = 0;
                }
                else if (_input.GetKey(SDL.SDL_Keycode.SDLK_LEFT))
                {
                    _h = -1;
                }
                else if (_input.GetKey(SDL.SDL_Keycode.SDLK_RIGHT))
                {
                    _h = 1;
                }
                else
                {
                    _h = 0;
                }

                /*if (_input.GetKey(SDL.SDL_Keycode.SDLK_SPACE))
                {
                    dateTime1 = DateTime.Now;
                    IsGrounded = false;
                }*/
                
                if (_h != 0 || _v != 0)
                {
                    if (_h < 0)
                    {
                        flip = 'v';
                    } else if (_h > 0)
                    {
                        flip = 'n';
                    }
                    _animate.SetAnimation("running", flip);
                }
                else
                {
                    _animate.SetAnimation("idle", flip);
                }
                _transform.Position.x += _h;
                _transform.Position.y += _v;
            }
        }
    }
}