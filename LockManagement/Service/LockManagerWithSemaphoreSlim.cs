using System.Collections.Concurrent;
using LockManagement.Interfaces; 

namespace LockManagement.Service
{
    public class LockManagerWithSemaphoreSlim : ILockManager
    {
        private static readonly ConcurrentDictionary<string, Semaphore> _lockObjects = new ConcurrentDictionary<string, Semaphore>();
        public object LockCustomer(string customerNumber, string cardNumber, Thread? thread = null)
        {
            var id = $"{customerNumber.GetHashCode()}{cardNumber}";
            Console.WriteLine($"Entering Lock for thread {thread?.Name} by {id}");
            _lockObjects.TryAdd(id, new Semaphore(1, 1));
            return _lockObjects[id];
        }

        public void UnlockCustomer(string customerNumber, string cardNumber, Thread? thread = null)
        {
            var id = $"{customerNumber}{cardNumber}";
            Console.WriteLine($"Exiting Lock for thread {thread?.Name} by {id}");
            _lockObjects.TryRemove(id, out _); 
        }
    }
}
