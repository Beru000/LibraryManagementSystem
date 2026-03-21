using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Filepath;
using LibraryManagementSystem.Services.Logics.Base;

namespace LibraryManagementSystem.Services.Logics
{
    public class EditMember
    {
        #region Properties
        private readonly LogicResultBase _result = new LogicResultBase();

        private readonly FileService _fileService;
        private readonly int _memberID;
        private readonly string _memberName;
        private readonly string _memberEmail;
        private readonly List<int>? _memberBorrowedBookIDs;

        private List<Member> _members = new List<Member>();
        private Member? _member;
        #endregion

        #region Constructors
        public EditMember(FileService fileService, int memberID, string memberName = "", string memberEmail = "", List<int>? memberBorrowedBookIDs = null)
        {
            _fileService = fileService;
            _memberID = memberID;
            _memberName = memberName;
            _memberEmail = memberEmail;
            _memberBorrowedBookIDs = memberBorrowedBookIDs;
        }
        #endregion

        #region Methods
        public async Task<LogicResultBase> Execute()
        {
            await GetMember();
            if (!_result.IsError)
            {
                await EditMemberProperties();
            }

            return _result;
        }

        async Task GetMember()
        {
            _members = await _fileService.LoadAsync<Member>(Constants.FilePaths.Members);
            var memberToEdit = _members.FirstOrDefault(member => member.MemberID == _memberID);
            if (memberToEdit == null)
            {
                _result.IsError = true;
                _result.ErrorMessage = $"Member with ID {_memberID} can't be found";
            }
            else
            {
                _member = memberToEdit;
            }
        }

        async Task EditMemberProperties()
        {
            if (_member != null)
            {
                _member.MemberName = string.IsNullOrEmpty(_memberName) ? _member.MemberName : _memberName;
                _member.MemberEmail = string.IsNullOrEmpty(_memberEmail) ? _member.MemberEmail : _memberEmail;
                _member.MemberBorrowedBookIDs = _memberBorrowedBookIDs ?? _member.MemberBorrowedBookIDs;

                await _fileService.SaveAsync(Constants.FilePaths.Members, _members);
            }
        }
        #endregion
    }
}
