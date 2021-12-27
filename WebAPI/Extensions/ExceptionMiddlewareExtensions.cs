﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Middlewares;

namespace WebAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app,
         IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }


    }
}
