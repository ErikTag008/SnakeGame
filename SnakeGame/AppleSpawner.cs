

namespace SnakeGame
{
    public static class AppleSpawner
    {
        public static Vector2Int CurrentSpawnedApple { get; private set; }
        

        public static void OnAppleEaten(Snake snake, Vector2Int gridSize)
        {
            var newApplePos = snake.GetSnakeBodyPositions()[0];
            while (snake.GetSnakeBodyPositions().Contains(newApplePos))
            {
                Random _rand = new Random(Environment.TickCount);
                newApplePos = new Vector2Int(_rand.Next(0, gridSize.x), _rand.Next(0, gridSize.y));
            }
            CurrentSpawnedApple = newApplePos;
        }
    }
}
