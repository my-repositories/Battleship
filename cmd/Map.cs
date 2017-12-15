using System.Linq;

namespace cmd
{
    public class Map
    {
        public enum Ceil { Destroyed = 'X', Empty = ' ', Injured = '-', Miss = '.', Ship = '@' };
        public int ShipsCount { get; private set; }
        public Ceil[,] Grid { get; }

        public Map()
        {
            Grid = new Ceil[Constants.MapSize, Constants.MapSize];
        }

        public Ceil this[int i, int j]
        {
            get { return Grid[i, j]; }
            set { Grid[i, j] = value; }
        }

        public void Reset()
        {
            ShipsCount = Constants.ShipsCount;

            for (var i = 0; i < Constants.MapSize; ++i)
            {
                for (var j = 0; j < Constants.MapSize; ++j)
                {
                    Grid[i, j] = Ceil.Empty;
                }
            }
        }

        public bool HandleStep(int i, int j)
        {
            if (Grid[i, j] == Ceil.Empty)
            {
                Grid[i, j] = Ceil.Miss;
                return false;
            }

            if (Grid[i, j] == Ceil.Ship)
            {
                // TODO: добавить Ceil.Miss соседним клеткам по диагоналям
                Grid[i, j] = Ceil.Injured;
                --ShipsCount;
            }

            // TODO: Добавить проверку - полностью ли корабль разрушен?
            // Если да то заменить все Ceil.Injured на Ceil.Destroyed
            // а так же заменить соседние Ceil.Empty на Ceil.Miss

            return true;
        }

        // TODO: REMOVE IT AFTER RELEASE v.1
        public override string ToString()
        {
            var letters = Enumerable.Range('A', Constants.MapSize)
                .Select(x => (char)x)
                .ToArray();
            var title = string.Join(" ", letters);
            var result = $"\r\n    {title}";

            for (var i = 0; i < Constants.MapSize; ++i)
            {
                result += $"\r\n{1 + i} | " + (char)Grid[i, 0];
                for (var j = 1; j < Constants.MapSize; ++j)
                {
                    result += " " + (char)Grid[i, j];
                }
                result += " |";
            }
            return result;
        }
    }
}
