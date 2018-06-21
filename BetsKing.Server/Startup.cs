using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using BetsKing.Server.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace BetsKing.Server
{
    public partial class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            AddSettings(services);
            AddAuth(services);
            services.AddEntityFrameworkSqlServer().AddDbContext<BetsKingDbContext>((serviceProvider, opt) =>
                {
                    opt.UseSqlServer(Configuration.GetConnectionString("Default")).UseInternalServiceProvider(serviceProvider);
                });
            services.AddMvc();

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var assemblyServices = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name.EndsWith("Service"));
            var assemblyServicesClasses = assemblyServices.Where(t => !t.IsInterface);

            foreach (var assemblyServiceClass in assemblyServicesClasses)
            {
                var assemblyServiceInterface = assemblyServices.FirstOrDefault(t => t.Name.EndsWith(assemblyServiceClass.Name) && t.IsInterface);

                services.AddScoped(assemblyServiceInterface, assemblyServiceClass);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BetsKingDbContext dbContext)
        {
            app.UseAuthentication();
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });

            app.UseBlazor<Client.Program>();

            if (env.IsDevelopment())
            {
                InitDev(dbContext);
            }
            else
            {
                InitProd(dbContext);
            }
        }
    }
}
