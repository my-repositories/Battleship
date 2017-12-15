namespace cmd
{
    public class Enemy : Challenger
    {
        protected int Xi = 1;

        protected override string DoAttack(Challenger target)
        {
            return $"A{Xi++}";
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
