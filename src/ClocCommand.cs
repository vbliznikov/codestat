
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Utils.Codestat.Processors;

namespace Utils.Codestat
{
    public class ClocCommand
    {
        private IFileProcessor _clocProcessor;
        public ClocCommand(ClocSettings settings)
        {
            Settings = settings;
            _clocProcessor = new ReportDecorator(new FileCountProcessor());
        }

        public ClocSettings Settings { get; }

        public void Run()
        {
            var files = EnumerateFiles();
            foreach (var file in files)
            {
                _clocProcessor.Process(file);
            }
            WriteDevider();
            _clocProcessor.WriteReport(Console.Out);
            WriteDevider();
        }

        protected IEnumerable<string> EnumerateFiles()
        {
            var foldersQue = new Queue<string>();

            foldersQue.Enqueue(Settings.Path);
            do
            {
                var path = foldersQue.Dequeue();
                foreach (var entry in Directory.EnumerateFileSystemEntries(path))
                {
                    if (File.GetAttributes(entry).HasFlag(FileAttributes.Directory))
                        foldersQue.Enqueue(entry);
                    else
                    {
                        if (MatchFilter(entry))
                            yield return entry;
                        else
                            continue;
                    }
                }

            } while (foldersQue.Count > 0);
        }
        private bool MatchFilter(string value)
        {
            return Settings.Filter.Length > 0
                ? Settings.Filter.Any((filter) => value.EndsWith(filter))
                : true;
        }
        private static void WriteDevider()
        {
            Console.WriteLine(new String('-', 50));
        }
    }
}
