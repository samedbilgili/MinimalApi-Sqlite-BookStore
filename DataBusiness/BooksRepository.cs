using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Models;
using Slugify;

namespace MinimalApi.DataBusiness
{
    public class BooksRepository : IGenericRepository<Books>
    {
        private readonly DataContext _dbContext = new DataContext();

        public Books GetById(int id)
        {
            return (from book in _dbContext.Books
                    where book.Id == id
                    select book).FirstOrDefault();
        }

        public Books GetByTitle(string title)
        {
            return (from book in _dbContext.Books
                    where book.Title.Contains(title)
                    select book).FirstOrDefault();
        }

        public IEnumerable<Books> GetAll(HttpContext httpContext)
        {
            string imageBasePath = httpContext.Request.Scheme + "://" + httpContext.Request.Host.Value + "/" + Path.Combine("upload") + "/";

            return (from book in _dbContext.Books
                    select new Books()
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        CoverImageUrl = imageBasePath + (book.CoverImageUrl == "" ? "ImageNotFound.png" : book.CoverImageUrl),
                        Description = book.Description,
                        Genre = book.Genre,
                        ISBN = book.ISBN,
                        Language = book.Language,
                        PageCount = book.PageCount,
                        PublicationDate = book.PublicationDate,
                        Publisher = book.Publisher

                    }).ToList();
        }

        public int Add(Books book)
        {
            _dbContext.Books.Add(book);

            if (_dbContext.SaveChanges() > 0)
            {
                return book.Id;
            }
            return 0;
        }

        public int Update(Books book)
        {
            var existingBook = _dbContext.Books.Find(book.Id);
            if (existingBook != null)
            {
                _dbContext.Entry(existingBook).CurrentValues.SetValues(book);
                _dbContext.Entry(existingBook).State = EntityState.Modified;
                if (_dbContext.SaveChanges() > 0)
                {
                    return book.Id;
                }
            }
            return 0;
        }

        public bool Delete(int id)
        {
            var book = _dbContext.Books.Find(id);
            if (book != null)
            {
                _dbContext.Books.Remove(book);
                if (_dbContext.SaveChanges() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool UploadFile(IFormFile image, int bookId)
        {
            //image name remove brake for uploaded image 
            //Examine < https://www.nuget.org/packages/Slugify.Core/

            SlugHelper helper = new SlugHelper();

            if (image != null)
            {
                string uploadsFolder = Path.Combine("wwwroot/upload");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + helper.GenerateSlug(image.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                //save image path
                var book = GetById(bookId);
                book.CoverImageUrl = uniqueFileName;
                _dbContext.Entry(book).State = EntityState.Modified;
                if (_dbContext.SaveChanges() > 0)
                    return true;
            }

            return false;
        }
    }
}
