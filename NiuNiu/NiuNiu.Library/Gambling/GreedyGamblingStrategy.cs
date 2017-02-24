namespace NiuNiu.Library.Gambling
{
    public sealed class GreedyGamblingStrategy : IGamblingStrategy
    {
        public int CurrentBet => 50;

        public bool ShouldTakePot(int potValue)
        {
            return potValue == 400;
        }
    }
}