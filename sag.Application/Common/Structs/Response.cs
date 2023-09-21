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
        Error = new ErrorResponse
        {
            Message = error?.Message,
            Exception = error
        };
    }

    private Response(Exception error)
    {
        Error = new ErrorResponse
        {
            Message = error.Message,
            Exception = error
        };
    }

    public static Response<T> Success(T data)
    {
        return new Response<T>(data, null, null);
    }

    public static Response<T> Success(T data, string? message)
    {
        return new Response<T>(data, message, null);
    }

    public static Response<T> Fail(T data)
    {
        return new Response<T>(data, null, null);
    }

    public static Response<T?> Fail(Exception error)
    {
        return new Response<T?>(default, null, error);
    }
}

public struct ErrorResponse
{
    public string? Message { get; set; }
    public Exception? Exception { get; set; }
}