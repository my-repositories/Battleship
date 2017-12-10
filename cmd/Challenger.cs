using System;
using System.Linq;

namespace cmd
{
    public abstract class Challenger
    {
        public int ShipsCount => Map.ShipsCount;
        protected readonly Map Map;

        protected Challenger()
        {
            Map = new Map();
        }

        protected abstract string DoMove();
        protected abstract void DoRender();
        protected abstract void InstallShips();

        public void HandleStep(int i, int j) => Map.HandleStep(i, j);

        public void Init()
        {
            Map.Reset();
            InstallShips();
        }

        public Tuple<int, int> Move()
        {
            var step = DoMove();
            Logger.Write(GetType().Name + ".Move to: " + step);

            var pair = step.ToUpper().ToCharArray(0, 2);
            return Tuple.Create(pair[1] - '1', pair[0] - 'A');
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
    }
}
