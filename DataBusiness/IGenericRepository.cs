using MinimalApi.Models;

namespace MinimalApi.DataBusiness
{
    public interface IGenericRepository<T>
    {
        T GetById(int id);
        T GetByTitle(string title);
        IEnumerable<T> GetAll();
        bool Add(T product);
        bool Update(T product);
        bool Delete(int id);
    }
}
