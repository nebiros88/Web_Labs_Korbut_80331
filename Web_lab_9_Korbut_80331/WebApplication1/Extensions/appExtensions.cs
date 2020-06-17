using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Middleware;

namespace WebApplication1.Extensions
{
    public static class appExtensions
    {
        public static IApplicationBuilder UseFileLogging(this IApplicationBuilder app)=> app.UseMiddleware<LogMiddleware>();
    }
}
