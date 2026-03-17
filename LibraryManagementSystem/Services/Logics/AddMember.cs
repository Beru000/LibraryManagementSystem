using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Filepath;

namespace LibraryManagementSystem.Services.Logics
{
    internal class AddMember
    {
        private List<Member> _members = new List<Member>();
        private readonly FileService _fileService;

        public AddMember(FileService fileService)
        {
            _fileService = fileService;
        }

        public async Task AddBookAsync(string name, string email)
        {
            _members = await _fileService.LoadAsync<Member>(Constants.MemberFilePath);
            int memberId = _members.Count + 1;
            Member member = new Member(memberId, name, email);
            _members.Add(member);
            await _fileService.SaveAsync(Constants.MemberFilePath, _members);
        }
    }
}
