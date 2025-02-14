namespace SnakeGame
{
    public struct Vector2Int
    {
        
        public int x { get; set; }
        public int y { get; set; }
        public Vector2Int(int xValue, int yValue)
        {
            x = xValue;
            y = yValue;
        }

        public static Vector2Int operator+(Vector2Int vec1, Vector2Int vec2)
        {
            return new Vector2Int(vec1.x + vec2.x, vec1.y + vec2.y);
        }

        public static Vector2Int operator -(Vector2Int vec1, Vector2Int vec2)
        {
            return new Vector2Int(vec1.x - vec2.x, vec1.y - vec2.y);
        }

        public static bool operator ==(Vector2Int vec1, Vector2Int vec2)
        {
            return vec1.x == vec2.x && vec1.y == vec2.y;
        }

        public static bool operator !=(Vector2Int vec1, Vector2Int vec2)
        {
            return !(vec1 == vec2);
        }

        public static Vector2Int operator *(Vector2Int vec1, Vector2Int vec2)
        {
            return new Vector2Int(vec1.x * vec2.x, vec1.y * vec2.y);
        }

        public static Vector2Int operator /(Vector2Int vec1, Vector2Int vec2)
        {
            return new Vector2Int(vec1.x / vec2.x, vec1.y / vec2.y);
        }

        public Vector2Int(float xValue, float yValue)
        {
            x = (int)xValue;
            y = (int)yValue;
        }

        public override readonly bool Equals(object? obj)
        {
            if (obj != null && obj.GetType() == GetType()) return this == (Vector2Int)obj;
            else return false;
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }
    }
}
