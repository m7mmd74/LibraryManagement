using LibraryManagement.Models;
using LibraryManagement.Repositoties;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LibraryManagement.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookRepository<Book> bookRepository;
        private readonly IBookRepository<Author> authorRepository;

        public BookController(IBookRepository<Book> bookRepository,IBookRepository<Author> authorRepository) 
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }
        // GET: BookController
        public IActionResult Index()
        {
            var books = bookRepository.GetAll();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Get(id);
            return View(book);
        }

        // GET: BookController/Create
        public IActionResult Create()
        {
            var viewModel = new BookAuthorViewModel
            {
                Authors = authorRepository.GetAll().ToList(),
            };
           
            return View(viewModel);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookAuthorViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Model is not valid, return the view with validation errors
                try
                {

                    var newBook = new Book
                    {
                        Title = viewModel.Title,
                        Description = viewModel.Description,
                        ISBN = viewModel.ISBN,
                        PublicationDate = viewModel.PublicationDate,
                        Genre = viewModel.Genre,
                        Author = authorRepository.Get(viewModel.AuthorId),

                        //other properties to impelemnt Feature to track history of book
                        CreatedAt = DateTime.Now,
                        LastUpdatedAt = DateTime.Now
                    };
                    bookRepository.Add(newBook);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
                return View(viewModel);
            }
                        
            return RedirectToAction(nameof(Index));
            
        }

        // GET: BookController/Edit/5
        public IActionResult Edit(int id)
        {
            var book = bookRepository.Get(id);
            var vModel = new BookAuthorViewModel
            {
                Title = book.Title,
                Description = book.Description,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
                Genre = book.Genre,
                AuthorId = book.Author.Id,
                Authors = authorRepository.GetAll().ToList(),
            };
            return View(vModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, BookAuthorViewModel vModel)
        {
            try
            {
                // Retrieve the existing book
                var existingBook = bookRepository.Get(id);

                // Update the properties
                existingBook.Title = vModel.Title;
                existingBook.Description = vModel.Description;
                existingBook.ISBN = vModel.ISBN;
                existingBook.PublicationDate = vModel.PublicationDate;
                existingBook.Genre = vModel.Genre;
                existingBook.Author = authorRepository.Get(vModel.AuthorId);

                existingBook.LastUpdatedAt = DateTime.Now;

                // Update the existing book
                bookRepository.Update(existingBook);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        // GET: BookController/Delete/5
        public IActionResult Delete(int id)
        {
            var book = bookRepository.Get(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
      public IActionResult Search(string term)
        {
            var result = bookRepository.Search(term);
            return View("Index", result);
        }
    }
}
