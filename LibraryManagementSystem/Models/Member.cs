namespace LibraryManagementSystem.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<int> BorrowedBookIds { get; set; }

        public Member(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            BorrowedBookIds = new List<int>();
            
        }
    }
}
