namespace Utils.Codestat.Processors
{
    public interface IChainBuilder<T>
    {
        IChainBuilder<T> Next<TDerived>();
        IChainBuilder<T> Decorate<Tderived>();
        T Build();
    }
}