using core.Queue.Manager;
using Microsoft.Extensions.Logging;
using Quartz;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vote.Queue.DatabaseContext;


namespace Vote.Queue.Job
{
    [DisallowConcurrentExecution]
    public class SaveVoteJob : IJob
    {
        private readonly IQueueManager _queueManager;
        private readonly ILogger<SaveVoteJob> _logger;
        public Thread SaveVoteThread; 
        public SaveVoteJob(
            IQueueManager queueManager,
            ILogger<SaveVoteJob> logger)
        {
            _logger = logger;
            _queueManager = queueManager;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var votesToSave = _queueManager
                           .GetAllVotesAndClearQueue()
                           .Select(o => new VoteEntity() { CandidateId = o.CandidateId, UserId = o.UserId });
            if(votesToSave == null || votesToSave.Count() <= 0)
                return Task.CompletedTask;

            using (var db = new VoteContext())
            {
                db.Vote.AddRange(votesToSave);
                db.SaveChanges();
            }
            _logger.LogInformation("Saved votes");
            return Task.CompletedTask;
        }
    }
}
