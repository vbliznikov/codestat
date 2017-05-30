namespace Utils.Codestat
{
    public class ClocSettings
    {
        public string Path { get; set; } = ".";
        public string[] Filter { get; set; } = new string[0];

        public static ClocSettings Default => new ClocSettings();
    }
}
