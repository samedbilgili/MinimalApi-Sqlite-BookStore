using Microsoft.EntityFrameworkCore;
using MinimalApi.Models;

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

        public IEnumerable<Books> GetAll()
        {
            return (from book in _dbContext.Books
                    select book).ToList();
        }

        public bool Add(Books product)
        {
            _dbContext.Books.Add(product);

            if (_dbContext.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public bool Update(Books product)
        {
            var existingProduct = _dbContext.Books.Find(product.Id);
            if (existingProduct != null)
            {
                _dbContext.Entry(existingProduct).CurrentValues.SetValues(product);
                if (_dbContext.SaveChanges() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Delete(int id)
        {
            var product = _dbContext.Books.Find(id);
            if (product != null)
            {
                _dbContext.Books.Remove(product);
                if (_dbContext.SaveChanges() > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
