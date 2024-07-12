using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Journey.Api.Filter;
/*Com esse metodo eu nao preciso mais alterar ele mantendo a boa pratica
 importante ter ciudado e sempre lemrbar que não devo ficar alterando códio ja escrito 
mas sim implementando ele criando novos metodos e utilizando a herança*/
public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is JourneyException)
        {
            var journeyException = (JourneyException)context.Exception;

            context.HttpContext.Response.StatusCode = (int)journeyException.GetStatusCode();

            var responseJson = new ResponseErrorJson(journeyException.GetErrorMessages());

            context.Result = new ObjectResult(responseJson);
        }
        else 
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            /*essa linha dentro do parenteses é o equivalente a forma como vou descrever abaixo
            var responseJson = new ResponseErrorJson(new List<string> { "Erro Desconecido"});*/

            var list = new List<string>
            {
                "Erro Desconhecido"
            };
            var responseJson = new ResponseErrorJson(list);

            context.Result = new ObjectResult(responseJson);
        }
    }
}
