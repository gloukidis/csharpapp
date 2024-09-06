using CSharpApp.Application.Configuration;
using CSharpApp.Core.Dtos;
using CSharpApp.Infrastructure.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger());

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddDefaultConfiguration();

builder.Services.AddHttpClient<IHttpClientWrapper, HttpClientWrapper>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

#region Todos

app.MapGet("/todos", async (ITodoService todoService) =>
    {
        var todos = await todoService.GetAll();
        return todos;
    })
    .WithName("GetTodos")
    .WithOpenApi();

app.MapGet("/todos/{id}", async ([FromRoute] int id, ITodoService todoService) =>
    {
        var todos = await todoService.GetById(id);
        return todos;
    })
    .WithName("GetTodosById")
    .WithOpenApi();

#endregion

#region Posts

app.MapGet("/posts", async (IPostService postService) =>
{
    var posts = await postService.GetAll();
    return posts;
})
    .WithName("GetPosts")
    .WithOpenApi();

app.MapGet("/posts/{id}", async ([FromRoute] int id, IPostService postService) =>
{
    var posts = await postService.GetById(id);
    return posts;
})
    .WithName("GetPostsById")
    .WithOpenApi();

app.MapPost("/posts", async (IPostService postService, PostRecord newPost) =>
{
    var createdPost = await postService.Add(newPost);
    return Results.Created("/posts", createdPost);
})
    .WithName("AddPost")
    .WithOpenApi();

app.MapDelete("/posts/{id}", async ([FromRoute] int id, IPostService postService) =>
{
    await postService.Delete(id);
    return Results.NoContent();
})
    .WithName("DeletePost")
    .WithOpenApi();

#endregion

app.Run();