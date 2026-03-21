using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Filepath;

namespace LibraryManagementSystem.Services.Logics
{
    public class GetMemberBooks
    {
        private List<Book> _books = new List<Book>();
        private List<Member> _members = new List<Member>();
        private readonly FileService _fileService;
        public GetMemberBooks(FileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<List<Book>> GetMemberBooksAsync(int memberId)
        {
            _books = await _fileService.LoadAsync<Book>(Constants.FilePaths.Books);
            _members = await _fileService.LoadAsync<Member>(Constants.FilePaths.Members);
            var member = _members.FirstOrDefault(m => m.MemberID == memberId);
            if (member == null)
            {
                return new List<Book>();
            }
            var books = _books.Where(b => member.MemberBorrowedBookIDs.Contains(b.BookID)).ToList();
            return books;
        }
    }
}
