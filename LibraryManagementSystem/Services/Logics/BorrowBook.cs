using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Filepath;
using LibraryManagementSystem.Services.Logics.Base;

namespace LibraryManagementSystem.Services.Logics
{
    public class BorrowBook
    {
        #region Properties
        private readonly LogicResultBase _result = new LogicResultBase();

        private readonly FileService _fileService;
        private readonly int _bookID;
        private readonly int _memberID;

        private List<BorrowRecord> _borrowRecords = new List<BorrowRecord>();
        #endregion

        #region Constructors
        public BorrowBook(FileService fileService, int bookID, int memberID)
        {
            _fileService = fileService;
            _bookID = bookID;
            _memberID = memberID;
        }
        #endregion

        #region Methods
        public async Task<LogicResultBase> Execute()
        {
            await CreateBorrowRecord();
            if (!_result.IsError)
            {
                await EditMemberBorrowedBooks();
            }

            return _result;
        }

        async Task CreateBorrowRecord()
        {
            _borrowRecords = await _fileService.LoadAsync<BorrowRecord>(Constants.FilePaths.BorrowRecords);

            var borrowRecord = new BorrowRecord(bookId: _bookID, memberId: _memberID);
            _borrowRecords.Add(borrowRecord);

            await _fileService.SaveAsync(Constants.FilePaths.BorrowRecords, _borrowRecords);
        }

        async Task EditMemberBorrowedBooks()
        {
            var memberBorrowedBookIDs = _borrowRecords.Where(item => item.BorrowRecordMemberID == _memberID).Select(item => item.BorrowRecordBookID).ToList();

            var editMemberLogic = new EditMember(
                fileService: _fileService,
                memberID: _memberID,
                memberBorrowedBookIDs: memberBorrowedBookIDs
            );

            var editMemberResult = await editMemberLogic.Execute();

            if (editMemberResult.IsError)
            {
                _result.IsError = true;
                _result.ErrorMessage = editMemberResult.ErrorMessage;
            }
        }
        #endregion
    }
}
