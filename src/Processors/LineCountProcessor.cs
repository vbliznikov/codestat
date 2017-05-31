using System;
using System.IO;

namespace Utils.Codestat.Processors
{
    public class LineCountProcessor : FileProcessor
    {
        private int _lineCount;
        public LineCountProcessor() : this(null) { }
        public LineCountProcessor(IFileProcessor next) : base(next) { }
        protected override void OnProcess(string filePath)
        {
            if (!File.Exists(filePath)) return;

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite & FileShare.Delete))
            {
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        reader.ReadLine();
                        System.Threading.Interlocked.Increment(ref _lineCount);
                    }
                }
            }
        }
        protected override void OnWriteReport(TextWriter writer)
        {
            writer.Write(" {0} lines ", _lineCount);
        }
    }
}