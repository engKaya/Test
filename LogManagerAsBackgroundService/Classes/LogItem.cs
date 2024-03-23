namespace LogManagerAsBackgroundService.Classes
{
    public class LogItem
    {
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } 
        public Exception Exception { get; set; }
    }
}
