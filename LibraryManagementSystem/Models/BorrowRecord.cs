namespace LibraryManagementSystem.Models
{
    public class BorrowRecord
    {
        public int BorrowRecordBookID { get; set; }
        public int BorrowRecordMemberID { get; set; }
        public DateTime BorrowRecordCreateDate { get; set; }
        public DateTime? BorrowRecordReturnDate { get; set; }

        public BorrowRecord(int bookId, int memberId)
        {
            BorrowRecordBookID = bookId;
            BorrowRecordMemberID = memberId;
            BorrowRecordCreateDate = DateTime.Now;
            BorrowRecordReturnDate = null;
        }
    }
}
