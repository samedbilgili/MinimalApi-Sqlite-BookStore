using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using MinimalApi.DataBusiness;
using MinimalApi.Models;
using System.IO;
using System.IO.Pipes;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore Minimal API", Description = "Book Registration System", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
         policy =>
         {
             policy.AllowAnyHeader();
             policy.AllowAnyMethod();
             policy.AllowAnyOrigin();
             // policy.WithOrigins("http://localhost:3000/",
             //                     "http://localhost:3000");
         });
});


var app = builder.Build();

//app.UseStaticFiles(new StaticFileOptions()
//{
//    FileProvider = new PhysicalFileProvider(
//            System.IO.Path.GetFullPath("upload")),
//    RequestPath = new PathString("/images"),
//    DefaultContentType = "application/octet-stream"
//});

app.UseCors();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore Minimal API V1");
    });
}

app.MapGet("/", () => "Hello World!");
app.MapGet("/books", (HttpContext httpContext) => new BooksRepository().GetAll(httpContext));
app.MapGet("/books/{id}", (int id) => new BooksRepository().GetById(id));
app.MapGet("/booksGetByTitle/{title}", (string title) => new BooksRepository().GetByTitle(title));
app.MapPost("/books", (Books books) => new BooksRepository().Add(books));
app.MapPut("/books", (Books books) => new BooksRepository().Update(books));
app.MapDelete("/books/{id}", (int id) => new BooksRepository().Delete(id));
app.MapPost("/bookimage", ([FromQuery]int bookId, [FromForm]IFormFile image) => new BooksRepository().UploadFile(image, bookId)).DisableAntiforgery();

//app.MapGet("/upload/{image}", (string image) =>
//{
//    //var mimeType = "image/png";
//    //var path = @image;
//    //return Results.File(path, contentType: mimeType);

//    return Results.File("58fa5c72-bc11-4c97-a0cd-6cbe36d53632_a.jpg", "image/jpg", "58fa5c72-bc11-4c97-a0cd-6cbe36d53632_a.jpg");
//});

//app.MapGet("/download", () =>
//{
//    var mimeType = "image/jpg";
//    //var path = @"//upload/58fa5c72-bc11-4c97-a0cd-6cbe36d53632_a.jpg";
//    //return Results.File(path, contentType: mimeType);
//    string uniqueFileName = null;
//    FileStream _filestream = null;
//    string uploadsFolder = Path.Combine("upload");
//    uniqueFileName = "58fa5c72-bc11-4c97-a0cd-6cbe36d53632_a.jpg";
//    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
//    using (var fileStream = new FileStream(filePath, FileMode.Create))
//    {
//        uniqueFileName = fileStream.Name;
//        _filestream = fileStream;
//        //image.CopyTo(fileStream);

//        return Results.File(uniqueFileName, contentType: mimeType);
//    }

//});

//app.MapGet("/getpath", (HttpContext context) =>
//{
//    var host = context.Request.Host.ToUriComponent();

//    var filePath = "PathToSomeFileInStaticFileFolder.ext";
//    var url = $"{context.Request.Scheme}://{host}/{filePath}";
//    return url;
//});

app.Run();