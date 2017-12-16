using System;

namespace cmd
{
    public class Game
    {
        private enum State { Draw, Loose, Win, NextStep }
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

        private void Display()
        {
            Console.Clear();

            Console.WriteLine("You:");
            _player.Render();
            Console.WriteLine("\r\nEnemy:");
            _enemy.Render();
        }

        private State GetGameState()
        {
            if (_player.ShipsCount == 0)
            {
                return _enemy.ShipsCount == 0 ? State.Draw : State.Loose;
            }

            if (_enemy.ShipsCount == 0)
            {
                return State.Win;
            }

            return State.NextStep;
        }

        private void MainLoop()
        {
            State gameState;
            while ((gameState = GetGameState()) == State.NextStep)
            {
                // TODO: refactoring ??
                // _player.Shoot(_enemy);
                // _enemy.Shoot(_player);
                Shoot(initiator: _player, target: _enemy);
                Shoot(initiator: _enemy, target: _player);
            }
            Display();
            Console.WriteLine(gameState);
        }

        private void Shoot(Challenger initiator, Challenger target)
        {
            bool hitting;
            do
            {
                Display();
                var step = initiator.Attack(target);
                hitting = target.HandleStep(step.Item1, step.Item2);
                Logger.Write(target, $"HandleStep <{step.Item1}, {step.Item2}>");
            } while (hitting && GetGameState() == State.NextStep);
        }
    }
}
