using System.Collections.Concurrent;
using Test.Classes;
using Test.Interfaces;

namespace Test.Service
{
    internal class LockManager: ILockManager
    {
        private static readonly ConcurrentDictionary<string, object> _lockObjects = new ConcurrentDictionary<string, object>();
        public object LockCustomer(string customerNumber, string cardNumber, Thread? thread = null)
        {
            var lockObject = $"{customerNumber}{cardNumber}";
            Console.WriteLine($"Entering Lock for thread {thread?.Name} by {lockObject}"); 
            _lockObjects.TryAdd(lockObject, new object());
            return _lockObjects[lockObject];
        }

        public void UnlockCustomer(string customerNumber, string cardNumber, Thread? thread = null)
        {
            var lockObject = $"{customerNumber}{cardNumber}";
            Console.WriteLine($"Exiting Lock for thread {thread?.Name} by {lockObject}");
            _lockObjects.TryRemove(lockObject, out _);
        }
    }
}
