using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactsManager.Filters.ResultFilters
{
    public class PersonsAlwaysRunResultFlter : IAsyncAlwaysRunResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            await Task.CompletedTask;
        }
    }
}
