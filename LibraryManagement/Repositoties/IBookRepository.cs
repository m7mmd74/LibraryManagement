using LibraryManagement.Models;

namespace LibraryManagement.Repositoties
{
    public interface IBookRepository<TEntity>
    {
        IList<TEntity> GetAll();
        TEntity Get(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
        List<TEntity> Search(string term);
    }
}
