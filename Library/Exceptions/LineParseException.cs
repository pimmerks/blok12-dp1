namespace DP1.Library.Exceptions
{
    using System;

    public class LineParseException : Exception
    {
        public LineParseException(string message, string line, int? lineNumber = null)
            : base($"{message}{Environment.NewLine}Line: {lineNumber} - {line}")
        {
        }
    }
}
