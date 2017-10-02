using System.Collections.Generic;
using WebApp.Models.Domain;

namespace WebApp.Services.Stores
{
    public interface IFeedOptionStore
    {
        List<FeedOption> GetFeedOptions();
    }
}
