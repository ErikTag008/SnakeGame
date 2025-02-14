namespace SnakeGame
{
    public static class Input
    {
        private static Vector2Int _currentDirection = new(1, 0);

        public static Vector2Int CurrentKeyDirection()
        {
            if (Console.KeyAvailable) // ✅ Check if a key is available
            {
                var consoleKey = Console.ReadKey(intercept: true); // ✅ Read key without displaying

                if (consoleKey.Key == ConsoleKey.W || consoleKey.Key == ConsoleKey.UpArrow) _currentDirection = new Vector2Int(0, 1);
                else if (consoleKey.Key == ConsoleKey.D || consoleKey.Key == ConsoleKey.RightArrow) _currentDirection = new Vector2Int(1, 0);
                else if (consoleKey.Key == ConsoleKey.S || consoleKey.Key == ConsoleKey.DownArrow) _currentDirection = new Vector2Int(0, -1);
                else if (consoleKey.Key == ConsoleKey.A || consoleKey.Key == ConsoleKey.LeftArrow) _currentDirection = new Vector2Int(-1, 0);
            }

            return _currentDirection; // ✅ Keeps moving even if no key is pressed
        }
    }
}
