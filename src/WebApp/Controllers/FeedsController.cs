using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using WebApp.Services.Requests;
using System.Linq;
using System;

namespace WebApp.Controllers
{
    [Route("api/feeds")]
    public class FeedsController : Controller
    {
        private readonly IMediator _mediator;

        public FeedsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Post(string ids)
        {
            var feedIds = ids.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToArray();
            var request = new GetFeedsRequest() { Ids = feedIds };
            var options = await _mediator.Send(request);

            return Ok(options);
        }
    }
}
