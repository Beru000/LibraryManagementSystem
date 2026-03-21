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

        public Book(int id, string title, string author, string genre)
        {
            BookID = id;
            BookTitle = title;
            BookAuthor = author;
            BookGenre = genre;
            BookIsAvailable = true;
        }
    }
}
