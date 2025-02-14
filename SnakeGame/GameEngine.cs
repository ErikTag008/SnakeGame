using System.Text;

namespace SnakeGame
{
    public class GameEngine
    {
        private int _width = 10;
        private int _height = 10;
        private int _updateDelay = 1000;
        private readonly CancellationTokenSource _cts = new();

        public GameEngine(int width, int height, int updateDelayInMs, int startingSnakeLength)
        {
            _width = width;
            _height = height;
            _updateDelay = updateDelayInMs;
            Snake snake = new(startingSnakeLength, new Vector2Int((int)(0.5f * width), (int)(0.5f * height)), new Vector2Int(width, height));
            AppleSpawner.OnAppleEaten(snake, new Vector2Int(width, height));
            Console.CursorVisible = false;
            Task.Run(() => StartGame(snake, _cts.Token));
        }

        ~GameEngine()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        private async Task StartGame(Snake snake, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(_updateDelay, cancellationToken: token);
                snake.MoveSnake(Input.CurrentKeyDirection());
                if (snake.IsDead)
                {
                    Console.Clear();
                    Console.WriteLine("Game Over!");
                    return;
                } 
                GenerateGrid(snake.GetSnakeBodyPositions());
            }
        }

        private void GenerateGrid(Vector2Int[] snakeBodyPositions)
        {
            HashSet<(int, int)> snakePositions = new();
            foreach (var pos in snakeBodyPositions)
            {
                snakePositions.Add((pos.x, pos.y));
            }
            var applePos = AppleSpawner.CurrentSpawnedApple;

            // Use StringBuilder to construct the grid in memory
            StringBuilder output = new();

              // Windows Terminal

            for (int i = _height - 1; i >= 0; i--)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (snakePositions.Contains((j, i)))
                    {
                        output.Append('*'); // Fallback to plain text
                    }
                    else if (applePos == new Vector2Int(j, i))
                    {
                        output.Append('@'); // Fallback to plain text
                    }
                    else
                    {
                        output.Append('#'); // Fallback to plain text
                    }
                }
                output.AppendLine();
            }

            string grid = output.ToString();
            ConsoleActions.SetCursorToStart();
            foreach (var ch in grid)
            {
                if (ch == '*')
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (ch == '@')
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (ch == '#')
                    Console.ForegroundColor = ConsoleColor.White;

                Console.Write(ch);
            }
            Console.ResetColor();


        }
    }
}
