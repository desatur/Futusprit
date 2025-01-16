namespace Futusprit.Extensions
{
    internal class FutuspritException : Exception
    {
        public int ErrorCode { get; set; }

        public FutuspritException()
        {
            HandleException();
        }

        public FutuspritException(string message) : base(message)
        {
            HandleException();
        }

        public FutuspritException(string message, Exception innerException) : base(message, innerException)
        {
            HandleException();
        }

        public FutuspritException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
            HandleException();
        }

        static void HandleException()
        {

        }
    }
}
