using System;
using System.Linq;

namespace cmd
{
    public class Challenger
    {
        private readonly Map _map;

        public Challenger()
        {
            _map = new Map();
        }

        public void InstallShips()
        {
            _map.InstallRandomly();
        }

        public void Render()
        {
            var letters = Enumerable.Range('A', Constants.MapSize)
                .Select(x => (char)x)
                .ToArray();
            var title = string.Join(" ", letters);
            Console.WriteLine($"    {title}");

            for (var i = 0; i < Constants.MapSize; ++i)
            {
                Console.Write($"{1 + i} | ");
                for (var j = 0; j < Constants.MapSize; ++j)
                {
                    Console.Write((char)_map[i, j] + " ");
                }
                Console.WriteLine("|");
            }
        }
    }
}
