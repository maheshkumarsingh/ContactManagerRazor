using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactsManager.Filters.ResourceFilters
{
    public class FeatureDisabledResourceFilter : IAsyncResourceFilter
    {

        private readonly ILogger<FeatureDisabledResourceFilter> _logger;
        private readonly bool _disabled;

        public FeatureDisabledResourceFilter(ILogger<FeatureDisabledResourceFilter> logger, bool disabled = true)
        {
            _logger = logger;
            _disabled = disabled;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            //before logic

            _logger.LogInformation($"{nameof(FeatureDisabledResourceFilter)}, {nameof(OnResourceExecutionAsync)} -- before logic");
            if ( _disabled )
            {
                context.Result = new NotFoundResult();
                //context.Result = new StatusCodeResult(501);
            }
            else
            {
                await next();
            }

            //after logic
            _logger.LogInformation($"{nameof(FeatureDisabledResourceFilter)}, {nameof(OnResourceExecutionAsync)} -- after logic");
        }
    }
}
