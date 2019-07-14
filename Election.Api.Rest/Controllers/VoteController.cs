using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Election.Api.Rest.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Election.Api.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly List<string> ElectionCandidated = new List<string>() { "Candidate1", "Candidate2", "Candidate3", "Candidate4", "Candidate5" };
        private readonly IBusControl _bus;
        private readonly RabbitMqOptions _rabbitOptions;
        //private readonly Uri EndPoint = new Uri("vote_queue", UriKind.Relative);

        public VoteController(IBusControl bus, IOptions<RabbitMqOptions> rabbitOptions)
        {
            _bus = bus;
            _rabbitOptions = rabbitOptions.Value;
        }
        [HttpPost]
        public async Task<IActionResult> Post(VoteModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!ElectionCandidated.Contains(model.CandidateId))
                return BadRequest("Invalid candidate");

            Vote vote = new Vote(model.CandidateId, model.UserId);

            Uri uri = new Uri($"{_rabbitOptions.HostName.ToString()}vote_queue");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(vote);

            return Ok("vote was registered");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok($"Api is running in the machine: {Environment.MachineName}");
        }
    }
}
