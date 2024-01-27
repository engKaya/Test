namespace LockManagement.Classes
{
    public class CustomerBalance
    {
        public string CustomerNumber { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; } = 0.0m;
    }
}
