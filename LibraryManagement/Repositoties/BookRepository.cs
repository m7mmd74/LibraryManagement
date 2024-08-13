using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repositoties
{
    public class BookRepository : IBookRepository<Book>
    {
        LibraryDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BookRepository(LibraryDbContext _db, UserManager<ApplicationUser> _userManager, IHttpContextAccessor _httpContextAccessor) 
        { 
            db = _db;
            userManager = _userManager;
            httpContextAccessor = _httpContextAccessor;
        }
        public void Add(Book entity)
        {
            var currentUser = userManager.GetUserAsync(httpContextAccessor.HttpContext.User).Result; // Get the current user
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = currentUser.UserName;
            entity.LastUpdatedAt = DateTime.Now;
            entity.LastUpdatedBy = currentUser.UserName;

            db.Books.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Get(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book Get(int id)
        {
            return db.Books.Include(a => a.Author).SingleOrDefault(b => b.Id == id); ;
        }

        public IList<Book> GetAll()
        {
            return db.Books.Include(a=>a.Author).ToList();
        }

        public void Update(Book updatedBook)
        {
            var existingBook = Get(updatedBook.Id);

            var currentUser = userManager.GetUserAsync(httpContextAccessor.HttpContext.User).Result; // Get the current user
            existingBook.Title = updatedBook.Title;
            existingBook.LastUpdatedAt = DateTime.Now;
            existingBook.LastUpdatedBy = currentUser.UserName;

            db.Update(updatedBook);
            db.SaveChanges();
        }
        public List<Book>Search(string term)
        {
            var result = db.Books.Include(a=>a.Author)
                .Where(b=>b.Title.Contains(term) 
                       || b.Author.AuthorName.Contains(term)
                       ||b.Description.Contains(term)).ToList();
            return result;
        }

    }
}
