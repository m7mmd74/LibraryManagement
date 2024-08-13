using LibraryManagement.Models;

namespace LibraryManagement.Repositoties
{
    public class AuthorRepository : IBookRepository<Author>
    {
        LibraryDbContext db;
        public AuthorRepository(LibraryDbContext _db)
        {
            db = _db;
        }
        public void Add(Author entity)
        {
            db.Authors.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = Get(id);
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public Author Get(int id)
        {
            return db.Authors.SingleOrDefault(a => a.Id == id);
        }

        public IList<Author> GetAll()
        {
            return db.Authors.ToList();
        }

        public List<Author> Search(string term)
        {
            return db.Authors.Where(a=>a.AuthorName.Contains(term)).ToList();
        }

        public void Update(Author newAuthor)
        {
            db.Update(newAuthor);
            db.SaveChanges(); 
        }
    }
}
