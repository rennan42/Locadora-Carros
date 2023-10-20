using FluentValidation;
using Newtonsoft.Json;
using System.Net;

namespace LocadoraCarros.Api.Configuracao
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException e)
            {
                context.Response.StatusCode = 400;
                var respostaValidacao = new RespostaException
                {
                    Errors = e.Errors.Select(p => new RespostaValidacao
                    {
                        Mensagem = p.ErrorMessage,
                        Propriedade = p.PropertyName
                    }).ToList()
                };

                await context.Response.WriteAsJsonAsync(respostaValidacao);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }

        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }

    public class RespostaValidacao
    {
        public string Mensagem { get; set; }
        public string Propriedade { get; set; }
    }

    public class RespostaException
    {
        public IList<RespostaValidacao> Errors { get; set; }
    }
}
