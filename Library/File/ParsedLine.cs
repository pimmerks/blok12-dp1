namespace DP1.Library.File
{
    public abstract class ParsedLine
    {
        public static string[] NodeTypes =
        {
            "INPUT_HIGH", "INPUT_LOW", "PROBE", "OR", "AND", "NOT", "NAND", "NOR", "XOR"
        };

        protected ParsedLine()
        {
        }
    }
}
