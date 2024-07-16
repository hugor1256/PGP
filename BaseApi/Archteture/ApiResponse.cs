namespace PGP.Archteture;

public class ApiResponse<T>
{
    public T Data { get; set; }
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public bool ShowMessage { get; set; }

    public static ApiResponse<T> Fail(string errorMessage, bool showMessage = true)
    {
        return new ApiResponse<T> { Succeeded = false, Message = errorMessage, ShowMessage = showMessage};
    }

    public static ApiResponse<T> Success(T data,string? message = null, bool showMessage = true)
    {
        return new ApiResponse<T> { Succeeded = true, Data = data, Message = message, ShowMessage = showMessage };
    }
}