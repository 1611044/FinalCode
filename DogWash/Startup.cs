using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DogWash.Startup))]
namespace DogWash
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
