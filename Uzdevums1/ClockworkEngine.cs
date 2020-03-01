using Uzdevums1.Enums;
using Uzdevums1.Services;
using Uzdevums1.States;

namespace Uzdevums1
{
    /// <summary>
    /// Responsible for running the main loop and orchestrating the app
    /// </summary>
    public class ClockworkEngine
    {
        private readonly InputOutputService iOService;
        private readonly AlgorithmService algorithmService;
        private readonly PolygonState fieldState;

        public ClockworkEngine()
        {
            iOService = new InputOutputService();
            algorithmService = new AlgorithmService();
            fieldState = new PolygonState();
        }

        /// <summary>
        /// Runs the main app loop and handles actions chosen by the user 
        /// </summary>
        public void Run()
        {
            iOService.GreetingMessage();

            bool continueClockwork = true;
            do
            {
                var nextAction = iOService.GetNextAction(fieldState);
                switch (nextAction)
                {
                    case ActionEnum.Exit:
                        continueClockwork = false;
                        break;
                    case ActionEnum.AddNextPoint:
                        var newPoint = iOService.GetNewPoint(fieldState);
                        fieldState.AddPoint(newPoint);
                        fieldState.SetDirection(algorithmService.GetCurrentDirection(fieldState));
                        break;
                    case ActionEnum.ShowPoints:
                        // TODO: Implemet field display
                        break;
                    case ActionEnum.Restart:
                        fieldState.CleanFieldState();
                        break;
                    default:
                        break;
                }
            } while (continueClockwork);
        }
    }
}
