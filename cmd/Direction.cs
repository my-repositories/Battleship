namespace cmd
{
    public enum Direction { Up, Right, Down, Left };

    public static class DirectionMethods
    {
        public static Direction GetDirectionByPoints(int x0, int y0, int x, int y)
        {
            if (x0 > x)
            {
                return Direction.Up;
            }

            if (y0 > y)
            {
                return Direction.Left;
            }

            if (y0 < y)
            {
                return Direction.Right;
            }

            return Direction.Down;
        }

        public static Direction GetRandomDirection()
        {
            return (Direction)Constants.RandomGenerator.Next(0, 3);
        }

        /// <summary>
        /// Инвертирует направление: Up -> Down, Right -> Left, etc... 
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Direction Invert(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return Direction.Down;
                case Direction.Right: return Direction.Left;
                case Direction.Down: return Direction.Up;
                case Direction.Left: return Direction.Right;
                default: return Direction.Down;
            }
        }

        public static int Move(this Direction direction, ref int x, ref int y)
        {
            switch (direction)
            {
                case Direction.Up: return --y;
                case Direction.Right: return ++x;
                case Direction.Down: return ++y;
                case Direction.Left: return --x;
                default: return -1;
            }
        }
    }
}
