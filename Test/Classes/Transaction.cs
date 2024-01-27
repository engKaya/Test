namespace Test.Classes
{
    public class Transaction
    {
        public string CustomerNumber { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public decimal TransactionAmount { get; set; } = 0.0m;
    }
}
