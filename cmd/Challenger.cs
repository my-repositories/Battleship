using System;
using System.Linq;

namespace cmd
{
    public abstract class Challenger
    {
        protected readonly Map Map;

        protected Challenger()
        {
            Map = new Map();
        }

        public abstract void InstallShips();
        protected abstract void DoMove();
        protected abstract void DoRender();

        public void Move()
        {
            Logger.Write(GetType().Name + "::Move()");
            DoMove();
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
