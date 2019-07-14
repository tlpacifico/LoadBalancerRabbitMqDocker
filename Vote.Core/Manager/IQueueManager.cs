using System.Collections.Generic;
using System.Threading.Tasks;


namespace Vote.Core.Queue.Manager
{
    public interface IQueueManager
    {
        Task AddVote(Vote vote);

        List<Vote> GetAllVotesAndClearQueue();
    }
}
