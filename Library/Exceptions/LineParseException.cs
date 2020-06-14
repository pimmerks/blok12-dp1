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

    public class NoOutputNodesException : Exception
    {
        public NoOutputNodesException(string message) : base(message)
        {
        }
    }
    
    public class NoInputNodesException : Exception
    {
        public NoInputNodesException(string message) : base(message)
        {
        }
    }

    public class InvalidConnectionException : Exception
    {
        public InvalidConnectionException(string message) : base(message)
        {
        }
    }

    public class PathException : Exception
    {
        public PathException(string message) : base(message)
        {
        }
    }
}
