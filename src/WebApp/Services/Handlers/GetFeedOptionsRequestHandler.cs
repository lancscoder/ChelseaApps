using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Responses;
using WebApp.Services.Requests;
using WebApp.Services.Stores;

namespace WebApp.Services.Handlers
{
    public class GetFeedOptionsRequestHandler : IAsyncRequestHandler<GetFeedOptionsRequest, List<FeedOption>>
    {
        private readonly IFeedOptionStore _feedOptionsStore;

        public GetFeedOptionsRequestHandler(IFeedOptionStore feedOptionsStore)
        {
            _feedOptionsStore = feedOptionsStore;
        }

        public Task<List<FeedOption>> Handle(GetFeedOptionsRequest request)
        {
            var feedOptions = _feedOptionsStore.GetFeedOptions();

            return Task.FromResult(feedOptions.Select(f => new FeedOption
            {
                Id = f.Id,
                Name = f.Name
            }).ToList());
        }
    }
}
