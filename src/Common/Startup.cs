using Owin;

namespace Common
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Run(ctx => ctx.Response.WriteAsync(ctx.Request.GetEncodedUrl()));
        }
    }
}