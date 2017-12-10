using System;

namespace cmd
{
    public class Game
    {
        private readonly Challenger _enemy;
        private readonly Challenger _player;

        public Game()
        {
            _enemy = new Challenger();
            _player = new Challenger();
        }

        public void Run()
        {
            Console.Clear();

            _enemy.InstallShips();
            _player.InstallShips();

            Console.WriteLine("You:");
            _player.Render();
            Console.WriteLine("\r\nEnemy:");
            _enemy.Render();
        }
    }
}
