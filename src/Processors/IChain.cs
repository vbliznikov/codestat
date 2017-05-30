namespace Utils.Codestat.Processors
{
    public interface IChain<T>
    {
        T Next { get; }
    }
}