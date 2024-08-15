using MinimalApi.Models;

namespace MinimalApi.DataBusiness
{
    public interface IGenericRepository<T>
    {
        T GetById(int id);
        T GetByTitle(string title);
        IEnumerable<T> GetAll(HttpContext httpContext);
        int Add(T product);
        int Update(T product);
        bool Delete(int id);
        bool UploadFile(IFormFile image, int bookId);
    }
}
