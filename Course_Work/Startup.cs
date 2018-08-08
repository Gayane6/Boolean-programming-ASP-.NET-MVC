using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Course_Work.Startup))]
namespace Course_Work
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
