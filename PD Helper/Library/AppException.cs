namespace PD_Helper.Library
{
    /// <summary>
    /// Custom application exception used to throw exceptions meant to be displayed to the user
    /// </summary>
    internal class AppException : Exception
    {
        public AppException()
        {
        }

        public AppException(string message)
            : base(message)
        {
        }

        public AppException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
