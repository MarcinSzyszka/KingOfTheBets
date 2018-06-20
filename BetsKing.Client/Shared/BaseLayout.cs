using BetsKing.Client.Services;
using BetsKing.Shared.ViewModels;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Layouts;
using Microsoft.AspNetCore.Blazor.Services;
using System.Threading.Tasks;

namespace BetsKing.Client.Shared
{
    public class BaseLayout : BlazorLayoutComponent
    {
        [Inject]
        protected IApiClientService Client { get; set; }

        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected LocalStorage Storage { get; set; }

        [Inject]
        public StaticClientInfoViewModel StaticClientInfoViewModel { get; set; }

        protected override async Task OnInitAsync()
        {
            await Client.GetAndSetGamblerInfo();
        }
    }
}
