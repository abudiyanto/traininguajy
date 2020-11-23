using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrainingUAJY.Startup))]
namespace TrainingUAJY
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
