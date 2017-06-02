using System;
using System.IO;

namespace Utils.Codestat.Processors
{
    public class FileCountProcessor : FileProcessor
    {
        private int _count;
        public FileCountProcessor() : this(null) { }
        public FileCountProcessor(IFileProcessor next) : base(next) { }
        protected override void OnProcess(string filePath)
        {
            System.Threading.Interlocked.Increment(ref _count);
        }

        protected override void OnWriteReport(TextWriter writer)
        {
            writer.Write(" {0} files processed;", _count);
        }
    }

}