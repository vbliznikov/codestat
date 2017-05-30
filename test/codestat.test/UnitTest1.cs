using System;
using Xunit;

namespace Utils.Codestat.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var cmd = new ClocCommand(ClocSettings.Default);
            cmd.Run();
        }
    }
}