namespace CSharpApp.Infrastructure.Http
{
    public interface IHttpClientWrapper
    {
        Task<T?> GetAsync<T>(string uri);
        Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest content);
        Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest content);
        Task<bool> DeleteAsync(string uri);
    }
}