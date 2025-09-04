namespace DatingApi.Common.Response
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Error {  get; set; }
        public static ApiResponse<T> SuccessResponse(T? Data)
        {
            return new ApiResponse<T> { Data = Data,IsSuccess=true };
        }
        public static ApiResponse<T> ErrorResponse(string? Error)
        {
            return new ApiResponse<T> { Error = Error, IsSuccess = false };
        }
    }
}
