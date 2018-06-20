using BetsKing.Server.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BetsKing.Server
{
    public partial class Startup
    {
        public void AddSettings(IServiceCollection services)
        {
            services.Configure<AuthSettings>(Configuration.GetSection("Auth"));
        }
    }
}
