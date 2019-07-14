
using Election.Api.Rest;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace core.Queue.Manager
{
    public interface IQueueManager
    {
        Task AddVote(Vote vote);

        List<Vote> GetAllVotesAndClearQueue();
    }
}
