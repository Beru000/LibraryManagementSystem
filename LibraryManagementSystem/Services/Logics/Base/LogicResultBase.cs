namespace LibraryManagementSystem.Services.Logics.Base
{
    public class LogicResultBase
    {
        public bool IsError { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
