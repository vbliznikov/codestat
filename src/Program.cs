using System;
using System.IO;

namespace Utils.Codestat
{
    internal class Program
    {
        private string[] args { get; }

        public Program(string[] args)
        {
            this.args = args;
        }

        static int Main(string[] args)
        {
            var p = new Program(args);
            return p.Run();
        }

        public int Run()
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("codestat <path> [filters]");
                return -1;
            }
            try
            {
                var settings = new ClocSettings()
                {
                    // TODO: Add more robust args parsing
                    Path = args[0],
                    Filter = args.Length > 1 ? args[1].Split(',') : new string[0]
                };
                var cmd = new ClocCommand(settings);
                cmd.Run();
                return 0;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return -1;
            }
        }
    }
}
