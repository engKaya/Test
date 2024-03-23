
using LogManagerAsBackgroundService.Classes;
using System.Threading.Channels;

namespace LogManagerAsBackgroundService.Implements
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<LogItem> _workItems;
        private readonly SemaphoreSlim _signal = new(0);
        public BackgroundTaskQueue()
        {
            // Capacity should be set based on the expected application load and
            // number of concurrent threads accessing the queue.            
            // BoundedChannelFullMode.Wait will cause calls to WriteAsync() to return a task,
            // which completes only when space became available. This leads to backpressure,
            // in case too many publishers/calls start accumulating.
            var options = new BoundedChannelOptions(250)
            {
                FullMode = BoundedChannelFullMode.Wait
            };
            _workItems = Channel.CreateBounded<LogItem>(options);
        }
        public async ValueTask<LogItem> DequeueAsync(
            CancellationToken cancellationToken)
        {
            var workItem = await _workItems.Reader.ReadAsync(cancellationToken);

            return workItem;
        }

        public async ValueTask QueueBackgroundWorkItemAsync(
            LogItem workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }


            await _workItems.Writer.WriteAsync(workItem);
        }
    }

    public interface IBackgroundTaskQueue
    {
        public ValueTask QueueBackgroundWorkItemAsync(
           LogItem workItem);

        public ValueTask<LogItem> DequeueAsync(
             CancellationToken cancellationToken);
    }


    public class LogWorker : BackgroundService
    {
        private readonly ILogger<LogWorker> _logger;
        private readonly IBackgroundTaskQueue _taskQueue;

        public LogWorker(ILogger<LogWorker> logger, IBackgroundTaskQueue taskQueue)
        {
            _logger = logger;
            _taskQueue = taskQueue;
        }       

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("LogWorker is starting.");

            stoppingToken.Register(() =>
                _logger.LogInformation("LogWorker background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await _taskQueue.DequeueAsync(stoppingToken);

                try
                {
                    var randomExcepetion = new Random().Next(1, 10) % 2 == 0;
                    if (randomExcepetion)
                    {
                        throw new Exception("Random exception occurred.");
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"LogWorker is processing {workItem.Message}.");
                    Console.WriteLine($"LogWorker is processing {workItem.CreatedAt}.");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Error occurred executing {WorkItem}.", nameof(workItem));
                }
            }

            _logger.LogInformation("LogWorker is stopping.");
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("LogWorker is stopping.");

            await base.StopAsync(cancellationToken);
        }
    }

}
