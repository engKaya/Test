namespace Test.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> MakeTransaction(string customerNumber, string CardNumber, decimal transaction, Thread? thread = null); 

        void WriteCustomerBalances();
    }
}
