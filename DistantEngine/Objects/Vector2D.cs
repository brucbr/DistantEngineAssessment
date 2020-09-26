namespace DistantEngine.Objects
{
    public class Vector2D
    {
        private float _x, _y;

        public float x
        {
            get => _x;
            set => _x = value;
        }

        public float y
        {
            get => _y;
            set => _y = value;
        }
        
        
        public Vector2D()
        {
            _x = 0.0f;
            _y = 0.0f;
        }

        public Vector2D(float x, float y)
        {
            this._x = x;
            this._x = y;
        }

        public void Zero()
        {
            this._x = 0;
            this._y = 0;
        }

        public Vector2D Add(Vector2D vector)
        {
            this._x += vector._x;
            this._y += vector._y;
            return this;
        }

        public Vector2D Subtract(Vector2D vector)
        {
            this._x -= vector._x;
            this._y -= vector._y;
            return this;
        }

        public Vector2D Multiply(Vector2D vector)
        {
            this._x *= vector._x;
            this._y *= vector._y;
            return this;
        }

        public Vector2D Divide(Vector2D vector)
        {
            this._x /= vector._x;
            this._y /= vector._y;
            return this;
        }

        public static Vector2D operator +(Vector2D vec1, Vector2D vec2)
        {
            return vec1.Add(vec2);
        }
        
        public static Vector2D operator -(Vector2D vec1, Vector2D vec2)
        {
            return vec1.Subtract(vec2);
        }
        
        public static Vector2D operator *(Vector2D vec1, Vector2D vec2)
        {
            return vec1.Multiply(vec2);
        }
        
        public static Vector2D operator /(Vector2D vec1, Vector2D vec2)
        {
            return vec1.Divide(vec2);
        }
    }
}