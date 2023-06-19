namespace MagicVillaApi.Models;

public class Response
{
    public string Status { get; set; } = string.Empty;
    public object? Data { get; set; }
    public string Message { get; set; } = string.Empty;
}