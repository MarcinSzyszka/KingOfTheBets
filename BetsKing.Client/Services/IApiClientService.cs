using System.Net.Http;
using System.Threading.Tasks;

namespace BetsKing.Client.Services
{
    public interface IApiClientService
    {
        void AddAuthorizationHeader(string token);
        Task<T> GetJsonAuthorizedAsync<T>(string uri);
        Task<T> GetJsonNotAuthorizedAsync<T>(string uri);
        bool HasAuthorizationHeader();
        Task<T> SendJsonAuthorizedAsync<T>(HttpMethod method, string uri, object content);
        Task<T> SendJsonNotAuthorizedAsync<T>(HttpMethod method, string uri, object content);
        Task GetAndSetGamblerInfo();
    }
}