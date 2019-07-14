
using System.ComponentModel.DataAnnotations;

namespace Election.Api.Rest.Contracts
{
    public class VoteModel
    {
        [Required]
        public string CandidateId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
