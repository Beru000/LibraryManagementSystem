namespace LibraryManagementSystem.Models
{
    public class BorrowRecord
    {
        public int BorrowRecordBookID { get; set; }
        public int BorrowRecordMemberID { get; set; }
        public DateTime BorrowRecordCreateDate { get; set; }
        public DateTime? BorrowRecordReturnDate { get; set; }

        public BorrowRecord(int borrowRecordBookID, int borrowRecordMemberID)
        {
            BorrowRecordBookID = borrowRecordBookID;
            BorrowRecordMemberID = borrowRecordMemberID;
            BorrowRecordCreateDate = DateTime.Now;
            BorrowRecordReturnDate = null;
        }
    }
}
