namespace GenCodeWebHNC.Common
{
    public class ErrorItem
    {
        public string Key { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
    }

    public interface IResult
    {
        string Code { get; set; }

        string Message { get; set; }

        List<ErrorItem> ErrorItems { get; set; }

        string ErrorContent { get; set; }

        bool IsOk();

        bool IsError();

        bool IsException();

        bool IsValidate();
    }

    public class ErrorItems : List<ErrorItem> { }
}
