using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Filepath;

namespace LibraryManagementSystem.Services.Logics
{
    public class AddBook
    {
        private List<Book> _books = new List<Book>();
        private readonly FileService _fileService;

        public AddBook(FileService fileService)
        {
            _fileService = fileService;
        }

        public async Task AddBookAsync(string title, string author, string genre)
        {
            _books = await _fileService.LoadAsync<Book>(Constants.BookFilePath);
            int bookId = _books.Count + 1; 
            Book book = new Book(bookId, title, author, genre);
            _books.Add(book);
            await _fileService.SaveAsync(Constants.BookFilePath, _books);
        }
    }
}
