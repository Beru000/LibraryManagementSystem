using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Filepath;

namespace LibraryManagementSystem.Services.Logics
{
    public class BorrowBook
    {
        private List<Member> _members = new List<Member>();
        private List<Book> _books = new List<Book>();
        private readonly FileService _fileService;

        public BorrowBook(FileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<bool> BorrowBookAsync(int bookId, int memberId)
        {
            _books = await _fileService.LoadAsync<Book>(Constants.BookFilePath);
            _members = await _fileService.LoadAsync<Member>(Constants.MemberFilePath);
            var book = _books.FirstOrDefault(b => b.Id == bookId);
            var member = _members.FirstOrDefault(m => m.Id == memberId);
            if (book == null || member == null)
            {
                return false;
            }
            if (!book.IsAvailable)
            {
                return false;
            }
            book.IsAvailable = false; 
            await _fileService.SaveAsync(Constants.BookFilePath, _books); 
            return true;
        }
    }
}
