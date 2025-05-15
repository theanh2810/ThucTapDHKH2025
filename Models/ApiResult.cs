using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HeThongDatBan.Models
{
    public class ApiResult<T>   
    {
        public bool Success { get; set; } // Trạng thái API (true/false)
        public string Message { get; set; } // Mô tả lỗi hoặc thành công
        public T? Data { get; set; } // Dữ liệu trả về (có thể null)
        public int? TotalRecords { get; set; } // Dùng cho phân trang

        //trả về thành công
        public ApiResult(bool success, string message, T? data, int? totalRecords = null)
        {
            Success = success;
            Message = message;
            Data = data;
            TotalRecords = totalRecords;
        }


        // Trả về lỗi
        public ApiResult(bool success, string message)
        {
            Success = success;
            Message = message;
            Data = default;
            TotalRecords = 0;
        }

    }
}
