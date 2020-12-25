using Core.CustomException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Test_API_RebbitMQ.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case AuthorizeException authorizeException:
                    context.Result = new JsonResult(new { authorizeException.Message })
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized
                    };
                    context.ExceptionHandled = true;
                    break;

                case NotFoundException notFoundEx:
                    context.Result = new JsonResult(new { notFoundEx.Message })
                    {
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                    context.ExceptionHandled = true;
                    break;

                case BadRequest badRequest:
                    context.Result = new JsonResult(new { badRequest.Message })
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                    context.ExceptionHandled = true;
                    break;

                default:
                    context.Result = new JsonResult(new { Message = "Server Error" })
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                    context.ExceptionHandled = true;
                    break;
            }
        }
    }
}
