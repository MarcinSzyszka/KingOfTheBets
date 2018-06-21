using AutoMapper;
using BetsKing.Client.Services;
using BetsKing.Shared.ViewModels;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;

namespace BetsKing.Client.Pages
{
    public class BasePage : BlazorComponent
    {
        [Inject]
        protected IApiClientService ApiClient { get; set; }

        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected StaticClientInfoViewModel LoggedGambler { get; set; }
    }
}
