using SlidingPuzzleEngine;
using System.Runtime.CompilerServices;

internal class Program
{
    private static Dictionary<Position, int> positionToNumber = new Dictionary<Position, int>();
    private static void Main(string[] args)
    {
        Game game = new Game(3, 3);

        PopulateViewCollection(game);
        bool gameIsSolved = game.GameIsSolved();

        while(!gameIsSolved)
        {
            DrawBoard(game);
            int input = ReadInput();

            KeyValuePair<Position, int> pair = positionToNumber.FirstOrDefault((pair) => { return pair.Value == input; });
            var position = pair.Key;
            Slider slider = game.Sliders.First((sl) => {return sl.DesiredPosition == position;});

            if(slider != null)
            {
                bool success = slider.TryMove(out SlideDirection direction);
            }

            gameIsSolved = game.GameIsSolved();
        } 

        DrawBoard(game);
        Console.WriteLine("Game solved! Press any key to exit");
        Console.ReadLine();
    }

    private static int ReadInput()
    {
        bool inputRead = false;
        int result = 0;

        do
        {
            Console.WriteLine("Enter a number to slide.");
            string? input = Console.ReadLine();

            if(input != null)
            {
                inputRead = Int32.TryParse(input, out result);
            }
        }
        while (!inputRead);

        return result;
    }

    private static void DrawBoard(Game game)
    {
        for (int y = 0; y < game.Width; y++)
        {
            Console.Write(" ___ ");
        }

        Console.WriteLine();

        for (int y = 0; y < game.Height; y++)
        {
            for (int x = 0; x < game.Width; x++)
            {
                Slider currentSlider;
                bool success = game.TryGetSliderFromActualPosition(new Position(x, y), out currentSlider);
                string sliderView = " ";

                if (success)
                {
                    int value;
                    if (positionToNumber.TryGetValue(currentSlider.DesiredPosition, out value))
                    {
                        sliderView = value.ToString();
                    }
                }

                Console.Write(string.Format("|_{0}_|", sliderView));
            }

            Console.WriteLine();
        }
    }

    private static void PopulateViewCollection(Game game)
    {
        int counter = 1;
        for (int y = 0; y < game.Height; y++)
        {
            for (int x = 0; x < game.Width; x++)
            {
                positionToNumber.Add(new Position(x, y), counter);
                counter++;
            }
        }

        positionToNumber.Remove(new Position(game.Width, game.Height));
    }
}