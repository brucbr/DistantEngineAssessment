using System;
using DistantEngine.Graphics;
using DistantEngine.Objects;
using DistantEngine.Objects.Components;
using Game.Objects;
using SDL2;
namespace Game
{
    internal class Program
    {
        public static void Main()
        {
            var win = new Window(800, 640, "Testing"); 
            Shared.CurrentLevel = new TextureMap();
            Shared.CurrentLevel.LoadMap(Shared.CurrentLevel._lvl1);
            var player = new Player("assets/player.png", 0, 0, 32, 32);
            var player1Col = player.GetComponent<ColliderComponent>();
            while (win.Running)
            {
                win.FrameCheck();
                win.HandleEvents();
                win.Update();
                win.Render();
            }
            win.Clean();
        }
    }
}