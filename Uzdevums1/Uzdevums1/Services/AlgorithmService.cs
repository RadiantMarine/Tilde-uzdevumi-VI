using Uzdevums1.Enums;
using Uzdevums1.States;

namespace Uzdevums1.Services
{
    /// <summary>
    /// Handles main business logic
    /// </summary>
    public class AlgorithmService
    {
        /// <summary>
        /// Gets the current direction of the points on a polygon
        /// </summary>
        /// <param name="polygonState">Used to find the current direction based on the points list</param>
        /// <returns>Current direction of the points on polygon</returns>
        public DirectionEnum GetCurrentDirection(PolygonState polygonState)
        {
            // (x2-x1)(y2+y1) from https://stackoverflow.com/a/1165943
            // Sum polygon edges. Positive sum = clockwise, negative sum = counterclockwise
            int edgesum = 0;
            for (var point = 0; point < polygonState.Points.Count - 1; point++)
            {
                edgesum += (polygonState.Points[point + 1].Item1 - polygonState.Points[point].Item1) * (polygonState.Points[point + 1].Item2 + polygonState.Points[point].Item2);
            }

            // here we close the polygon for formula to work
            edgesum += (polygonState.Points[0].Item1 - polygonState.Points[polygonState.Points.Count - 1].Item1) * (polygonState.Points[0].Item2 + polygonState.Points[polygonState.Points.Count - 1].Item2);

            if (edgesum > 0)
            {
                return DirectionEnum.Clockwise;
            }
            else if (edgesum < 0)
            {
                return DirectionEnum.CounterClockwise;
            }

            return DirectionEnum.None;
        }
    }
}
