namespace Vote.Queue.DatabaseContext
{
    public class VoteEntity
    {
        public int Id { get; set; }
        public string CandidateId { get; set; }
        public string UserId { get; set; }
    }
}
