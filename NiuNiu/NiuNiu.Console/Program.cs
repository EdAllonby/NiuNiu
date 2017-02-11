using NiuNiu.Library;

namespace NiuNiu.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var niuniu = new Game();
            while (niuniu.PlayersRemain)
            {
                niuniu.PlayRound();
            }

            int totalRounds = niuniu.Round;
            System.Console.WriteLine($"Total rounds played: {totalRounds}");
        }
    }
}