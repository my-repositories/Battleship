using System.Text.RegularExpressions;

namespace cmd
{
    public class Player : Challenger
    {
        private readonly Regex _stepPattern = new Regex(@"^[A-H][1-8]$");

        protected override string DoAttack(Challenger target)
        {
            System.Console.Write("Enter your step: ");
            var step = System.Console.ReadLine()?.ToUpper() ?? "-";

            while (!ValidateStep(step, target))
            {
                System.Console.WriteLine("Incorrect input!");
                System.Console.Write("Enter your step: ");
                step = System.Console.ReadLine()?.ToUpper() ?? "-";
            }

            return step;
        }

        protected override void DoRender()
        {
            for (var i = 0; i < Constants.MapSize; ++i)
            {
                System.Console.Write($"{1 + i} | ");
                for (var j = 0; j < Constants.MapSize; ++j)
                {
                    System.Console.Write((char)Map[i, j] + " ");
                }
                System.Console.WriteLine("|");
            }
        }

        protected override void InstallShips()
        {
            Map.InstallRandomly();
        }

        private bool ValidateStep(string step, Challenger target)
        {
            if (!_stepPattern.IsMatch(step))
            {
                Logger.Write(this, $"ValidateStep({step}): isn't match regex!");
                return false;
            }

            var pair = ParseStep(step);

            if (target.Map[pair.Item1, pair.Item2] == Map.Ceil.Empty
                || target.Map[pair.Item1, pair.Item2] == Map.Ceil.Ship)
            {
                return true;
            }

            Logger.Write(
                this,
                $"ValidateStep({step}): isn't valid"
                + $", cuz target.Map[{pair.Item1}, {pair.Item2}] == {target.Map[pair.Item1, pair.Item2]}"
            );
            return false;
        }
    }
}
