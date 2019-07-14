using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vote.Queue.DatabaseContext;

namespace Vote.Queue
{
    public static class MappingHelper
    {
        public static VoteEntity ToEntity(this Vote obj) =>
               obj == null ? null :
            new VoteEntity
            {
                CandidateId = obj.CandidateId,
                UserId = obj.UserId
            };

    }
}
