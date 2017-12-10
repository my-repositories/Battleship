using System;
using System.Linq;

namespace cmd
{
    public abstract class Challenger
    {
        public int ShipsCount => Map.ShipsCount;
        protected internal readonly Map Map;

        protected Challenger()
        {
            Map = new Map();
        }

        protected abstract string DoAttack(Challenger target);
        protected abstract void DoRender();
        protected abstract void InstallShips();

        public void HandleStep(int i, int j) => Map.HandleStep(i, j);

        public void Init()
        {
            Map.Reset();
            InstallShips();
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

        protected Tuple<int, int> ParseStep(string step)
        {
            var pair = step.ToCharArray(0, 2);
            return Tuple.Create(pair[1] - '1', pair[0] - 'A');
        }
    }
}
