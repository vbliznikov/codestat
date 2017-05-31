namespace Utils.Codestat.Processors
{
    public class ReportDecorator : FileProcessor
    {
        public ReportDecorator() : this(null) { }
        public ReportDecorator(IFileProcessor next) : base(next) { }
        protected override void OnProcess(string filePath)
        {
            //Do nothing
        }

        protected override void OnWriteReport(System.IO.TextWriter writer)
        {
            //Do nothing since all work is done in other overload
        }

        public override void WriteReport(System.IO.TextWriter writer)
        {
            writer.Write("Found:");
            if (Next != null)
                Next.WriteReport(writer);
            writer.Write("\n");
        }
    }
}