using System;

namespace cmd
{
    public class Game
    {
        private readonly Challenger _enemy;
        private readonly Challenger _player;

        public Game()
        {
            _enemy = new Enemy();
            _player = new Player();
        }

        public void Run()
        {
            string answer;
            do
            {
                _enemy.Init();
                _player.Init();

                MainLoop();
                Console.Write("Do you want play again ? y/n: ");
                answer = Console.ReadLine()?.Trim().ToLower() ?? "y";

            } while (answer != "n");
        }

        private bool GameOver()
        {
            if (_player.ShipsCount == 0)
            {
                Console.WriteLine(_enemy.ShipsCount == 0 ? "Draw" : "You loose!");
                return true;
            }

            if (_enemy.ShipsCount == 0)
            {
                Console.WriteLine("You win!");
                return true;
            }

            return false;
        }

        private void MainLoop()
        {
            while (!GameOver())
            {
                Console.Clear();

                Console.WriteLine("You:");
                _player.Render();
                Console.WriteLine("\r\nEnemy:");
                _enemy.Render();

                // TODO: refactoring ??
                // _player.Shoot(_enemy);
                // _enemy.Shoot(_player);
                Shoot(initiator: _player, target: _enemy);
                Shoot(initiator: _enemy, target: _player);
            }
        }

        private void Shoot(Challenger initiator, Challenger target)
        {
            var step = initiator.Attack(target);
            target.HandleStep(step.Item1, step.Item2);
            Logger.Write(target, $"HandleStep <{step.Item1}, {step.Item2}>");
        }
    }
}
