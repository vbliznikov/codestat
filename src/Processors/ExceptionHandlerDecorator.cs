
using System;
using System.IO;

namespace Utils.Codestat.Processors
{
    public class ExceptionHandlerDecorator : IFileProcessor
    {
        private int _skipFilesCount;
        public ExceptionHandlerDecorator(IFileProcessor next)
        {
            Next = next;
        }
        public IFileProcessor Next { get; }

        public void Process(string path)
        {

            if (Next == null) return;

            try
            {
                Next.Process(path);
            }
            catch (System.Exception e)
            {
                _skipFilesCount++;
                Console.Error.WriteLine("Can't process {0} file.\n{1}", path, e.Message);
            }
        }

        public void WriteReport(TextWriter writer)
        {
            if (Next != null)
                Next.WriteReport(writer);

            if (_skipFilesCount > 0)
                writer.Write(" {0} files skipped;", _skipFilesCount);
        }
    }
}