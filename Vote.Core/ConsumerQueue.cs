using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Vote.Core.Queue.Manager;

namespace Vote.Core
{
    public class ConsumerQueue : IConsumer<Vote>
    {
        private readonly IQueueManager _queueManager;
        private readonly ILogger<ConsumerQueue> _logger;
        public ConsumerQueue(IQueueManager queueManager, ILogger<ConsumerQueue> logger)
        {
            _queueManager = queueManager;
            _logger = logger;
        }
     
        public Task Consume(ConsumeContext<Vote> context)
        {
            _logger.LogInformation("vote received");
            _queueManager.AddVote(context.Message);
            return Task.CompletedTask;
        }
    }
}
