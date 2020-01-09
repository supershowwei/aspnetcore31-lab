using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AspNetCore31Lab.Protocol.Model.Data;
using Chef.Extensions.DbAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AspNetCore31Lab.Middlewares
{
    public static class SignatureMiddlewareExtension
    {
        public static void UseSignature(this IApplicationBuilder app, string signature)
        {
            app.UseMiddleware<SignatureMiddleware>(signature);
        }
    }

    public class SignatureMiddleware
    {
        private readonly RequestDelegate next;
        private readonly string signature;

        public SignatureMiddleware(RequestDelegate next, string signature)
        {
            this.next = next;
            this.signature = signature;
        }

        public async Task InvokeAsync(HttpContext context, IDataAccess<Member> memDataAccess)
        {
            await this.next.Invoke(context);

            if (Regex.IsMatch(context.Response.ContentType, "text/html", RegexOptions.IgnoreCase))
            {
                await context.Response.WriteAsync(this.signature);
            }
        }
    }
}