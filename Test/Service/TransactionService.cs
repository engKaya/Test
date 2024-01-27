using Test.Classes;
using Test.Interfaces;

namespace Test.Service
{
    internal class TransactionService : ITransactionService
    {
        private List<CustomerBalance> customerBalances = new();
        private ILockManager _lockManager;
        public TransactionService(ILockManager lockManager)
        {
            _lockManager = lockManager;
            customerBalances = StaticCustomerRepository.customerBalances;
        }
        public Task<bool> MakeTransaction(string customerNumber, string CardNumber, decimal transaction, Thread? thread = null)
        {
            Console.WriteLine($"Transaction for customer {customerNumber} with card {CardNumber} for {transaction} started. Thread Name: {thread?.Name ?? "Null"}");

            var customerBalance = customerBalances.FirstOrDefault(x => x.CustomerNumber == customerNumber && x.CardNumber == CardNumber);
            var lockObject = _lockManager.LockCustomer(customerNumber, CardNumber, thread) as SemaphoreSlim;
            Console.WriteLine($"Entering Lock for thread {thread?.Name} by {lockObject}");
            lockObject.Wait();
            decimal oldBalance = 0.0m;
            if (customerBalance == null)
            {
                return Task.FromResult(false);
            }
            if (customerBalance.Balance < transaction)
            {
                return Task.FromResult(false);
            }
            oldBalance = customerBalance.Balance;
            customerBalance.Balance -= transaction;
            Console.WriteLine($"Transaction for customer {customerNumber} with card {CardNumber} for {transaction} finished. New Balance: {customerBalance.Balance}, old balance {oldBalance}. Thread Name: {thread?.Name ?? "Null"}");
            lockObject.Release();
            _lockManager.UnlockCustomer(customerNumber, CardNumber, thread);


            return Task.FromResult(true);
        }

        public void WriteCustomerBalances()
        {
            foreach (var customerBalance in customerBalances)
            {
                Console.WriteLine($"Customer Number: {customerBalance.CustomerNumber}, Card Number: {customerBalance.CardNumber}, Balance: {customerBalance.Balance}");
            }
        }
    }
}
