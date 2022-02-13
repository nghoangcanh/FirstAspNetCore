using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.Extensions.Primitives;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace HelloWebApi.Middlewares
{
    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseAttractRequest(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AttractRequest>();
        }
    }
    public class AttractRequest
    {
        private readonly RequestDelegate _next;
        private string strHeader = "Key : Values \n";
        public AttractRequest(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            saveHeader2File(context.Request.Headers);
            
            string body = "";
            using (StreamReader stream = new StreamReader(context.Request.Body))
            {
                body = await stream.ReadToEndAsync();
            }
            saveBody(body);

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }

        private void saveBody(string strBody)
        {
            string fileName = "Body_" + DateTime.Now.ToFileTime() + ".txt";
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "//Data//Bodies";
            System.IO.Directory.CreateDirectory(path);
            string pathString = System.IO.Path.Combine(path, fileName);

            using (StreamWriter writer = new StreamWriter(pathString, true))
            {
                writer.WriteLine(strBody);
            }
        }

        private void saveHeader2File(IHeaderDictionary dict)
        {
            strHeader = string.Empty;
            foreach (StringValues keys in dict.Keys)
            {
                string str = keys + " : " + dict[keys];

                strHeader += str + "\n";
            }
            string fileName = "Header_" + DateTime.Now.ToFileTime() + ".txt";
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "//Data//Headers";
            System.IO.Directory.CreateDirectory(path);
            string pathString = System.IO.Path.Combine(path, fileName);

            using (StreamWriter writer = new StreamWriter(pathString, true))
            {
                writer.WriteLine(strHeader);
            }
        }
    }
}
