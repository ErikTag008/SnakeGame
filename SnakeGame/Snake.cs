namespace SnakeGame
{
    public class Snake
    {
        private readonly List<Vector2Int> _snakeBody = new();
        private Vector2Int _gridSize;
        public bool IsDead { get; private set; }
          
        public Snake(int startingLength, Vector2Int gridCenter, Vector2Int gridSize)
        {
            _gridSize = gridSize;
            InitializeSnake(startingLength, gridCenter, gridSize);
        }

        private void InitializeSnake(int length, Vector2Int center, Vector2Int bounds)
        {
            for(int i = 0; i < length; i++)
            {
                var xValue = center.x - i >= 0 ? center.x - i : center.x - i + bounds.x;
                _snakeBody.Add(new Vector2Int(xValue, center.y));
                
            }
        }

        public void MoveSnake(Vector2Int direction)
        {
            for(int i = _snakeBody.Count - 1; i > 0; i--)
            {
                _snakeBody[i] = _snakeBody[i - 1];
            }
            Vector2Int newHeadPosition = _snakeBody[0] + direction;
            if (_snakeBody[0].x + direction.x >= _gridSize.x) newHeadPosition.x = 0;
            else if (_snakeBody[0].x + direction.x < 0) newHeadPosition.x = _gridSize.x;
            if (_snakeBody[0].y + direction.y >= _gridSize.y) newHeadPosition.y = 0;
            else if (_snakeBody[0].y + direction.y < 0) newHeadPosition.y = _gridSize.y;
            _snakeBody[0] = newHeadPosition;
            if (_snakeBody[0] == AppleSpawner.CurrentSpawnedApple)
            {
                EatApple();
            }
            for (int i = 1;i < _snakeBody.Count; i++)
            {
                if (_snakeBody[0] == _snakeBody[i])
                {
                    IsDead = true;
                }
            }
            
            
        }

        private void EatApple()
        {
            AppleSpawner.OnAppleEaten(this, _gridSize);
            _snakeBody.Add(_snakeBody[_snakeBody.Count - 1]);
        }

        public Vector2Int[] GetSnakeBodyPositions()
        {
            return _snakeBody.ToArray();
        }


    }
}
