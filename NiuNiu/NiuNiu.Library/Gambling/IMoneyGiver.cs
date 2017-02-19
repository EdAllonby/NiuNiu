namespace NiuNiu.Library.Gambling
{
    /// <summary>
    /// Something that gives money to an <see cref="IMoneyReceiver" />.
    /// </summary>
    public interface IMoneyGiver
    {
        /// <summary>
        /// Give money to an <see cref="IMoneyReceiver" />.
        /// </summary>
        /// <param name="receiver">The receiver of the money.</param>
        /// <param name="amountToGive">The amount to give to the <see cref="IMoneyReceiver" />.</param>
        void GiveMoney(IMoneyReceiver receiver, int amountToGive);
    }
}