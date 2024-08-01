using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MinimalApi.DataBusiness;
using MinimalApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore Minimal API", Description = "Book Registration System", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore Minimal API V1");
    });
}


app.MapGet("/", () => "Hello World!");
app.MapGet("/books", () => new BooksRepository().GetAll());
app.MapGet("/books/{id}", (int id) => new BooksRepository().GetById(id));
app.MapGet("/booksGetByTitle/{title}", (string title) => new BooksRepository().GetByTitle(title));
app.MapPost("/books", (Books books) => new BooksRepository().Add(books));
app.MapPut("/books", (Books books) => new BooksRepository().Update(books));
app.MapDelete("/books/{id}", (int id) => new BooksRepository().Delete(id));

app.Run();