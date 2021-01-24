using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(project_sem_3.Startup))]
namespace project_sem_3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
