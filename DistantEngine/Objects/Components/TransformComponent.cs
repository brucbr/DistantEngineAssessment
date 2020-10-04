using System;
using DistantEngine.Graphics;

namespace DistantEngine.Objects.Components
{
    public class TransformComponent : IGoComponent, ITileComponent
    {
        public Vector2D Position { get; set; } = new Vector2D(0, 0);

        public TransformComponent(dynamic obj)
        {
            
        }

        public void Update()
        {
            
        }

        public void Draw()
        {
            
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}