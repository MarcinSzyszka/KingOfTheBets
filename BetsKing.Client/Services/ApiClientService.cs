using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Services;
using System.Net.Http;
using System.Threading.Tasks;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using BetsKing.Shared.ViewModels.Gambler;
using BetsKing.Shared.ViewModels;

namespace BetsKing.Client.Services
{
    public class ApiClientService : IApiClientService
    {
        readonly HttpClient _httpClient;
        readonly IUriHelper _uriHelper;
        readonly LocalStorage _storage;
        readonly StaticClientInfoViewModel _staticClientInfoViewModel;

        public ApiClientService(HttpClient httpClient, IUriHelper uriHelper, LocalStorage storage, StaticClientInfoViewModel staticClientInfoViewModel)
        {
            _httpClient = httpClient;
            _uriHelper = uriHelper;
            _storage = storage;
            _staticClientInfoViewModel = staticClientInfoViewModel;
        }

        public Task<T> GetJsonNotAuthorizedAsync<T>(string uri)
        {
            TryRetrieveToken();

            return _httpClient.GetJsonAsync<T>(uri);
        }

        public async Task<T> GetJsonAuthorizedAsync<T>(string uri)
        {
            TryRetrieveToken();

            var response = await _httpClient.GetAsync("api/Gambler");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                _uriHelper.NavigateTo($"/login?returnUrl={_uriHelper.GetAbsoluteUri()}");

                return await Task.FromResult(default(T));
            }

            return await _httpClient.GetJsonAsync<T>(uri);
        }

        public Task<T> SendJsonNotAuthorizedAsync<T>(HttpMethod method, string uri, object content)
        {
            TryRetrieveToken();

            return _httpClient.SendJsonAsync<T>(method, uri, content);
        }

        public async Task<T> SendJsonAuthorizedAsync<T>(HttpMethod method, string uri, object content)
        {
            TryRetrieveToken();

            var response = await _httpClient.GetAsync("api/Gambler");
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                _uriHelper.NavigateTo($"/login?returnUrl={_uriHelper.GetAbsoluteUri()}");

                return await Task.FromResult(default(T));
            }

            return await _httpClient.SendJsonAsync<T>(method, uri, content);
        }

        public bool HasAuthorizationHeader()
        {
            return _httpClient.DefaultRequestHeaders.Contains("Authorization");
        }

        public void AddAuthorizationHeader(string token)
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
            }

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            _storage["Token"] = token;
        }

        public async Task GetAndSetGamblerInfo()
        {
            var gambler = await GetJsonNotAuthorizedAsync<GamblerViewModel>("api/Gambler/GetNotAuthorized");

            _staticClientInfoViewModel.SetGamblerData(gambler);
        }

        private void TryRetrieveToken()
        {
            if (!HasAuthorizationHeader())
            {
                var token = _storage["Token"];

                if (!string.IsNullOrEmpty(token))
                {
                    AddAuthorizationHeader(token);
                }
            }
        }
    }
}
