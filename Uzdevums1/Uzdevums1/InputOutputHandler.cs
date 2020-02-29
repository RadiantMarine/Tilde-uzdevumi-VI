using System;
using System.Collections.Generic;
using System.Globalization;

namespace Uzdevums1
{
    public class InputOutputHandler
    {
        readonly char[] separator = { ',' };

        public void GreetingMessage()
        {
            Console.WriteLine("Greetings to the clockwork apparatus!");
            Console.WriteLine("Initializing...");
        }

        public ActionEnum GetNextAction(FieldState field)
        {
            // TODO: Remove magic strings from command letters
            ShowFieldState(field);
            Console.WriteLine("To enter new point, press N");
            Console.WriteLine("To view field, press S");
            Console.WriteLine("To exit, press Esc");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.Escape:
                    return ActionEnum.Escape;
                case ConsoleKey.N:
                    return ActionEnum.Next;
                case ConsoleKey.S:
                    return ActionEnum.ShowPoints;
                default:
                    return ActionEnum.Continue;
            }
        }

        public void ShowPoints(FieldState field)
        {
            ShowFieldState(field);
            for (var y = 0; y < field.SizeY; y++)
            {
                for (var x = 0; x < field.SizeX; x++)
                {
                    var target = 0;//field.Field[x, y];
                    /*if (target > 0 )
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{target} ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write($"{target} ");
                    }*/
                }
                Console.Write("\n");
            }
            Console.ReadLine();
        }

        public Tuple<int, int> GetNewPoint()
        {
            int coordinateX;
            int coordinateY;
            CultureInfo culture = CultureInfo.InvariantCulture;
            NumberStyles styles = NumberStyles.AllowLeadingWhite;

            // TODO: Find a better way to show that result hasn't been initiated.
            var result = new Tuple<int, int>(-1, -1);

            do
            {
                Console.Clear();
                Console.WriteLine($"Enter coordinates for the point. Use \"{separator[0]}\" as a separator");
                var response = Console.ReadLine();
                var coordinates = new List<string>(response.Split(separator));
                if (int.TryParse(coordinates[0], styles, culture, out coordinateX) && int.TryParse(coordinates[1], styles, culture, out coordinateY))
                {
                    result = new Tuple<int, int>(coordinateX, coordinateY);
                }
            } while (result.Item1 < 0 || result.Item2 < 0);

            return result;
        }

        private void ShowFieldState(FieldState field)
        {
            Console.Clear();

            Console.WriteLine($"Field size is {field.SizeX} by {field.SizeY}.");
            Console.WriteLine($"Currently entered {field.Field.Count} points.");
            Console.WriteLine($"Current direction: {field.Direction}");
        }
    }
}
