using ProjectManager.Services.Enums;

namespace ProjectManager.Services.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException()
        {
        }

        public InternalServerErrorException(string message)
            : base(message)
        {
        }

        public InternalServerErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InternalServerErrorException(Exception innerException)
            : base("Internal Server Error", innerException)
        {
        }

        //Can these be reused?
        public string DisplayMessage => "Internal Server Error occured.";

        public ErrorCode GetErrorCode()
        {
            return ErrorCode.ServerError;
        }
    }
}
