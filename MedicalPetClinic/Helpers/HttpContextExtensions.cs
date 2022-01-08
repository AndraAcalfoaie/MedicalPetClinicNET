using DataAccess.Models;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Helpers
{
    public static class HttpContextExtensions
    {
        public static int GetUserId(this HttpContext context)
        {
            var user = (User)context.Items["User"];
            return user.Id;
        }
    }
}
