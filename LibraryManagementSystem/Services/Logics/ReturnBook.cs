using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Filepath;

namespace LibraryManagementSystem.Services.Logics
{
    public class ReturnBook
    {
        private List<Member> _members = new List<Member>();
        private List<Book> _books = new List<Book>();
        private readonly FileService _fileService;

        public ReturnBook(FileService fileService)
        {
            _fileService = fileService;
        }
        public async Task<bool> ReturnBookAsync(int bookId, int memberId)
        {
            _books = await _fileService.LoadAsync<Book>(Constants.FilePaths.Books);
            _members = await _fileService.LoadAsync<Member>(Constants.FilePaths.Members);
            var book = _books.FirstOrDefault(b => b.BookID == bookId);
            var member = _members.FirstOrDefault(m => m.MemberID == memberId);
            if (book == null || member == null)
            {
                return false;
            }
            if (book.BookIsAvailable)
            {
                return false; 
            }
            book.BookIsAvailable = true; 
            await _fileService.SaveAsync(Constants.FilePaths.Books, _books); 
            return true;
        }
    }
}
