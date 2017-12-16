using System;
using System.Linq;

namespace cmd
{
    public abstract class Challenger
    {
        public int ShipsCount => Map.ShipsCount;
        protected internal readonly Map Map = new Map();

        protected abstract string DoAttack(Challenger target);
        protected abstract void DoRender();

        public bool HandleStep(int x, int y) => Map.HandleStep(x, y);

        public void Init()
        {
            Map.Reset();
            foreach (var option in Constants.ShipsSettings)
            {
                InstallShips(size: option.Key, count: option.Value);
            }
            Logger.Write(this, $"InstallShips: {Map}");
        }

        public Tuple<int, int> Attack(Challenger target)
        {
            var step = DoAttack(target);
            Logger.Write(this, $"Shoot <{step}>");

            return ParseStep(step);
        }

        public void Render()
        {
            var letters = Enumerable.Range('A', Constants.MapSize)
                .Select(x => (char)x)
                .ToArray();
            var title = string.Join(" ", letters);
            Console.WriteLine($"    {title}");

            DoRender();
        }

        protected bool CanInstallShip(Direction direction, int size, int x, int y)
        {
            for (var i = 0; i < size; ++i)
            {
                // проверка на выход за пределы карты
                if (x < 0 || y < 0 || x >= Constants.MapSize || y >= Constants.MapSize)
                {
                    return false;
                }

                // проверка на соприкосновение с другими палубами
                if (
                    Map[x, y] == Map.Ceil.Ship
                    || (Constants.MapSize > y + 1 && Map[x, y + 1] == Map.Ceil.Ship)
                    || (y > 0 && Map[x, y - 1] == Map.Ceil.Ship)
                    || (Constants.MapSize > x + 1 && Map[x + 1, y] == Map.Ceil.Ship)
                    || (Constants.MapSize > x + 1 && Constants.MapSize > y + 1 && Map[x + 1, y + 1] == Map.Ceil.Ship)
                    || (Constants.MapSize > x + 1 && y > 0 && Map[x + 1, y - 1] == Map.Ceil.Ship)
                    || (x > 0 && Map[x - 1, y] == Map.Ceil.Ship)
                    || (x > 0 && Constants.MapSize > y + 1 && Map[x - 1, y + 1] == Map.Ceil.Ship)
                    || (x > 0 && y > 0 && Map[x - 1, y - 1] == Map.Ceil.Ship)
                    )
                {
                    return false;
                }

                direction.Move(ref x, ref y);
            }
            return true;
        }

        protected void InstallShip(Direction direction, int size, int x, int y)
        {
            for (var i = 0; i < size; ++i)
            {
                Map[x, y] = Map.Ceil.Ship;
                direction.Move(ref x, ref y);
            }
        }

        protected virtual void InstallShips(int count, int size)
        {
            for (int counter = 0, x, y; counter < count;)
            {
                x = Constants.RandomGenerator.Next(0, Constants.MapSize);
                y = Constants.RandomGenerator.Next(0, Constants.MapSize);
                var direction = DirectionMethods.GetRandomDirection();

                if (CanInstallShip(direction, size, x, y))
                {
                    InstallShip(direction, size, x, y);
                    ++counter;
                }
            }
        }

        protected Tuple<int, int> ParseStep(string step)
        {
            var pair = step.ToCharArray(0, 2);
            return Tuple.Create(pair[1] - '1', pair[0] - 'A');
        }
    }
}
