namespace sag.Application.Common.Structs;

public struct ErrorResponse
{
    public string? Message { get; set; }
    public Exception? Exception { get; set; }
}