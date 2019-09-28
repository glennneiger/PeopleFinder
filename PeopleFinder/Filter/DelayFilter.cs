using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace PeopleFinder.Filter
{
    public class DelayFilter : IAsyncActionFilter
    {
        private readonly int _delayInMilliseconds;
        public DelayFilter()
        {
            _delayInMilliseconds = 3000;
        }

        async Task IAsyncActionFilter.OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await Task.Delay(_delayInMilliseconds);
            await next();
        }
    }
}
