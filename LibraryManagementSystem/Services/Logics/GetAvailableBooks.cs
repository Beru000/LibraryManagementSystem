using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Filepath;

namespace LibraryManagementSystem.Services.Logics
{
    public class GetAvailableBooks
    {
        private List<Book> _books = new List<Book>();
        private readonly FileService _fileService;

        public GetAvailableBooks(FileService fileService)
        {
            _fileService = fileService;
        }   

        public async Task<List<Book>> GetAvailableBooksAsync()
        {
            _books = await _fileService.LoadAsync<Book>(Constants.BookFilePath);
            return _books.Where(b => b.IsAvailable).ToList();
        }

    }
}
