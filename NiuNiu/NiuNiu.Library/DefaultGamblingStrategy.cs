namespace NiuNiu.Library
{
    internal class DefaultGamblingStrategy : IGamblingStrategy
    {
        public int CurrentBet => 20;

        public bool ShouldTakePot(int value)
        {
            return value > 100;
        }
    }
}