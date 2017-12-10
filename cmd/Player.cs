namespace cmd
{
    public class Player : Challenger
    {
        public override void InstallShips()
        {
            Map.InstallRandomly();
        }

        protected override void DoMove()
        {
            System.Console.WriteLine("player :: move54645");
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
    }
}
