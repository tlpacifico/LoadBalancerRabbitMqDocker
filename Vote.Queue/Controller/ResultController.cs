using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vote.Queue.DatabaseContext;

namespace Vote.Queue.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController: ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (var db = new VoteContext())
            {
                return Ok(db.Vote.ToList());
            }
        }
    }
}
