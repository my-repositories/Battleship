namespace cmd
{
    public class Enemy : Challenger
    {
        protected override string DoAttack(Challenger target)
        {
            do
            {
                var ch = (char) Constants.RandomGenerator.Next('A', 'A' + Constants.MapSize);
                var step = $"{ch}{Constants.RandomGenerator.Next(1, 1 + Constants.MapSize)}";

                var pair = ParseStep(step);
                if (target.Map[pair.Item1, pair.Item2] == Map.Ceil.Empty
                    || target.Map[pair.Item1, pair.Item2] == Map.Ceil.Ship)
                {
                    return step;
                }
            } while (true);
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
    }
}
