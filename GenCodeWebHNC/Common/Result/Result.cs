namespace GenCodeWebHNC.Common
{
    public class Result
    {
        public static Result OK => Ok();

        public static Result UNKNOWN => new Result("98", "Lỗi không xác định");

        public static Result ACCOUNT_INVALID_USERNAME_OR_PASSWORD => new Result("001", "Sai tên hoặc mật khẩu");

        public static Result ACCOUNT_INACTIVE => new Result("002", "Tài khoản chưa được kích hoạt");

        public static Result ACCOUNT_LOCKED => new Result(GetCode(3), "Tài khoản bị khóa");

        public static Result ACCOUNT_INVALID_EMAIL => new Result(GetCode(4), "Email không hợp lệ");

        public static Result ACCOUNT_DUPLICATE_EMAIL => new Result(GetCode(5), "Email đã tồn tại");

        public static Result ACCOUNT_DUPLICATE_USERNAME => new Result(GetCode(6), "Tên người dùng đã tồn tại");

        public static Result ACCOUNT_REJECTED => new Result(GetCode(7), "Tài khoản người dùng bị từ chối");

        public static Result ACCOUNT_NOT_EXISTED => new Result(GetCode(8), "Tài khoản không tồn tại");

        public static Result ACCOUNT_PASSWORD_DOES_NOT_MATCH_POLICY => new Result(GetCode(9), "Mật khẩu không thỏa mãn các yêu cầu cần thiết");

        public static Result ACCOUNT_BLOCKED_PASSWORD => new Result(GetCode(10), "Mật khẩu nằm trong danh sách bị chặn");

        public static Result ACCOUNT_GROUP_NOT_EXISTED => new Result(GetCode(11), "Nhóm tài khoản không tồn tại");

        public static Result ACCOUNT_CANNOT_CREATE => new Result(GetCode(12), "Không tạo được tài khoản");

        public static Result SESSION_NOT_EXISTED => new Result(GetCode(100), "Phiên làm việc không tồn tại");

        public static Result SESSION_EXPIRED => new Result(GetCode(101), "Phiên làm việc đã vượt quá thời gian cho phép");

        public static Result SESSION_INVALID_DATA => new Result(GetCode(102), "Dữ liệu phiên làm việc không hợp lệ");

        public static Result SESSION_IP_CHANGED => new Result(GetCode(103), "Địa chỉ truy cập bị thay đổi");

        public static Result SESSION_CLIENT_INFO_CHANGED => new Result(GetCode(104), "Thông tin của client bị thay đổi");

        public static Result SESSION_ID_EMPTY => new Result(GetCode(105), "Phải cung cấp mã phiên làm việc");

        public static Result SESSION_CANNOT_CREATE_SESSION => new Result(GetCode(106), "Không tạo được phiên làm việc");

        public static Result SECURITY_ACCESS_DENIED => new Result(GetCode(200), "Truy cập bị từ chối");

        public static Result SECURITY_INVALID_IP => new Result(GetCode(201), "Địa chỉ IP của phiên làm việc không hợp lệ");

        public static Result SECURITY_USER_NOT_IN_GROUP => new Result(GetCode(202), "Người dùng chưa được gán nhóm");

        public static Result SECURITY_INVALID_STAFF_CODE => new Result(GetCode(203), "Mã nhân viên của tài khoản này không hợp lệ");

        public static Result SECURITY_CANNOT_IDENTIFY_CLIENT => new Result(GetCode(204), "Không xác định được thông tin thiết bị người dùng");

        public static Result SECURITY_ACCESS_DENIED_BY_IP => new Result(GetCode(205), "Địa chỉ mạng của thiết bị không được phép truy cập hệ thống");

        public static Result SECURITY_ACCESS_DENIED_BY_LOCATION => new Result(GetCode(206), "Thiết bị không được phép truy cập từ địa chỉ này");

        public static Result SECURITY_ACCESS_DENIED_BY_DEVICE => new Result(GetCode(207), "Người dùng chưa được cấp quyền sử dụng thiết bị này");

        public static Result SECURITY_ACCESS_DENIED_BY_POLICY => new Result(GetCode(208), "Truy cập bị từ chối theo chính sách quản lý bảo mật");

        public static Result SECURITY_DEVICE_NOT_REGISTERD => new Result(GetCode(209), "Thiết bị chưa được đăng ký hoặc bị thay đổi phần cứng");

        public static Result SECURITY_UNAUTHORIZED_ACCESS => new Result(GetCode(210), "Truy cập chưa được xác thực");

        public static Result PROTOCOL_INVALID_REQUEST => new Result(GetCode(300), "Yêu cầu không hợp lệ");

        public static Result PROTOCOL_INVALID_DATA => new Result(GetCode(301), "Dữ liệu không hợp lệ");

        public static Result PROTOCOL_INVALID_FUNCTION_ID => new Result(GetCode(302), "Mã hàm không hợp lệ");

        public static Result PROTOCOL_CLIENT_VERSION_NOT_MATCH => new Result(GetCode(303), "Phiên bản phần mềm của bạn không hợp lệ, bạn cần nâng cấp để tiếp tục sử dụng");

        public static Result PROTOCOL_CLIENT_ID_NOT_FOUND => new Result(GetCode(304), "Định danh của phần mềm chưa được đăng ký");

        public static Result PROTOCOL_RESPONSE_NULL_CONTENT => new Result(GetCode(305), "Dữ liệu trả về null");

        public static Result DATA_EXISTED => new Result(GetCode(400), "Dữ liệu đã tồn tại");

        public static Result DATA_NOT_EXISTED => new Result(GetCode(401), "Dữ liệu không tồn tại");

        public static Result DATA_NOT_EXISTED_OR_ACCESS_DENIED => new Result(GetCode(402), "Dữ liệu không tồn tại hoặc đã bị khóa");

        public static Result DATA_DUPLICATED => new Result(GetCode(403), "Dữ liệu bị trùng");

        public static Result DATA_INVALID => new Result(GetCode(404), "Dữ liệu không hợp lệ");

        public static Result DATA_NULL_OR_EMPTY => new Result(GetCode(405), "Dữ liệu không được cung cấp");

        public static Result DATA_CANNOT_CREATE => new Result(GetCode(406), "Không tạo mới được dữ liệu");

        public static Result DATA_CANNOT_UPDATE => new Result(GetCode(407), "Không cập nhật được dữ liệu");

        public static Result DATA_CANNOT_DELETE => new Result(GetCode(408), "Không xóa được dữ liệu");

        public static Result DATA_OUT_OF_MIN_VALUE => new Result(GetCode(409), "Giá trị vượt quá số lớn nhất được quy định");

        public static Result DATA_OUT_OF_MAX_VALUE => new Result(GetCode(410), "Giá trị nhỏ hơn số nhỏ nhất được quy định");

        public static Result DATA_OUT_OF_RANGE => new Result(GetCode(411), "Giá trị nằm ngoài phạm vi được quy định");

        public static Result NETWORK_SERVER_NOT_FOUNDED => new Result(GetCode(500), "Không tìm thấy máy chủ");

        public static Result NETWORK_ACTIVE_SERVER_NOT_AVAILABLE => new Result(GetCode(501), "Không tìm được máy chủ đang hoạt động để kết nối");

        public static Result NETWORK_TIMEOUT => new Result(GetCode(502), "Thời gian thực hiện lệnh quá lâu");

        public static Result NETWORK_ERROR => new Result(GetCode(503), "Lỗi mạng");

        public static Result FILE_CANNOT_CREATE => new Result(GetCode(600), "Không tạo được file");

        public static Result FILE_CANNOT_READ => new Result(GetCode(601), "Không đọc được file");

        public static Result FILE_CANNOT_DELETE => new Result(GetCode(602), "Không xóa được file");

        public static Result FILE_CANNOT_MERGE => new Result(GetCode(603), "Không gộp được file");

        public static Result SYSTEM_FUNCTION_NOT_IMPLEMENTED => new Result(GetCode(700), "Chức năng chưa được cài đặt");

        public static Result SYSTEM_FUNCTION_NOT_INITIALIZED => new Result(GetCode(701), "Tính năng chưa được khởi tạo");

        public static Result SYSTEM_FUNCTION_DISABLED => new Result(GetCode(702), "Tính năng bị tắt");

        public static Result SYSTEM_CANNOT_DECODE => new Result(GetCode(703), "Không giải mã được dữ liệu");

        public static Result SYSTEM_CANNOT_ENCODE => new Result(GetCode(704), "Không mã hóa được dữ liệu");

        public static Result SYSTEM_NO_SETTINGS => new Result(GetCode(705), "Không có cấu hình");

        public static Result SYSTEM_API_FILTER_ISSUE => new Result(GetCode(706), "Có lỗi trong chuỗi bộ lọc API");

        public static Result SYSTEM_COMMAND_ROUTE_NO_MAPPING => new Result(GetCode(707), "Không có cấu hình định tuyến cho yêu cầu");

        public static Result SYSTEM_TIMEOUT => new Result(GetCode(708), "Quá thời gian quy định");

        public static Result REQUEST_HEADER_INVALID_CONTENT_TYPE => new Result(GetCode(800), "Trường ContentType của yêu cầu không hợp lệ");

        public static Result REQUEST_HEADER_INVALID_MEDIA_TYPE => new Result(GetCode(801), "Trường MediaType của yêu cầu không hợp lệ");

        public static Result REQUEST_SIZE_TOO_BIG => new Result(GetCode(802), "Kích thước yêu cầu quá lớn");

        public static Result REQUEST_INVALID_FORMAT => new Result(GetCode(803), "Định dạng của yêu cầu không đúng");

        public static Result REQUEST_INVALID_METADATA => new Result(GetCode(804), "Định dạng mô tả của yêu cầu không đúng");

        public static Result REQUEST_HEADER_INVALID_AUTHORIZATION_CODE => new Result(GetCode(805), "Authorization Code không hợp lệ");

        public static Result PUSHSERVICE_DISABLED => new Result(GetCode(850), "Dịch vụ PUSH không được kích hoạt");

        public static Result PUSHSERVICE_CONNECTION_NOT_FOUND => new Result(GetCode(851), "Không tìm thấy kết nối PUSH");

        public static Result PUSHSERVICE_CONNECTION_EXISTED => new Result(GetCode(852), "Đinh danh kết nối của dịch vụ PUSH đã tồn tại");

        public static Result PUSHSERVICE_SESSION_NOT_FOUND => new Result(GetCode(853), "Không tìm thấy phiên làm việc chỉ định trong kết nối PUSH");

        public static Result PUSHSERVICE_UNAUTHENTICATED => new Result(GetCode(854), "Kết nối PUSH chưa được xác thực");

        public static Result PUSHSERVICE_USER_NOT_AVAILABLE => new Result(GetCode(855), "Người dùng PUSH không online");

        public static Result PUSHSERVICE_INVALID_DATA => new Result(GetCode(856), "Định dạng dữ liệu của dịch vụ PUSH không đúng");

        public static Result PUSHSERVICE_UNKNOWN_DATA => new Result(GetCode(857), "Không tìm thấy kiểu dữ liệu của dịch vụ PUSH");

        public static Result QUEUE_DISABLED => new Result(GetCode(900), "Tính năng hàng đợi không được kích hoạt");

        public static Result QUEUE_CANNOT_RECEIVE => new Result(GetCode(901), "Không nhận được dữ liệu từ hàng đợi");

        public static Result QUEUE_CANNOT_SUBCRIBE => new Result(GetCode(902), "Không nhận được dữ liệu quảng bá từ hàng đợi");

        public static Result QUEUE_SUBCRIBER_NOT_FOUND => new Result(GetCode(903), "Cấu hình đăng ký hàng đợi không có hoặc bị tắt");

        public static Result QUEUE_CANNOT_SEND => new Result(GetCode(904), "Không gửi được dữ liệu vào hàng đợi");

        public static Result QUEUE_CANNOT_SEND_NULL_DATA => new Result(GetCode(905), "Không được gửi dữ liệu NULL vào hàng đợi");

        public static Result QUEUE_NO_UNDERLINE_CONNECTION => new Result(GetCode(906), "Không thiết lập được kết nối vào hàng đợi");

        public static Result QUEUE_ALREADY_SUBCRIBED => new Result(GetCode(907), "Đã đăng ký nhận dữ liệu quảng bá từ hàng đợi");

        public string Code { get; set; }

        public string Message { get; set; }

        public string ErrorContent { get; set; }

        public ErrorItems ErrorItems { get; set; }

        private static string GetCode(int code)
        {
            return "SYS" + code.ToString().PadLeft(4, '0');
        }

        public static Result Ok()
        {
            return new Result("00", "Ok");
        }

        public static Result Error(string code, string message)
        {
            return new Result(code, message);
        }

        public static Result Error(string message)
        {
            return new Result("98", message);
        }

        public static Result Exception(string message, Exception ex)
        {
            return new Result("99", message, ex.ToString());
        }

        public static Result<TData> Ok<TData>(TData data, string message = null)
        {
            return new Result<TData>("00", message, data);
        }

        public static Result<TData> Error<TData>(string code, string message, TData data)
        {
            return new Result<TData>(code, message, data);
        }

        public static Result<TData> Error<TData>(string code, string message)
        {
            return new Result<TData>(code, message);
        }

        public static Result<TData> Error<TData>(string message)
        {
            return new Result<TData>("98", message);
        }

        public static Result<TData> ErrorWithData<TData>(string message, TData data)
        {
            return new Result<TData>("98", message, data);
        }

        public static Result<TData> Exception<TData>(string message, Exception ex)
        {
            return new Result<TData>("99", message, ex.ToString());
        }

        public static Result<TData> ToResultWithData<TData>(Result result)
        {
            return new Result<TData>(result.Code, result.Message, result.ErrorContent);
        }

        public Result(string code, string message, string errorContent = null)
        {
            Code = code;
            Message = message;
            ErrorContent = errorContent;
        }

        public Result()
        {
            Code = "00";
        }

        public bool IsOk()
        {
            if (!(Code == "00"))
            {
                return Code == "0";
            }

            return true;
        }

        public bool IsError()
        {
            return !IsOk();
        }

        public bool IsException()
        {
            return Code == "99";
        }

        public bool IsValidate()
        {
            return Code == "100";
        }
    }

    public class Result<TData> : Result/*, IResult<TData>, IResult*/
    {
        public TData Data { get; set; }

        public Result(string code, string message, TData data = default(TData))
            : base(code, message)
        {
            Data = data;
        }

        public Result(string code, string message, string errorContent)
            : base(code, message, errorContent)
        {
        }

        public Result()
        {
        }
    }
}
