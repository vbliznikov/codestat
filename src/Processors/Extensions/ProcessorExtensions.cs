
using System.Collections.Generic;

namespace Utils.Codestat.Processors.Extensions
{
    static class ProcessorsExtensions
    {
        public static void AcceptProcessor(this string path, IFileProcessor processor)
        {
            processor.Process(path);
        }

        public static IFileProcessor CalculateStats(this IEnumerable<string> fileEntries, IFileProcessor processor)
        {
            foreach (var filePath in fileEntries)
                filePath.AcceptProcessor(processor);

            return processor;
        }
    }

}