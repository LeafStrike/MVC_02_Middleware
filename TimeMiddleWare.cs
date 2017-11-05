using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace TimeWare
{
    class TimeMiddleWare
    {
        private readonly RequestDelegate _next;

        public TimeMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            int startTime = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
            await context.Response.WriteAsync("Middleware class started... \r\n");
            await _next.Invoke(context);
            int endTime = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
            await context.Response.WriteAsync("Middleware class time: " + (endTime - startTime).ToString() + "ms.\r\n");
        }
    }
}
