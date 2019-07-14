using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vote.Core.Queue.Manager
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

        public List<Vote> GetAllVotesAndClearQueue()
        {
            var allVotesQueue = new List<Vote>();           
            lock (_votes)
            {
                 allVotesQueue = _votes.ToList();
                _votes.Clear();
            }
            return allVotesQueue;
        }
    }
}
