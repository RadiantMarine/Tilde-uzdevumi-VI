using System;
using System.Collections.Generic;
using Uzdevums1.Enums;

namespace Uzdevums1.States
{
    public class PolygonState
    {
        // TODO: Consider allowing user to enter custom field size
        public readonly int SizeX = 20;
        public readonly int SizeY = 20;

        /// <summary>
        /// Shows the point direction on a polygon
        /// </summary>
        public DirectionEnum Direction { get; private set; }

        /// <summary>
        /// Holds a list of all points as an int-int tuple
        /// </summary>
        public List<Tuple<int, int>> Points { get; private set; }

        public PolygonState()
        {
            CleanFieldState();
        }

        /// <summary>
        /// Adds a new point to the Points list
        /// </summary>
        /// <param name="newPoint">value for the next point to add to the list</param>
        public void AddPoint(Tuple<int, int> newPoint)
        {
            Points.Add(newPoint);
        }

        /// <summary>
        /// Sets a new value for direction of the polygon
        /// </summary>
        /// <param name="direction">Value of the new polygon direction</param>
        public void SetDirection(DirectionEnum direction)
        {
            Direction = direction;
        }

        /// <summary>
        /// Sets field state to default values
        /// </summary>
        public void CleanFieldState()
        {
            Points = new List<Tuple<int, int>>();
            Direction = DirectionEnum.None;
        }
    }
}
