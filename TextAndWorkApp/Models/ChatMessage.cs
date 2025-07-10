namespace TextAndWorkApp.Models;

public class ChatMessage
{
    public string Username { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsFromUser { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}