using System.IO;

namespace Utils.Codestat.Processors
{
    public interface IFileProcessor : IChain<IFileProcessor>
    {
        void Process(string path);

        void WriteReport(TextWriter writer);
    }
}