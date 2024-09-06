namespace CSharpApp.Core.Interfaces;

public interface ITodoService
{
    Task<TodoRecord?> GetById(int id);
    Task<List<TodoRecord>?> GetAll();
}