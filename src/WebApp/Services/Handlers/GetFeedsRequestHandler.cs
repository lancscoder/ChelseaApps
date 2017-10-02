using MediatR;
using Microsoft.SyndicationFeed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Responses;
using WebApp.Services.Readers;
using WebApp.Services.Requests;
using WebApp.Services.Stores;

namespace WebApp.Services.Handlers
{
    public class GetFeedsRequestHandler : IAsyncRequestHandler<GetFeedsRequest, List<Feed>>
    {
        private readonly IFeedOptionStore _feedOptionsStore;
        private readonly IRssReader _rssReader;

        public GetFeedsRequestHandler(IFeedOptionStore feedOptionsStore, IRssReader rssReader)
        {
            _feedOptionsStore = feedOptionsStore;
            _rssReader = rssReader;
        }

        public async Task<List<Feed>> Handle(GetFeedsRequest request)
        {
            var feedOptions = _feedOptionsStore.GetFeedOptions();
            var rssTasks = new List<Task<List<SyndicationItem>>>();

            foreach (var id in request.Ids)
            {
                var option = feedOptions.FirstOrDefault(f => f.Id == id);

                if (option == null) continue;

                var task = _rssReader.GetItems(option.Url);

                rssTasks.Add(task);
            }

            await Task.WhenAll(rssTasks);

            var rssItems = new List<SyndicationItem>();
            
            foreach (var completedTask in rssTasks)
            {
                rssItems.AddRange(completedTask.Result);
            }

            return rssItems
                .OrderBy(o => o.Published)
                .Select(s => new Feed
                {
                    Title = s.Title,
                    Description = s.Description
                })
                .ToList();
        }
    }
}
