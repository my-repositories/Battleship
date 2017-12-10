namespace cmd
{
    public class Enemy : Challenger
    {
        // TODO: REMOVE THIS TEMP NUMERATOR
        private System.Collections.Generic.IEnumerator<string> _numerator;

        // TODO: REMOVE THIS TEMP GENERATOR
        private System.Collections.Generic.IEnumerable<string> TempStepGenerator()
        {
            for (var i = 'A'; i <= 'H'; ++i)
            {
                for (var j = 1; j <= 8; ++j)
                {
                    yield return $"{i}{j}";
                }
            }
        }

        protected override string DoAttack(Challenger target)
        {
            // TODO: REMOVE THIS TEMP GENERATOR
            return _numerator.MoveNext() ? _numerator.Current : "A2";
        }

        protected override void DoRender()
        {
            for (var i = 0; i < Constants.MapSize; ++i)
            {
                System.Console.Write($"{1 + i} | ");
                for (var j = 0; j < Constants.MapSize; ++j)
                {
                    System.Console.Write(
                        (Map.Ceil.Ship == Map[i, j]
                        ? (char)Map.Ceil.Empty
                        : (char)Map[i, j])
                        + " "
                    );
                }
                System.Console.WriteLine("|");
            }
        }

        protected override void InstallShips()
        {
            // TODO: REMOVE THIS TEMP NUMERATOR
            _numerator = TempStepGenerator().GetEnumerator();
            Map.InstallRandomly();
            Logger.Write(this, $"InstallShips: {Map}");
        }
    }
}
