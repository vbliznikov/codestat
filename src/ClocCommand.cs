
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Utils.Codestat.Processors;
using Utils.Codestat.Processors.Extensions;

namespace Utils.Codestat
{
    public class ClocCommand
    {
        private IFileProcessor _clocProcessor;
        public ClocCommand(ClocSettings settings)
        {
            Settings = settings;
            var builder = new ChainBuilder();
            _clocProcessor = builder
                .Next<LineCountProcessor>()
                .Next<FileCountProcessor>()
                .Decorate<ExceptionHandlerDecorator>()
                .Decorate<ReportDecorator>()
                .Build();
        }

        public ClocSettings Settings { get; }

        public void Run()
        {
            EnumerateFiles()
                .CalculateStats(_clocProcessor)
                .WriteReport(Console.Out);
            // foreach (var file in files)
            // {
            //     _clocProcessor.Process(file);
            // }
            //_clocProcessor.WriteReport(Console.Out);
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
                    {
                        foldersQue.Enqueue(entry);
                        continue;
                    }
                    if (MatchFilter(entry))
                        yield return entry;
                    else
                        continue;

                }

            } while (foldersQue.Count > 0);
        }
        private bool MatchFilter(string value)
        {
            return Settings.Filter.Length > 0
                ? Settings.Filter.Any((filter) => value.EndsWith(filter))
                : true;
        }
    }
}
