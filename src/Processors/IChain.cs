namespace Utils.Codestat.Processors
{
    public interface IChain<T> where T : class
    {
        T Next { get; }
    }
}