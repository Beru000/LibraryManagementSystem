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
            int bookId= _books.Count + 1; // Generate new Id
            Book book=new Book(bookId, title, author, genre);
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

            // 1. Find the book using LINQ (FirstOrDefault)
            // 2. Find the member using LINQ (FirstOrDefault)
            // 3. If book or member not found, return false
            // 4. If book is not available, return false
            // 5. Set book.IsAvailable = false
            // 6. Add bookId to member.BorrowedBookIds
            // 7. Create a new BorrowRecord and add to _borrowRecords
            // 8. Save data and return true
        }

        // Return a book
        public async Task<bool> ReturnBookAsync(int bookId, int memberId)
        {
            // 1. Find the book and member using LINQ
            // 2. If either not found, return false
            // 3. Set book.IsAvailable = true
            // 4. Remove bookId from member.BorrowedBookIds
            // 5. Find the borrow record and set ReturnDate = DateTime.Now
            // 6. Save data and return true
        }

        // Get all available books — LINQ
        public List<Book> GetAvailableBooks()
        {
            // return only books where IsAvailable is true
        }

        // Search books by title or author — LINQ
        public List<Book> SearchBooks(string query)
        {
            // return books where Title OR Author contains the query
            // hint: use .Contains() and ignore case with .ToLower()
        }

        // Get all books a member is currently borrowing — LINQ
        public List<Book> GetMemberBooks(int memberId)
        {
            // 1. Find the member
            // 2. Return books where Id is in member.BorrowedBookIds
            // hint: use .Contains()
        }

        public List<Book> GetAllBooks() => _books;
        public List<Member> GetAllMembers() => _members;
    }
}
