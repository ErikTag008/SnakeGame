namespace SnakeGame
{
    public static class ConsoleActions
    {
        public static void SetCursorToStart()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("\x1b[3J"); // ANSI escape sequence to clear the entire scrollback buffer
            Console.SetCursorPosition(0, 0);
        }
    }
}
