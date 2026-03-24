namespace LibraryManagementSystem.Models
{
    public class Member
    {
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
        public List<int> MemberBorrowedBookIDs { get; set; }

        public Member(int memberID, string memberName, string memberEmail)
        {
            MemberID = memberID;
            MemberName = memberName;
            MemberEmail = memberEmail;
            MemberBorrowedBookIDs = new List<int>();
        }
    }
}
