namespace CribQuest.Backend.Models;

public class GeneralResponse
{
    public string Message { get; set; }
    public bool Success { get; set; }
    public object Data { get; set; }
}