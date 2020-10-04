using System;
using System.Collections.Generic;
using DistantEngine.Graphics;

namespace DistantEngine.Objects.Components
{
    public class PhysicsComponent : IGoComponent
    {
        public TransformComponent Transform { get; set; }
        public Vector2D Velocity { get; set; } = new Vector2D(0, 0);
        public Vector2D Acceleration { get; set; } = new Vector2D(0, 0);

        private Vector2D _force = new Vector2D(0, 0);
        public float Mass { get; set; } = new float();
        
        private static Vector2D _gravity = new Vector2D(0, 9.8f);

        private DateTime _lastTime;

        public PhysicsComponent(GameObject obj)
        {
            Mass = 0;
            _lastTime = DateTime.Now;
            try
            {
                Transform = obj.GetComponent<TransformComponent>().Clone() as TransformComponent;
                obj.Table[typeof(TransformComponent)][1] = Transform;
            }
            catch (KeyNotFoundException e)
            {
                obj.AddComponent<TransformComponent>();
                Transform = obj.GetComponent<TransformComponent>();
            }
        }

        public void Update()
        {
            /*
            var currentTime = DateTime.Now;
            float timeDifference = Convert.ToSingle(System.Math.Abs(currentTime.Subtract(_lastTime).TotalSeconds));
            ApplyGravity();
            Velocity += Acceleration * timeDifference;
            Transform.Position += Velocity;
            _lastTime = currentTime;*/
        }

        public void Draw()
        {
            // No Draw
        }

        public void AddForce(Vector2D force)
        {
            Acceleration += force / Mass;
        }

        private void ApplyGravity()
        {
            Acceleration += _gravity;
        }
    }
}