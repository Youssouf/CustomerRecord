using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomerDataRecord.Startup))]
namespace CustomerDataRecord
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
