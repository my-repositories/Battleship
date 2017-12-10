using System;

namespace cmd
{
    public class Game
    {
        private readonly Challenger _enemy;
        private readonly Challenger _player;
        private bool _gameIsEnded;

        public Game()
        {
            _enemy = new Enemy();
            _player = new Player();
        }

        public void Run()
        {
            _gameIsEnded = false;
            _enemy.InstallShips();
            _player.InstallShips();

            MainLoop();
        }

        private void MainLoop()
        {
            while (!_gameIsEnded)
            {
                Console.Clear();

                Console.WriteLine("You:");
                _player.Render();
                Console.WriteLine("\r\nEnemy:");
                _enemy.Render();

                _player.Move();
                _enemy.Move();
                // TODO: REMOVE IT
                _gameIsEnded = true;
            }
        }
    }
}
