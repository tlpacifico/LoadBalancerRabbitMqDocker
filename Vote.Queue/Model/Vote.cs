
namespace Vote.Queue
{
    public class Vote
    {
        public Vote(string candidateId, string userId)
        {
            CandidateId = candidateId;
            UserId = userId;
        }
        public string CandidateId { get; set; }
        public string UserId { get; set; }
    }
}
