using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ExceptionFilter : Attribute, IExceptionFilter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void OnException(ExceptionContext context)
    {
        var mensagemErro = context.Exception.InnerException is not null
            ? context.Exception.InnerException.Message
            : context.Exception.Message;

        int statusCode = 500;
        string mensagem = $"Ocorreu um erro inesperado durante a execução da aplicação: {mensagemErro}";

        var result = new ObjectResult(new List<Notification>()
        {
            new(mensagem, false)
        })
        {
            StatusCode = statusCode
        };

        context.Result = result;
        context.ExceptionHandled = true;
    }
}
