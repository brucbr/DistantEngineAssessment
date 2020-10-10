using System;
using System.Collections.Generic;
using System.Diagnostics;
using DistantEngine.Graphics;
using DistantEngine.Objects.TileMapping;
using SDL2;

namespace DistantEngine.Objects.Components
{
    public class AnimationComponent : IGoComponent, ITileComponent
    {
        private dynamic _parent;
        private Animation _currentAnimation;
        private List<Animation> _animations = new List<Animation>();

        public AnimationComponent(GameObject obj)
        {
            _parent = obj;
            Initialise();
        }

        public AnimationComponent(Tile obj)
        {
            _parent = obj;
            Initialise();
        }

        private void Initialise()
        {
            _currentAnimation = new Animation(_parent, "blank", 0, 0, 0, 0, 0, TextureManager.Set("assets/coin.png"));
        }

        public void Update()
        {
            _currentAnimation.Update();
        }

        public void Draw()
        {
            _currentAnimation.Draw();
        }

        public void NewAnimation(string name, int frames, int row, int speed, int sh, int sw, IntPtr texture)
        {
            var index = -1;
            try
            {
                index = _animations.FindIndex(x => x.Name == name);
            }
            catch (ArgumentNullException e)
            {
                index = -1;
            }
            finally
            {
                if (index >= 0)
                {
                    Console.WriteLine("Uh Oh! You already have an animation with the name: " + name);
                }
                else
                {
                    _animations.Add((Animation) Activator.CreateInstance(typeof(Animation), _parent, name, frames, row,
                        speed, sh, sw, texture));
                }
            }
        }

        public void NewAnimation(string name, int frames, int row, int speed, int sh, int sw, string path)
        {
            var index = -1;
            try
            {
                index = _animations.FindIndex(x => x.Name == name);
            }
            catch (ArgumentNullException e)
            {
                index = -1;
            }
            finally
            {
                if (index >= 0)
                {
                    Console.WriteLine("Uh Oh! You already have an animation with the name: " + name);
                }
                else
                {
                    _animations.Add((Animation) Activator.CreateInstance(typeof(Animation), _parent, name, frames, row,
                        speed, sh, sw, path));
                }
            }
        }

        public void SetAnimation(string name)
        {
            try
            {
                _currentAnimation = _animations.Find(x => x.Name == name);
                if (_currentAnimation != null) _currentAnimation.Flip = SDL.SDL_RendererFlip.SDL_FLIP_NONE;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("No animation set! No animation found with name: " + name);
            }
        }

        public void SetAnimation(string name, char flip_axis)
        {
            SDL.SDL_RendererFlip flip = SDL.SDL_RendererFlip.SDL_FLIP_NONE;
            try
            {
                if (flip_axis == 'h')
                {
                    flip = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
                }
                else if (flip_axis == 'v')
                {
                    flip = SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;
                }
                else if (flip_axis == 'n')
                {
                    flip = SDL.SDL_RendererFlip.SDL_FLIP_NONE;
                }
                else if (flip_axis == 'b')
                {
                    flip = SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL | SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("SetAnimation: Flip axis must be vertical (v), horizontal (h), both (b) or none (n)");
            }
            finally
            {
                try
                {
                    _currentAnimation = _animations.Find(x => x.Name == name);
                    if (_currentAnimation != null) _currentAnimation.Flip = SDL.SDL_RendererFlip.SDL_FLIP_NONE;
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("No animation set! No animation found with name: " + name);
                }
            }
        }

        public class Animation
        {
            private int _frames, _speed;
            private string _name;
            private dynamic _parent;
            private SDL.SDL_Rect _src, _dst;
            private IntPtr _texture;
            public string Name => _name;
            public SDL.SDL_RendererFlip Flip { get; set; } = SDL.SDL_RendererFlip.SDL_FLIP_NONE;


            public Animation(dynamic parent, string name, int frames, int row, int speed, int sh, int sw,
                IntPtr texture)
            {
                _parent = parent;
                _name = name;
                _frames = frames;
                _speed = speed;
                _src.y = sh * row;
                _src.x = 0;
                _src.w = sw;
                _src.h = sh;
                _dst.x = Convert.ToInt32(_parent.GetComponent<TransformComponent>().Position.x);
                _dst.y = Convert.ToInt32(_parent.GetComponent<TransformComponent>().Position.y);
                _dst.h = 32;
                _dst.w = 32;
                _texture = texture;
            }

            public Animation(dynamic parent, string name, int frames, int row, int speed, int sh, int sw,
                string spriteSheet)
            {
                _parent = parent;
                _name = name;
                _frames = frames;
                _speed = speed;
                _src.y = sh * row;
                _src.x = 0;
                _src.w = sw;
                _src.h = sh;
                _dst.x = Convert.ToInt32(_parent.GetComponent<TransformComponent>().Position.x);
                _dst.y = Convert.ToInt32(_parent.GetComponent<TransformComponent>().Position.y);
                _dst.h = 32;
                _dst.w = 32;
                _texture = TextureManager.Set(spriteSheet);
            }

            public void Update()
            {
                _dst.x = Convert.ToInt32(_parent.GetComponent<TransformComponent>().Position.x);
                _dst.y = Convert.ToInt32(_parent.GetComponent<TransformComponent>().Position.y);
                _src.x = _src.w * (Convert.ToInt32((SDL.SDL_GetTicks() / _speed) % _frames));
                Console.WriteLine(_src.y);
            }

            public void Draw()
            {
                TextureManager.Draw(_texture, _src, _dst, Flip);
            }
        }

    }
}