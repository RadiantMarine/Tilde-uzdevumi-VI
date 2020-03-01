using System;
using System.Collections.Generic;
using System.Globalization;
using Uzdevums1.Enums;
using Uzdevums1.States;

namespace Uzdevums1.Services
{
    /// <summary>
    /// Used to display messages to the user and to parse their responses
    /// </summary>
    public class InputOutputService
    {
        /// <summary>
        /// Used to separate X and Y positions of a new point
        /// </summary>
        readonly char[] separators = { ',' };

        /// <summary>
        /// Displays greeting message if the process is slow enough.
        /// </summary>
        public void GreetingMessage()
        {
            Console.WriteLine("Greetings to the clockwork apparatus!");
            Console.WriteLine("Initializing...");
        }

        /// <summary>
        /// Gets next action from user and then returns it as an ActionEnum
        /// </summary>
        /// <param name="fieldState">Used to call ShowFieldState</param>
        /// <returns>Selected user action parsed to an ActionEnum result</returns>
        public ActionEnum GetNextAction(PolygonState fieldState)
        {
            // TODO: Remove magic strings from command letters
            ShowFieldState(fieldState);
            Console.WriteLine("To enter new point, press N");
            Console.WriteLine("To exit, press Esc");
            Console.WriteLine("To restart press R");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.Escape:
                    return ActionEnum.Exit;
                case ConsoleKey.N:
                    return ActionEnum.AddNextPoint;
                case ConsoleKey.R:
                    return ActionEnum.Restart;
                default:
                    return ActionEnum.Continue;
            }
        }

        /// <summary>
        /// Requests new point from the user then parses it into an int-int tuple 
        /// </summary>
        /// <param name="fieldState">Used to call ShowFieldState</param>
        /// <returns>New point parsed into an int-int tuple</returns>
        public Tuple<int, int> GetNewPoint(PolygonState fieldState)
        {
            ShowFieldState(fieldState);

            // TODO: Find a better way to show that result hasn't been initiated.
            var result = new Tuple<int, int>(-1, -1);

            do
            {
                int coordinateX;
                int coordinateY;
                CultureInfo culture = CultureInfo.InvariantCulture;
                NumberStyles styles = NumberStyles.AllowLeadingWhite;

                Console.Clear();
                Console.WriteLine($"Enter coordinates for the point. Please use \"{separators[0]}\" as a separator.");

                var response = Console.ReadLine();
                var coordinates = new List<string>(response.Split(separators));

                // TODO: Consider what to do if the point is not unique.
                if (int.TryParse(coordinates[0], styles, culture, out coordinateX) && int.TryParse(coordinates[1], styles, culture, out coordinateY))
                {
                    result = new Tuple<int, int>(coordinateX, coordinateY);
                }
            } while (result.Item1 < 0 || result.Item2 < 0);

            return result;
        }

        /// <summary>
        /// Shows algorithm metadata
        /// </summary>
        /// <param name="fieldState">Used to get field state metadata</param>
        private void ShowFieldState(PolygonState fieldState)
        {
            Console.Clear();

            Console.WriteLine($"Field size is {fieldState.SizeX} by {fieldState.SizeY}.");
            Console.WriteLine($"Currently entered {fieldState.Points.Count} points.");
            Console.WriteLine($"Current direction: {fieldState.Direction}.\n");
        }
    }
}
