namespace CSharpApp.Core.Interfaces;

public interface IPostService
{
    Task<PostRecord?> GetById(int id);

    Task<List<PostRecord>?> GetAll();

    Task<PostRecord?> Add(PostRecord newPost);

    Task<bool> Delete(int id);
}
