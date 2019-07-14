using Election.Api.Rest;
using MassTransit;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace core.Queue.Manager
{
    public class QueueManager : IQueueManager
    {      
        private readonly ConcurrentBag<Vote> _votes;
        public QueueManager()
        {
            _votes = new ConcurrentBag<Vote>();
        }


        public Task AddVote(Vote vote)
        {
            _votes.Add(vote);

            return Task.CompletedTask;
        }

        public List<Vote> GetQueue()
        {
            var allVoteQueue = new List<Vote>();           
            lock (_votes)
            {
                 allVoteQueue = _votes.ToList();
                _votes.Clear();
            }
            return allVoteQueue;
        }
    }
}
