using System;
using System.Linq;

namespace cmd
{
    public enum Direction { Up, Right, Down, Left };

    public class Map
    {
        public enum Ceil { Destroyed = 'X', Empty = ' ', Injured = '-', Miss = '.', Ship = '@' };
        public int ShipsCount { get; private set; }
        public Ceil[,] Grid { get; }

        public Map()
        {
            Grid = new Ceil[Constants.MapSize, Constants.MapSize];
        }

        public Ceil this[int x, int y]
        {
            get { return Grid[x, y]; }
            set { Grid[x, y] = value; }
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

        public bool HandleStep(int x, int y)
        {
            if (Grid[x, y] == Ceil.Empty)
            {
                Grid[x, y] = Ceil.Miss;
                return false;
            }

            if (Grid[x, y] == Ceil.Ship)
            {
                MarkDiagonalsAsMiss(x, y);
                Grid[x, y] = Ceil.Injured;
                --ShipsCount;
            }

            if (CheckShipCompletelyDestroyed(x, y))
            {
                MarkShipsAsDestroyed(x, y);
            }

            return true;
        }

        // TODO: REMOVE IT AFTER RELEASE v.1
        public override string ToString()
        {
            var letters = Enumerable.Range('A', Constants.MapSize)
                .Select(ch => (char)ch)
                .ToArray();
            var title = string.Join(" ", letters);
            var result = $"\r\n    {title}";

            for (var x = 0; x < Constants.MapSize; ++x)
            {
                result += $"\r\n{1 + x} | " + (char)Grid[x, 0];
                for (var y = 1; y < Constants.MapSize; ++y)
                {
                    result += " " + (char)Grid[x, y];
                }
                result += " |";
            }
            return result;
        }

        private Tuple<int, int> CheckNearestCell(Ceil ceil, int x, int y)
        {
            // Проверка палубы слева
            if (x > 0 && Grid[x - 1, y] == ceil)
            {
                return Tuple.Create(x - 1, y);
            }

            // Проверка палубы справа
            if (x < Constants.MapSize - 1 && Grid[x + 1, y] == ceil)
            {
                return Tuple.Create(x + 1, y);
            }

            // Проверка палубы сверху
            if (y > 0 && Grid[x, y - 1] == ceil)
            {
                return Tuple.Create(x, y - 1);
            }

            // Проверка палубы снизу
            if (y < Constants.MapSize - 1 && Grid[x, y + 1] == ceil)
            {
                return Tuple.Create(x, y + 1);
            }

            return null;
        }

        private bool? CheckNeighborsByDirection(Direction direction, int x, int y, bool condition = false)
        {
            while (
                0 <= x && x < Constants.MapSize
                &&
                0 <= y && y < Constants.MapSize
            )
            {
                if (Grid[x, y] == Ceil.Ship)
                {
                    return false;
                }

                if (condition && Grid[x, y] != Ceil.Injured)
                {
                    return true;
                }

                switch (direction)
                {
                    case Direction.Up:
                        --x;
                        break;
                    case Direction.Right:
                        ++y;
                        break;
                    case Direction.Down:
                        ++x;
                        break;
                    case Direction.Left:
                        --y;
                        break;
                }
            }

            return null;
        }

        private bool CheckShipCompletelyDestroyed(int x, int y)
        {
            // если по указанным координатам (x;y) есть соседняя целая палуба
            // значит корабль еще НЕ полностью разрушен
            if (null != CheckNearestCell(Ceil.Ship, x, y))
            {
                return false;
            }

            var pair = CheckNearestCell(Ceil.Injured, x, y);
            if (null != pair)
            {
                Direction direction;
                var i = pair.Item1;
                var j = pair.Item2;

                if (x > i)
                {
                    direction = Direction.Up;
                } else if (y > j)
                {
                    direction = Direction.Left;
                }
                else if (y < j)
                {
                    direction = Direction.Right;
                }
                else
                {
                    direction = Direction.Down;
                }

                var result = CheckNeighborsByDirection(direction, x, y);
                if (result.HasValue)
                {
                    return (bool) result;
                }

                if (direction == Direction.Up)
                {
                    direction = Direction.Down;
                }
                else if (direction == Direction.Down)
                {
                    direction = Direction.Up;
                }
                else if (direction == Direction.Left)
                {
                    direction = Direction.Right;
                }
                else
                {
                    direction = Direction.Left;
                }

                result = CheckNeighborsByDirection(direction, x, y, true);
                if (null != result)
                {
                    return (bool) result;
                }
            }

            return true;
        }

        private void MarkDiagonalsAsMiss(int x, int y)
        {
            // заполнение соседней клетки сверху-слева
            if (x > 0 && y > 0)
            {
                Grid[x - 1, y - 1] = Ceil.Miss;
            }

            // заполнение соседней клетки снизу-справа
            if (x < Constants.MapSize - 1 && y < Constants.MapSize - 1)
            {
                Grid[x + 1, y + 1] = Ceil.Miss;
            }

            // заполнение соседней клетки снизу-слева
            if (x < Constants.MapSize - 1 && y > 0)
            {
                Grid[x + 1, y - 1] = Ceil.Miss;
            }

            // заполнение соседней клетки сверху-справа
            if (x > 0 && y < Constants.MapSize - 1)
            {
                Grid[x - 1, y + 1] = Ceil.Miss;
            }
        }

        private void MarkShip(int x, int y)
        {
            if (Grid[x, y] == Ceil.Injured)
            {
                MarkShipsAsDestroyed(x, y);
            }
            else if (Grid[x, y] == Ceil.Empty)
            {
                Grid[x, y] = Ceil.Miss;
            }
        }

        private void MarkShipsAsDestroyed(int x, int y)
        {
            Logger.Write("MarkShipsAsDestroyed" + x + y);
            Grid[x, y] = Ceil.Destroyed;

            if (x > 0)
            {
                MarkShip(x - 1, y);
            }

            if (y > 0)
            {
                MarkShip(x, y - 1);
            }

            if (Constants.MapSize > x + 1)
            {
                MarkShip(x + 1, y);
            }

            if (Constants.MapSize > y + 1)
            {
                MarkShip(x, y + 1);
            }
        }
    }
}
