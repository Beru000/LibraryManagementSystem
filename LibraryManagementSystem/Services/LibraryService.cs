using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public class LibraryService
    {
        private List<Book> _books= new List<Book>();
        private List<Member> _members= new List<Member>();
        private List<BorrowRecord> _borrowRecords= new List<BorrowRecord>();
        private readonly FileService _fileService;

        public LibraryService(FileService fileService)
        {
            _fileService = fileService;
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
            var book = _books.FirstOrDefault(b => b.BookID == bookId);
            var member = _members.FirstOrDefault(m => m.MemberID == memberId);
            if (book == null || member == null)
            {
                return false;
            }
            if (!book.BookIsAvailable)
            {
                return false;
            }
            book.BookIsAvailable = false;
            member.MemberBorrowedBookIDs.Add(bookId);
            int recordId = _borrowRecords.Count + 1;
            var record = new BorrowRecord(recordId, bookId, memberId);
            await SaveDataAsync();
            return true;
        }

        // Return a book
        public async Task<bool> ReturnBookAsync(int bookId, int memberId)
        {
            var book = _books.FirstOrDefault(b => b.BookID == bookId);
            var member = _members.FirstOrDefault(m => m.MemberID == memberId);
            if (book == null || member == null)
            {
                return false;
            }
            book.BookIsAvailable = true;
            member.MemberBorrowedBookIDs.Remove(bookId);
            var record = _borrowRecords.FirstOrDefault(r => r.BorrowRecordBookID == bookId && r.BorrowRecordMemberID == memberId&&r.BorrowRecordReturnDate==null);
            if (record != null)
            {
                record.BorrowRecordReturnDate = DateTime.Now;
            }
            await SaveDataAsync();
            return true;
        }

        // Get all available books — LINQ
        public List<Book> GetAvailableBooks()
        {
            return _books.Where(b => b.BookIsAvailable).ToList();
        }

        // Search books by title or author — LINQ
        public List<Book> SearchBooks(string query)
        {
             return _books.Where(b => b.BookTitle.ToLower().Contains(query.ToLower()) || b.BookAuthor.ToLower().Contains(query.ToLower())).ToList();
        }

        // Get all books a member is currently borrowing — LINQ
        public List<Book> GetMemberBooks(int memberId)
        {
            var member=_members.FirstOrDefault(m => m.MemberID == memberId);
            if(member==null)
            {
                return new List<Book>();
            }
            var books = _books.Where(b => member.MemberBorrowedBookIDs.Contains(b.BookID)).ToList();
            return books;
        }

        public List<Book> GetAllBooks() => _books;
        public List<Member> GetAllMembers() => _members;
    }
}
