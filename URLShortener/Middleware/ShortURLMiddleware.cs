using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShortener.Services;

namespace URLShortener.Middleware {
    public class ShortURLMiddleware {
        public ShortURLMiddleware(RequestDelegate next) {

        }

        public async Task InvokeAsync(HttpContext context, URLShortenerService shortenerService) {
            // Запрос на получение короткой ссылки
            if (context.Request.Method == "POST") {
                string url = context.Request.Query["url"];
                string shortUrl = shortenerService.GetShortURL(url);
                if (shortUrl == null) {
                    shortUrl = await shortenerService.CreateShortURL(url);
                    context.Response.StatusCode = 201;
                }
                else {
                    context.Response.StatusCode = 200;
                }
                await context.Response.WriteAsync($"{context.Request.Scheme}://{context.Request.Host}/{shortUrl}");
            }
            // Переход по короткой ссылке
            else if (context.Request.Method == "GET") {
                string fullUrl = shortenerService.GetFullURL(context.Request.Path.Value[1..]);
                if (fullUrl == null) {
                    context.Response.StatusCode = 404;
                }
                else {
                    context.Response.Redirect(fullUrl, false);
                }
            }
            else {
                context.Response.StatusCode = 400;
            }
        }
    }
}
