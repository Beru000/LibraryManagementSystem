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
            _books = await _fileService.LoadAsync<Book>(Constants.BookFilePath);
            _members = await _fileService.LoadAsync<Member>(Constants.MemberFilePath);
            var member = _members.FirstOrDefault(m => m.Id == memberId);
            if (member == null)
            {
                return new List<Book>();
            }
            var books = _books.Where(b => member.BorrowedBookIds.Contains(b.Id)).ToList();
            return books;
        }
    }
}
