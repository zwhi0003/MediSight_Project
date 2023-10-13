using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MediSight_Project.Startup))]
namespace MediSight_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
