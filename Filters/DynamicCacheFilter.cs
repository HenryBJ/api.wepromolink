using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WePromoLink.Models;

namespace WePromoLink.Filters;


public class DynamicCacheAttribute : Attribute, IAsyncResultFilter
{
    private readonly string _resultType;
    public DynamicCacheAttribute(string resultType)
    {
        _resultType = resultType;
    }

    // public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    // {
    //     // Implemente el código aquí para leer los valores de duración de caché dinámicamente del modelo de datos 
    //     // y establecer la caché en consecuencia utilizando el filtro de acción ResponseCache.
    //     // Puede acceder al modelo de datos a través de la propiedad context.ActionArguments en el contexto de ejecución de la acción.

    //     // Lea los valores de duración de caché dinámicamente del modelo de datos.
    //     var model = context.ActionArguments.Values.OfType<StatsBaseModel>().FirstOrDefault();
    //     if (model != null && model.MaxAge.HasValue)
    //     {
    //         // Establecer los valores de duración de caché dinámicamente utilizando el filtro de acción ResponseCache.
    //         context.HttpContext.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
    //         {
    //             Public = true,
    //             MaxAge = model.MaxAge.Value
    //         };

    //         // Procesar el ETag
    //         var etag = model.Etag;
    //         var requestETag = context.HttpContext.Request.Headers.IfNoneMatch[0];
    //         if (requestETag != null && requestETag == etag)
    //         {
    //             context.Result = new StatusCodeResult(StatusCodes.Status304NotModified);
    //             return;
    //         }
    //         context.HttpContext.Response.Headers.ETag = model.Etag;
    //     }

    //     await next();
    // }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        StatsBaseModel model = null;
        if(((ObjectResult)context.Result).Value is OkObjectResult item)
        {
            model = (StatsBaseModel)item.Value!;
        }
        else 
        {
            await next();
        }

        if (model != null && model.MaxAge.HasValue)
        {
            // Establecemos la cache
            var etag = model.Etag;
            context.HttpContext.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
            {
                Public = true,
                MaxAge = model.MaxAge.Value
            };

            // Verificamos el etag y devolvemos 304 si no se ha cambiado
            var requestETag = context.HttpContext.Request.Headers.IfNoneMatch[0];
            if (requestETag != null && requestETag == etag)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status304NotModified);
                return;
            }
            context.HttpContext.Response.Headers.ETag = model.Etag;

            // Procesamos el model para solo devolver el valor
            Type nameType = Type.GetType(_resultType);
            var valueProperty = context.Result.GetType().GetProperty("Value");
            var value = valueProperty.GetValue(context.Result);
            // var convertedValue = Convert.ChangeType(value, nameType);
            // context.Result = value;

        }


        await next();
    }
}
