using System.Net;

namespace BancoEjercicioApi.Exceptions
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public int ErrorCode { get; internal set; }
        public string ErrorMessage { get; internal set; }
        public string ErrorDetail { get; internal set; }

        public HttpException()
        {
            ErrorMessage = "";
            ErrorDetail = "";
        }

        public HttpException(HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : this()
        {
            StatusCode = statusCode;
        }

        public HttpException(string errorMessage)
        {
            ErrorMessage = errorMessage ?? "";
            ErrorDetail = "";
        }

        public HttpException(string errorMessage, string errorDetail, int errorCode = (int)HttpStatusCode.InternalServerError, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            ErrorMessage = errorMessage ?? "";
            ErrorDetail = errorDetail ?? "";
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
    }
}