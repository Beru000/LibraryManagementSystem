using LibraryManagementSystem.Services;
using LibraryManagementSystem.Services.Logics;

class Program
{
    static async Task Main()
    {
        var fileService = new FileService();
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n=== Library Management System ===");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Add Member");
            Console.WriteLine("3. Borrow Book");
            Console.WriteLine("4. Return Book");
            Console.WriteLine("5. View Available Books");
            Console.WriteLine("6. Search Books");
            Console.WriteLine("7. View Member's Books");
            Console.WriteLine("8. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    Console.Write("Enter book title: ");
                    var title = Console.ReadLine();
                    Console.Write("Enter book author: ");
                    var author = Console.ReadLine();
                    Console.Write("Enter book genre: ");
                    var genre = Console.ReadLine();
                    var addBook = new AddBook(fileService);
                    await addBook.AddBookAsync(title ?? string.Empty, author ?? string.Empty, genre ?? string.Empty);
                    Console.WriteLine("\nBook added successfully!");
                    break;

                case "2":
                    Console.Write("Enter member name: ");
                    var name = Console.ReadLine();
                    Console.Write("Enter member email: ");
                    var email = Console.ReadLine();
                    var addMember = new AddMember(fileService);
                    await addMember.AddMemberAsync(name ?? string.Empty, email ?? string.Empty);
                    Console.WriteLine("\nMember added successfully!");
                    break;

                case "3":
                    Console.WriteLine("Enter book ID to borrow: ");
                    int bookId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter member ID: ");
                    int memberId = Convert.ToInt32(Console.ReadLine());
                    var borrowBook = new BorrowBook(fileService, bookId, memberId);
                    var borrowResult = await borrowBook.Execute();
                    if (borrowResult.IsError)
                    {
                        Console.WriteLine($"\nError: {borrowResult.ErrorMessage}");
                    }
                    else
                    {
                        Console.WriteLine("\nBook borrowed successfully!");
                    }
                    break;

                case "4":
                    Console.WriteLine("Enter book ID to return: ");
                    var returnBookId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter member ID: ");
                    var returnMemberId = Convert.ToInt32(Console.ReadLine());
                    var returnBook = new ReturnBook(fileService, returnBookId, returnMemberId);
                    var returnResult = await returnBook.Execute();
                    if (returnResult.IsError)
                    {
                        Console.WriteLine($"\nError: {returnResult.ErrorMessage}");
                    }
                    else
                    {
                        Console.WriteLine("\nBook returned successfully!");

                    }
                    break;

                case "5":
                    var library = new GetAvailableBooks(fileService);
                    var availableBooks = await library.GetAvailableBooksAsync();
                    if (!availableBooks.Any())
                    {
                        Console.WriteLine("\nNo available books found.");
                        break;
                    }
                    Console.WriteLine("\n=== Available Books ===");
                    foreach (var book in availableBooks)
                    {
                        Console.WriteLine($"ID: {book.BookID}, Title: {book.BookTitle}, Author: {book.BookAuthor}");
                    }
                    break;

                case "6":
                    Console.Write("Enter search query (title, author, or genre): ");
                    var searchQuery = Console.ReadLine() ?? string.Empty;
                    var searchLogic = new SearchBooks(fileService);
                    var searchResults = await searchLogic.SearchBooksAsync(searchQuery);
                    if (!searchResults.Any())
                    {
                        Console.WriteLine("\nNo books found matching your search.");
                        break;
                    }
                    Console.WriteLine("\n=== Search Results ===");
                    foreach (var book in searchResults)
                    {
                        Console.WriteLine($"ID: {book.BookID}, Title: {book.BookTitle}, Author: {book.BookAuthor}, Genre: {book.BookGenre}");
                    }
                    break;

                case "7":
                    Console.Write("Enter member ID: ");
                    var id = Convert.ToInt32(Console.ReadLine());
                    var memberBooksLogic = new GetMemberBooks(fileService);
                    var memberBooks = await memberBooksLogic.GetMemberBooksAsync(id);
                    if (!memberBooks.Any())
                    {
                        Console.WriteLine("\nThis member has no borrowed books.");
                        break;
                    }
                    Console.WriteLine("\n=== Member's Borrowed Books ==="); // moved outside loop
                    foreach (var book in memberBooks)
                    {
                        Console.WriteLine($"ID: {book.BookID}, Title: {book.BookTitle}, Author: {book.BookAuthor}");
                    }
                    break;

                case "8":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option, try again!");
                    break;
            }
        }
    }
}