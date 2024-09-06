namespace CSharpApp.Core.Interfaces;

public interface IPostService
{
    Task<PostRecord?> GetById(int id);

    Task<List<PostRecord>?> GetAll();

    Task<int?> Add(PostRecord newPost);

    Task<bool> Delete(int id);
}
