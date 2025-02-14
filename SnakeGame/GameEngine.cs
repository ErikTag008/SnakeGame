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
            
            HashSet<(int, int)> snakePositions = [];
            foreach (var pos in snakeBodyPositions)
            {
                snakePositions.Add((pos.x, pos.y));
            }
            var applePos = AppleSpawner.CurrentSpawnedApple;
            // Use StringBuilder to construct the grid in memory
            StringBuilder output = new();

            for (int i = _height - 1; i >= 0; i--)
            {
                for (int j = 0; j < _width; j++)
                {

                    if (snakePositions.Contains((j, i)))
                    {
                        output.Append("[G]*"); // Token for green snake body
                    }
                    else if (applePos == new Vector2Int(j, i))
                    {
                        output.Append("[R]@"); // Token for red apple
                    }
                    else
                    {
                        output.Append("[W]#"); // Token for white empty space
                    }

                }
               
                output.AppendLine();
            }
            string grid = output.ToString();

            // Replace tokens with corresponding console color codes
            grid = grid.Replace("[G]", "\x1b[32m") // Green
                       .Replace("[R]", "\x1b[31m") // Red
                       .Replace("[W]", "\x1b[37m") // White
                       + "\x1b[0m"; // Reset color at the end of the grid

            // Output the final string with colors
            ConsoleActions.SetCursorToStart();
            Console.Write(grid);

        }

        
    }
}
