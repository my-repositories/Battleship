using System.Collections.Generic;
using System.Linq;

namespace cmd
{
    internal static class Constants
    {
        /// <summary>
        /// Размер игрового поля
        /// </summary>
        public const int MapSize = 8;
        public static readonly System.Random RandomGenerator = new System.Random();
        public static int ShipsCount => ShipsSettings.Select(x => x.Key * x.Value).Sum();

        /// <summary>
        /// Настройки для кораблей:
        /// первое число - длина корабля в палубах
        /// второе число - количество таких кораблей
        /// Например запись {3, 2} означает, что можно создать два трёхпалубных корабля
        /// </summary>
        public static readonly Dictionary<int, int> ShipsSettings = new Dictionary<int, int> {
            { 4, 1 },
            { 3, 2 },
            { 2, 2 },
            { 1, 2 }
            
        };
    }
}
