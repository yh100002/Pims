 

using System.Net.Http;
using System.Threading.Tasks;

namespace Utils.Http
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri);
        Task<HttpResponseMessage> PostAsync<T>(string uri, T item);
    }
}
