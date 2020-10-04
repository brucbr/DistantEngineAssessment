using System;
using System.Linq;
using System.Runtime.InteropServices;
using SDL2;

namespace DistantEngine.Objects.Components
{
    public class Input : IGoComponent
    {
        public Input(GameObject obj)
        {
            
        }
        public void Initialise(GameObject obj)
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            
        }

        public void Draw()
        {
            
        }

        public long GetId()
        {
            return 1;
        }

        public bool GetKey(SDL.SDL_Keycode _keycode)
        {
            IntPtr origArray = SDL.SDL_GetKeyboardState(out int arraySize);
            byte[] keys = new byte[arraySize];
            Marshal.Copy(origArray, keys, 0, arraySize);
            return keys[(byte) SDL.SDL_GetScancodeFromKey(_keycode)] == 1;
        }
    }
}