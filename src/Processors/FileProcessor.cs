using System.IO;

namespace Utils.Codestat.Processors
{

    public abstract class FileProcessor : IFileProcessor
    {
        public FileProcessor() : this(null) { }
        protected FileProcessor(IFileProcessor next)
        {
            this.Next = next;
        }
        public void Process(string filePath)
        {
            OnProcess(filePath);

            if (Next != null)
                Next.Process(filePath);
        }
        protected abstract void OnProcess(string filePath);
        public virtual void WriteReport(TextWriter writer)
        {
            OnWriteReport(writer);
            if (Next != null)
                Next.WriteReport(writer);
        }
        protected abstract void OnWriteReport(TextWriter writer);
        public IFileProcessor Next { get; }
    }
}