using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public class LibraryService
    {
        private List<Book> _books;
        private List<Member> _members;
        private List<BorrowRecord> _borrowRecords;
        private readonly FileService _fileService;

        public LibraryService(FileService fileService)
        {
            _fileService = fileService;
            _books = new List<Book>();
            _members = new List<Member>();
            _borrowRecords = new List<BorrowRecord>();
        }

        // Load all data from files when program starts
        public async Task InitializeAsync()
        {
            _books = await _fileService.LoadAsync<Book>("books.json");
            _members = await _fileService.LoadAsync<Member>("members.json");
            _borrowRecords = await _fileService.LoadAsync<BorrowRecord>("borrowRecords.json");
        }

        // Save all data to files
        private async Task SaveDataAsync()
        {
            await _fileService.SaveAsync("books.json", _books);
            await _fileService.SaveAsync("members.json", _members);
            await _fileService.SaveAsync("borrowRecords.json", _borrowRecords);
        }

        // Add a new book
        public async Task AddBookAsync(string title, string author, string genre)
        {
            int bookId = _books.Count + 1; // Generate new Id
            Book book = new Book(bookId, title, author, genre);
            _books.Add(book);
            await SaveDataAsync();
        }

        // Add a new member
        public async Task AddMemberAsync(string name, string email)
        {
            int memberId = _members.Count + 1;
            Member member = new Member(memberId, name, email);
            _members.Add(member);
            await SaveDataAsync();
        }

        // Borrow a book
        public async Task<bool> BorrowBookAsync(int bookId, int memberId)
        {
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
            member.BorrowedBookIds.Add(bookId);
            int recordId = _borrowRecords.Count + 1;
            var record = new BorrowRecord(recordId, bookId, memberId);
            await SaveDataAsync();
            return true;
        }

        // Return a book
        public async Task<bool> ReturnBookAsync(int bookId, int memberId)
        {
            var book = _books.FirstOrDefault(b => b.Id == bookId);
            var member = _members.FirstOrDefault(m => m.Id == memberId);
            if (book == null || member == null)
            {
                return false;
            }
            book.IsAvailable = true;
            member.BorrowedBookIds.Remove(bookId);
            var record = _borrowRecords.FirstOrDefault(r => r.BookId == bookId && r.MemberId == memberId&&r.ReturnDate==null);
            if (record != null)
            {
                record.ReturnDate = DateTime.Now;
            }
            await SaveDataAsync();
            return true;
        }

        // Get all available books — LINQ
        public List<Book> GetAvailableBooks()
        {
            return _books.Where(b => b.IsAvailable).ToList();
        }

        // Search books by title or author — LINQ
        public List<Book> SearchBooks(string query)
        {
             return _books.Where(b => b.Title.ToLower().Contains(query.ToLower()) || b.Author.ToLower().Contains(query.ToLower())).ToList();
        }

        // Get all books a member is currently borrowing — LINQ
        public List<Book> GetMemberBooks(int memberId)
        {
            var member=_members.FirstOrDefault(m => m.Id == memberId);
            if(member==null)
            {
                return new List<Book>();
            }
            var books = _books.Where(b=>member.BorrowedBookIds.Contains(b.Id)).ToList();
            return books;
        }

        public List<Book> GetAllBooks() => _books;
        public List<Member> GetAllMembers() => _members;
    }
}
