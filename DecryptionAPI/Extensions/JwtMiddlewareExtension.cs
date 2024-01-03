﻿
using Business.Services;
using DecryptionAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace DecryptionAPI.Extensions
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        // private readonly IServiceWrapper _service;
        private JwtUtils _service;

        public JwtMiddleware(RequestDelegate next, JwtUtils service)
        {
            _next = next;
            _service = service;
            
        }        

        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is object)
            {
                await _next(context);
                return;
            }

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
          //  var userId = _service.JwtUtils.ValidateToken(token);
            var userId = _service.ValidateToken(token);

            if (userId != null)
            {
                // attach userId to context on successful jwt validation
                context.Items["User"] = userId;
                await _next(context);
            }
            else
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync(new ErrorDetails()
                {
                    statusCode = context.Response.StatusCode,
                    message = "Unauthorized"
                }.ToString());
            }            
        }
    }
}
