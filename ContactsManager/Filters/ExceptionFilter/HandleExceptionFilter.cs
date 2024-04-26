using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactsManager.Filters.ExceptionFilter
{
    public class HandleExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<HandleExceptionFilter> _logger;
        private readonly IHostEnvironment _environment;

        public HandleExceptionFilter(ILogger<HandleExceptionFilter> logger, IHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogInformation(context.Exception.Message);
            if(_environment.IsDevelopment())
            {
                context.Result = new ContentResult() { Content = context.Exception.Message, StatusCode= 500};
            }
        }
    }
}
