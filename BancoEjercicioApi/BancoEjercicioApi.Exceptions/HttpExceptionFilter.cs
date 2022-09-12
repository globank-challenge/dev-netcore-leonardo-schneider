using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BancoEjercicioApi.Exceptions
{
    public class HttpExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            // Por defecto crea un error generico del tipo HttpException (error custom)
            var error = new HttpException(
                "Internal Server Error",
                "",
                (int)System.Net.HttpStatusCode.InternalServerError,
                System.Net.HttpStatusCode.InternalServerError);

            // Por defecto el status code lo setea en 500 "Internal Server Error"
            context.HttpContext.Response.StatusCode = 500;

            // Si el error que llega es del tipo HttpException (error custom)
            // toma ese error y reemplaza al generico
            if (context.Exception is HttpException)
            {  
                error = (HttpException)context.Exception;
                context.HttpContext.Response.StatusCode = (int)error.StatusCode;
            }

            // Setea el resultado del error con un JSON detallado
            context.Result = new JsonResult(new
            {
                error = error.ErrorCode,
                message = error.ErrorMessage,
                detail = error.ErrorDetail
            });

            base.OnException(context);
        }
    }
}
