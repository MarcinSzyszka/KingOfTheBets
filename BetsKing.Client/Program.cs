using BetsKing.Client.Services;
using BetsKing.Shared.ViewModels;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BetsKing.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(services =>
            {
                services.AddStorage();
                services.Add(ServiceDescriptor.Scoped<IApiClientService, ApiClientService>());
                services.Add(ServiceDescriptor.Singleton<StaticClientInfoViewModel>(new StaticClientInfoViewModel()));
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}
