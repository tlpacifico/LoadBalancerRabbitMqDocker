
using Election.Api.Rest;
using MassTransit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace core.Queue.Manager
{
    public interface IQueueManager
    {
        Task AddVote(Vote vote);

        List<Vote> GetQueue();
    }
}
