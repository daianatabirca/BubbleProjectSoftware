using ProjectManager.Services.Enums;

namespace ProjectManager.Services.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException()
        {
        }

        public BadRequestException(string message)
            : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public string DisplayMessage => "Bad request input. Malformed request.";

        public ErrorCode GetErrorCode()
        {
            return ErrorCode.BadRequest;
        }
    }
}
