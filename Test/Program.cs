using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Test.Classes;
using Test.Interfaces;
using Test.Service; 
var transactions = new List<Transaction>() {  
    new Transaction { CustomerNumber = "2", CardNumber = "2222222222222222", TransactionAmount = 100 },
    new Transaction { CustomerNumber = "2", CardNumber = "2222222222222222", TransactionAmount = 100 }
};


var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<ILockManager, LockManagerWithSemaphoreSlim>();
serviceCollection.AddSingleton<ITransactionService, TransactionService>();

var taskCount = transactions.Count;
var tasks = new Task[taskCount];

var transactionService = serviceCollection.BuildServiceProvider().GetService<ITransactionService>(); 

for (int i = 0; i < taskCount; i++)
{
    var transaction = transactions[i];
    tasks[i] = Task.Run(() => transactionService.MakeTransaction(transaction.CustomerNumber, transaction.CardNumber, transaction.TransactionAmount, Thread.CurrentThread));
}

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
Task.WaitAll(tasks);
stopwatch.Stop();
Console.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds} ms");
transactionService.WriteCustomerBalances();

Console.ReadLine();



