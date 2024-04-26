using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactsManager.Filters.ResultFilters
{
    public class PersonListResultFilter : IAsyncResourceFilter
    {
        private readonly ILogger<PersonListResultFilter> _logger;

        public PersonListResultFilter(ILogger<PersonListResultFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            //Before logic
            _logger.LogInformation($"{nameof(PersonListResultFilter)} - {nameof(OnResourceExecutionAsync)} -- Before logic");
            await next();
            //After logic
            _logger.LogInformation($"{nameof(PersonListResultFilter)} - {nameof(OnResourceExecutionAsync)} -- After logic");

        }
    }
}
