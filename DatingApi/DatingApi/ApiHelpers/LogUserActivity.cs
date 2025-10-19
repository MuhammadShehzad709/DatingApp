using DatingApi.Common.Extensions;
using DatingApi.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace DatingApi.ApiHelpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
             var resultContext=await next();
            if (context.HttpContext.User.Identity?.IsAuthenticated != true) return;
            var memberId = Guid.Parse( resultContext.HttpContext.User.GetMemberId());
            var dbContext = resultContext.HttpContext.RequestServices.GetRequiredService<AppDbContext>();
            await dbContext.Members
                .Where(x => x.Id == memberId)
                .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.LastActive, DateTime.UtcNow));
        }
    }
}
