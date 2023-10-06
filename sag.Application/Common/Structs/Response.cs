using System.Runtime.InteropServices.JavaScript;

namespace sag.Application.Common.Structs;

public struct Response<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public ErrorResponse? Error { get; set; }

    private Response(T data, string? message, Exception? error)
    {
        Message = message;
        Data = data;
        Error = error is null
            ? null
            : new ErrorResponse
            {
                Message = error?.Message,
                Exception = error
            };
    }

    private Response(Exception error) =>
        Error = new ErrorResponse
        {
            Message = error.Message,
            Exception = error
        };

    public static Response<T> Success(T data) => new(data, null, null);

    public static Response<T> Success(T data, string? message) => new(data, message, null);

    public static Response<T> Fail(T data) => new(data, null, null);

    public static Response<T?> Fail(Exception? error) => new(default, null, error);
}