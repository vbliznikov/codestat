
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Utils.Codestat
{
    public class ClocCommand
    {
        public ClocCommand(ClocSettings settings)
        {
            Settings = settings;
        }

        public ClocSettings Settings { get; }

        public void Run()
        {
            WriteDevider();
            var files = EnumerateFiles();
            Console.WriteLine("Found:{0:} files", files.Count());
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

    public class ClocSettings
    {
        public string Path { get; set; } = ".";
        public string[] Filter { get; set; } = new string[0];

        public static ClocSettings Default => new ClocSettings();
    }
}
