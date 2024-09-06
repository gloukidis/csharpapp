using CSharpApp.Infrastructure.Http;

namespace CSharpApp.Application.Services;

public class PostService : IPostService
{
    private readonly IHttpClientWrapper _httpClientWrapper;

    public PostService(IHttpClientWrapper httpClientWrapper)
    {
        _httpClientWrapper = httpClientWrapper;
    }

    public async Task<PostRecord?> Add(PostRecord newPost)
    {
        return await _httpClientWrapper.PostAsync<PostRecord, PostRecord>("post", newPost);
    }

    public async Task<bool> Delete(int id)
    {
        return await _httpClientWrapper.DeleteAsync($"posts/{id}");
    }

    public async Task<List<PostRecord>?> GetAll()
    {
        return await _httpClientWrapper.GetAsync<List<PostRecord>>($"posts");
    }

    public async Task<PostRecord?> GetById(int id)
    {
        return await _httpClientWrapper.GetAsync<PostRecord>($"posts/{id}");
    }
}
