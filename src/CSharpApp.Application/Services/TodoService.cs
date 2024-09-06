using CSharpApp.Infrastructure.Http;

namespace CSharpApp.Application.Services;

public class TodoService : ITodoService
{
    private readonly IHttpClientWrapper _httpClientWrapper;

    public TodoService(IHttpClientWrapper httpClientWrapper)
    {
        _httpClientWrapper = httpClientWrapper;
    }

    public async Task<List<TodoRecord>?> GetAll()
    {
        return await _httpClientWrapper.GetAsync<List<TodoRecord>>("todos");
    }

    public async Task<TodoRecord?> GetById(int id)
    {
        return await _httpClientWrapper.GetAsync<TodoRecord>($"todos/{id}");
    }
}
