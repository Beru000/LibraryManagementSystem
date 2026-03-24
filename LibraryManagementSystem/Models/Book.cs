namespace LibraryManagementSystem.Models
{
    public class Book
    {
        // ctrl + r + r

        public int BookID { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public string BookGenre { get; set; }   
        public bool BookIsAvailable { get; set; }

        public Book(int bookID, string bookTitle, string bookAuthor, string bookGenre)
        {
            BookID = bookID;
            BookTitle = bookTitle;
            BookAuthor = bookAuthor;
            BookGenre = bookGenre;
            BookIsAvailable = true;
        }
    }
}
