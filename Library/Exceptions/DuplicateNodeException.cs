namespace DP1.Library.Exceptions
{
    using System;

    public class DuplicateNodeException : Exception
    {
        public DuplicateNodeException(string message)
            : base(message)
        {
        }
    }
}
