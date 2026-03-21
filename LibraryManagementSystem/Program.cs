using LibraryManagementSystem.Services;

class Program
{
    static async Task Main()
    {
        var fileService = new FileService();
        var library = new LibraryService(fileService);
        await library.InitializeAsync();

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
                    // Ask user for title, author, genre
                    // Call library.AddBookAsync()
                    break;

                case "2":
                    // Ask user for name, email
                    // Call library.AddMemberAsync()
                    break;

                case "3":
                    // Ask user for bookId and memberId
                    // Call library.BorrowBookAsync()
                    // Print success or failure message
                    break;

                case "4":
                    // Ask user for bookId and memberId
                    // Call library.ReturnBookAsync()
                    // Print success or failure message
                    break;

                case "5":
                    // Call library.GetAvailableBooks()
                    // Print each book's Id, Title, Author
                    break;

                case "6":
                    // Ask user for search query
                    // Call library.SearchBooks()
                    // Print results
                    break;

                case "7":
                    // Ask user for memberId
                    // Call library.GetMemberBooks()
                    // Print results
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