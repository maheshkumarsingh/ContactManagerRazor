using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactsManager.Filters.ActionFilters
{
    public class ResponseHeaderActionFilter : IAsyncActionFilter //ActionFilterAttribute// //,IOrderedFilter
    {
        private readonly ILogger<ResponseHeaderActionFilter> _logger;
        private readonly string? _key;
        private readonly string? _value;

        //public int Order { get; set; }

        public ResponseHeaderActionFilter(ILogger<ResponseHeaderActionFilter> logger, string? key, string? value)
        {
            _logger = logger;
            _key = key;
            _value = value;
            //Order = order;
        }


        //OLD : IActionFilter
        //public void OnActionExecuted(ActionExecutedContext context)
        //{
        //    _logger.LogInformation($"{nameof(ResponseHeaderActionFilter)}.{nameof(OnActionExecuted)}");
        //    context.HttpContext.Response.Headers[Key] = Value;
        //}

        //public void OnActionExecuting(ActionExecutingContext context)
        //{
            
        //}

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //To do : before logic
            _logger.LogInformation($"{nameof(ResponseHeaderActionFilter)}.{nameof(OnActionExecutionAsync)}");
            context.HttpContext.Response.Headers[_key] = _value;
            await next();
            //To do: after logic
            _logger.LogInformation($"{nameof(ResponseHeaderActionFilter)}.{nameof(OnActionExecutionAsync)}");
            context.HttpContext.Response.Headers[_key] = _value;
        }
    }
}
