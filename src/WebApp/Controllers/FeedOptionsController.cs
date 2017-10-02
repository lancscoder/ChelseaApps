using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services.Requests;
using MediatR;

namespace WebApp.Controllers
{

    [Route("api/feeds/options")]
    public class FeedOptionsController : Controller
    {
        private readonly IMediator _mediator;

        public FeedOptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new GetFeedOptionsRequest();
            var options = await _mediator.Send(request);

            return Ok(options);
        }
    }
}
