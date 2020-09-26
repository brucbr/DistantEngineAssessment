using System;
using System.Collections.Generic;
using System.Numerics;
using DistantEngine.Objects.Components;
using DistantEngine.Objects.TileMapping;
using SDL2;

namespace DistantEngine.Graphics
{
    public class TextureMap
    {
        private SDL.SDL_Rect _src, _dst;
        private readonly IntPtr _grass;
        private readonly IntPtr _dirt;
        private readonly IntPtr _water;
        private int[,] _map = new int[20, 25];
        private List<Tile> tiles;

        /// <summary>
        /// Example tile map
        /// </summary>
        public readonly int[,] _lvl1 =
        {
            {0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,1,2,2,2,2,2},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,2,2,2},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,2,2,2},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,2,2,2},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2,2,2,2},
            {0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,1,1,2,2},
            {0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
            {0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
        };

        /// <summary>
        /// Constructor. Set tile map types
        /// </summary>
        public TextureMap()
        {
            _dirt = Texture.Set("assets/dirt.jpg");
            _grass = Texture.Set("assets/grass.png");
            _water = Texture.Set("assets/water.jpg");
            Console.WriteLine(SDL.SDL_GetError());
            _dst.x = 0;
            _dst.y = 0;
            _src.w = _src.h = 128;
            _dst.w = _dst.h = 32;
            _src.x = _src.y = 0;
        }

        /// <summary>
        /// Load map array and set private variable.
        /// </summary>
        /// <param name="toLoad"></param>
        public void LoadMap(int[,] toLoad)
        {
            _map = (int[,]) toLoad.Clone();
            int type = 0;
            tiles = new List<Tile>();
            for (int row = 0; row < _map.GetLength(0); ++row)
            {
                for (int column = 0; column < _map.GetLength(1); ++column)
                {
                    type = _map[row, column];
                    var tile = new Tile();
                    switch (type)
                    {
                        case 0:
                            tile.Initialise();
                            tile.Position.x = column * 32;
                            tile.Position.y = row * 32;
                            tile.AddComponent<SpriteComponent>();
                            tile.GetComponent<SpriteComponent>().Initialise(_water, 32, 32, 0, 0, 32, 32);
                            break;
                        case 1:
                            tile.Initialise();
                            tile.Position.x = column * 32;
                            tile.Position.y = row * 32;
                            tile.AddComponent<SpriteComponent>();
                            tile.GetComponent<SpriteComponent>().Initialise(_dirt, 32, 32, 0, 0, 32, 32);
                            tile.Position.x = column * 32;
                            tile.Position.y = row * 32;
                            break;
                        case 2:
                            tile.Initialise();
                            tile.Position.x = column * 32;
                            tile.Position.y = row * 32;
                            tile.AddComponent<SpriteComponent>();
                            tile.GetComponent<SpriteComponent>().Initialise(_grass, 32, 32, 0, 0, 32, 32);
                            tile.Position.x = column * 32;
                            tile.Position.y = row * 32;
                            break;
                    }

                    tiles.Add(tile);
                }
            }
        }

        /// <summary>
        /// Draw loaded map to screen. Z-Index of 1
        /// </summary>
        public void Draw()
        {
            foreach (Tile tile in tiles)
            {
                tile.Draw();
            }
        }

        /// <summary>
        /// Clean and "destroy" current tilemap.
        /// </summary>
        public void Clean()
        {
            // Signal for GC Cleanup
            tiles = null;
        }
    }
}