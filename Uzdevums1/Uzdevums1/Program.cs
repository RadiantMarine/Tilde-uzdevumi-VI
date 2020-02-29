using System;

namespace Uzdevums1
{
    class Program
    {
        static void Main(string[] args)
        {
            InputOutputHandler iOHandler = new InputOutputHandler();
            FieldState field = new FieldState();
            iOHandler.GreetingMessage();

            bool continueClockwork = true;
            do
            {
                var nextAction = iOHandler.GetNextAction(field);
                switch (nextAction)
                {
                    case ActionEnum.Escape:
                        continueClockwork = false;
                        break;
                    case ActionEnum.Next:
                        var newPoint = iOHandler.GetNewPoint();
                        field.AddPoint(newPoint);
                        field.Direction = GetNewDirection(field);
                        break;
                    case ActionEnum.ShowPoints:
                        iOHandler.ShowPoints(field);
                        break;
                    default:
                        break;
                }
            } while (continueClockwork);
        }

        private static DirectionEnum GetNewDirection(FieldState field)
        {
            var resultDirection = DirectionEnum.None;

            // (x2-x1)(y2+y1) from https://stackoverflow.com/a/1165943
            // Sum edges. Positive sum = clockwise, negative sum = counter
            if (field.Field.Count > 1)
            {
                int edgesum = 0;
                for (var point = 0; point < field.Field.Count - 1; point++)
                {
                    edgesum += (field.Field[point + 1].Item1 - field.Field[point].Item1) * (field.Field[point + 1].Item2 + field.Field[point].Item2);
                }

                // here we close the polygon for formula to work
                edgesum += (field.Field[0].Item1 - field.Field[field.Field.Count - 1].Item1) * (field.Field[0].Item2 + field.Field[field.Field.Count - 1].Item2);

                if (edgesum > 0)
                {
                    resultDirection = DirectionEnum.Clockwise;
                }
                else if(edgesum < 0)
                {
                    resultDirection = DirectionEnum.CounterClockwise;
                }
            }

            return resultDirection;
        }
    }
}
