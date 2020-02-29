using System;
using System.Collections.Generic;

namespace Uzdevums1
{
    public class FieldState
    {
        public readonly int SizeX = 20;
        public readonly int SizeY = 20;
        public DirectionEnum Direction = 0;

        public List<Tuple<int, int>> Field { get; private set; }

        public FieldState()
        {
            Field = new List<Tuple<int,int>>();
        }

        public void AddPoint(Tuple<int, int> newPoint)
        {
            Field.Add(newPoint);
        }
    }
}
