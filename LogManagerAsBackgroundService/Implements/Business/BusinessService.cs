﻿using LogManagerAsBackgroundService.Classes;
using LogManagerAsBackgroundService.Interfaces.Business;

namespace LogManagerAsBackgroundService.Implements.Business
{
    public class BusinessService : IBusinessService
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        public BusinessService(IBackgroundTaskQueue taskQueue)
        {
                _taskQueue = taskQueue;
        }
        public void DoWork()
        {
            for (int i = 0; i < 50; i++)
            {
                var transaction = GenerateTransaction();
                _taskQueue.QueueBackgroundWorkItemAsync(new LogItem
                {
                    Message = $"Transaction generated: {transaction.TransactionId}",
                    CreatedAt = DateTime.Now
                });
                ProcessTransaction(transaction); 
            }
        }

        private TransactionClass GenerateTransaction()
        {
            var transaction = new TransactionClass();
            int waitTime = new Random().Next(100, 500);
            Task.Delay(waitTime).Wait();
            return transaction;
        }

        List<TransactionClass> processesedTransactions = new List<TransactionClass>();

        private void ProcessTransaction(TransactionClass transaction)
        {
            int waitTime = new Random().Next(100, 200);
            Task.Delay(waitTime).Wait();
            processesedTransactions.Add(transaction);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Transaction processed: {transaction.TransactionId}");
            Console.ResetColor();
            _taskQueue.QueueBackgroundWorkItemAsync(new LogItem
            {
                Message = $"Transaction processed: {transaction.TransactionId}",
                CreatedAt = DateTime.Now
            });
        }
    }
}
