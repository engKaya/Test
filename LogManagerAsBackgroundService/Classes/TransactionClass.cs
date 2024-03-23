namespace LogManagerAsBackgroundService.Classes
{
    public class TransactionClass
    {
        public Guid TransactionId { get; set; } = Guid.Empty;
        public TransactionType TransactionType { get; set; } = TransactionType.None;
        public string TransactionDate { get; set; } = string.Empty;
        public string TransactionAmount { get; set; } = string.Empty;

        public TransactionClass()
        {
                TransactionId = Guid.NewGuid();
                TransactionType = (TransactionType)TransactionType.GetType().GetRandomEnumValue();
                TransactionDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                TransactionAmount = new Random().Next(1, 1000).ToString();
        }
    }

    public enum TransactionType
    {
        None,
        Credit,
        Debit
    }

    public static class EnumExtensions
    {
        public static Enum GetRandomEnumValue(this Type t) => Enum.GetValues(t)
                .OfType<Enum>()
                .OrderBy(e => Guid.NewGuid())
                .FirstOrDefault();
    }

}
