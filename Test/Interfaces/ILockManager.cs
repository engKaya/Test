namespace Test.Interfaces
{
    internal interface ILockManager
    {
        object LockCustomer(string customerNumber, string cardNumber, Thread? thread = null); 
        void UnlockCustomer(string customerNumber, string cardNumber, Thread? thread = null); 
    }
}
