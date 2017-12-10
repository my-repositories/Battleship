namespace cmd
{
    public class Map
    {
        public enum Ceil { Empty = ' ', Ship = '@', Miss = '.' };

        public Ceil[,] Grid { get; }

        public Map()
        {
            Grid = new Ceil[Constants.MapSize, Constants.MapSize];
            Clear();
        }

        public Ceil this[int i, int j] => Grid[i, j];

        public void Clear()
        {
            for (var i = 0; i < Constants.MapSize; ++i)
            {
                for (var j = 0; j < Constants.MapSize; ++j)
                {
                    Grid[i, j] = Ceil.Empty;
                }
            }
        }

        public void InstallRandomly()
        {
            for (int counter = 0, x, y; counter < Constants.ShipsCount; )
            {
                x = Constants.RandomGenerator.Next(0, Constants.MapSize);
                y = Constants.RandomGenerator.Next(0, Constants.MapSize);

                if (Grid[x, y] == Ceil.Empty)
                {
                    Grid[x, y] = Ceil.Ship;
                    ++counter;
                }
            }
        }
    }
}
