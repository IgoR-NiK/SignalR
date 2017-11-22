using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Server.App_Start.Startup))]
namespace Server.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}