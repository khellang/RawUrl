using System.Net;
using System.Web;
using Microsoft.Owin;

namespace Common
{
    internal static class OwinRequestExtensions
    {
        internal static string GetEncodedUrl(this IOwinRequest request)
        {
            HttpListenerContext httpListenerContext;
            if (request.TryGet("System.Net.HttpListenerContext", out httpListenerContext))
            {
                return httpListenerContext.Request.RawUrl;
            }

            HttpContextBase httpContextBase;
            if (request.TryGet("System.Web.HttpContextBase", out httpContextBase))
            {
                var rawUrl = httpContextBase.Request.RawUrl;

                try
                {
                    var unencodedUrl = httpContextBase.Request.ServerVariables["UNENCODED_URL"];

                    return string.IsNullOrEmpty(unencodedUrl) ? rawUrl : unencodedUrl;
                }
                catch
                {
                    return rawUrl;
                }
            }

            return request.Uri.OriginalString;
        }

        private static bool TryGet<T>(this IOwinRequest request, string key, out T value) where T : class
        {
            object objectValue;
            if (request.Environment.TryGetValue(key, out objectValue))
            {
                value = objectValue as T;
                return value != null;
            }

            value = default(T);
            return false;
        }
    }
}