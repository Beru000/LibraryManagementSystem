namespace LibraryManagementSystem.Models
{
    public class Member
    {
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
        public List<int> MemberBorrowedBookIDs { get; set; }

        public Member(int id, string name, string email)
        {
            MemberID = id;
            MemberName = name;
            MemberEmail = email;
            MemberBorrowedBookIDs = new List<int>();
            
        }
    }
}
