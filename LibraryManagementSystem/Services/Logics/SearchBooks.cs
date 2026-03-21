using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Filepath;

namespace LibraryManagementSystem.Services.Logics
{
    public class SearchBooks
    {
        private List<Book> _books = new List<Book>();
        private readonly FileService _fileService;

        public SearchBooks(FileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<List<Book>> SearchBooksAsync(string query)
        {
            _books = await _fileService.LoadAsync<Book>(Constants.BookFilePath);
            return _books.Where(b => b.Title.ToLower().Contains(query.ToLower()) || b.Author.ToLower().Contains(query.ToLower())).ToList();
        }
    }
}
