using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Domain;

namespace WebApp.Services.Stores
{
    public class FeedOptionStore : IFeedOptionStore
    {
        public List<FeedOption> GetFeedOptions()
        {
            return new List<FeedOption>
            {
                new FeedOption { Id = 1, Name = "BBC News", Url = "http://feeds.bbci.co.uk/news/uk/rss.xml"},
                new FeedOption { Id = 2, Name = "BBC Technology News", Url = "http://feeds.bbci.co.uk/news/technology/rss.xml" },
                new FeedOption { Id = 3, Name = "Reuters UK News", Url = "http://feeds.reuters.com/reuters/UKdomesticNews?format=xml" },
                new FeedOption { Id = 4, Name = "Reuters Technology News", Url = "http://feeds.reuters.com/reuters/technologyNews?format=xml" },
            };
        }
    }
}
