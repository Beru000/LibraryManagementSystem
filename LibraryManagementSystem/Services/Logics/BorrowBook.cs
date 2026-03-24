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
            var bookList =await _fileService.LoadAsync<Book>(Constants.FilePaths.Books);
            Book ?borrowedBook = bookList.FirstOrDefault(item => item.BookID == _bookID);
            if (borrowedBook is null)
            {
                _result.IsError = true;
                _result.ErrorMessage = $"Book with ID {_bookID} was not found.";
                return;
            }

            if (!borrowedBook.BookIsAvailable)
            {
                _result.IsError = true;
                _result.ErrorMessage = $"Book with ID {_bookID} is already borrowed.";
                return;
            }

            var borrowRecord = new BorrowRecord(borrowRecordBookID: _bookID, borrowRecordMemberID: _memberID);
            borrowedBook.BookIsAvailable = false;
            _borrowRecords.Add(borrowRecord);

            await _fileService.SaveAsync(Constants.FilePaths.BorrowRecords, _borrowRecords);
            await _fileService.SaveAsync(Constants.FilePaths.Books, bookList);
        }

        async Task EditMemberBorrowedBooks()
        {
            var memberBorrowedBookIDs = _borrowRecords.Where(item => item.BorrowRecordMemberID == _memberID&&item.BorrowRecordReturnDate==null).Select(item => item.BorrowRecordBookID).ToList();

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
