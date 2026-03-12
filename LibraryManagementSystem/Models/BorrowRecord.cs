namespace LibraryManagementSystem.Models
{
    public class BorrowRecord
    {
        public int Id { get; set; } 
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public BorrowRecord(int id, int bookId, int memberId)
        {
            Id = id;
            BookId = bookId;
            MemberId = memberId;
            BorrowDate = DateTime.Now;
            ReturnDate = null;
        }
    }
}
