

namespace SnakeGame
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int gridSizeX, gridSizeY, updateDelay;

            Console.Write("If you want to play with the default values input [def], if not then input anything else: ");
            string? starter = Console.ReadLine();
            if(starter == null || starter.ToLower() == "def")
            {
                gridSizeX = 40;
                gridSizeY = 20;
                updateDelay = 150;
            }
            else
            {
                do
                {
                    Console.Write("Input the size of the grid [3 < X < 100, 3 < Y < 28]: ");
                    string[] input = Console.ReadLine().Split(',');

                    if (input.Length == 2 && int.TryParse(input[0], out gridSizeX) && int.TryParse(input[1], out gridSizeY))
                    {
                        if (gridSizeX >= 3 && gridSizeX <= 100 && gridSizeY >= 3 && gridSizeY <= 30)
                            break;
                    }
                    Console.WriteLine("Invalid input. Please enter two numbers separated by a comma (e.g., 10,10).");
                } while (true);

                do
                {
                    Console.Write("Input the update delay [50; 2000] in milliseconds: ");
                    if (int.TryParse(Console.ReadLine(), out updateDelay) && updateDelay >= 50 && updateDelay <= 2000)
                        break;

                    Console.WriteLine("Invalid input. Please enter a number between 50 and 2000.");
                } while (true);
            }
            
            Console.Clear();
            GameEngine gameEngine = new GameEngine(gridSizeX, gridSizeY, updateDelay, 5);

            await Task.Delay(Timeout.Infinite);
                
        }
    }
}
