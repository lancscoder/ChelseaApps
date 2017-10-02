using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.SyndicationFeed;

namespace WebApp.Services.Readers
{
    public interface IRssReader
    {
        Task<List<SyndicationItem>> GetItems(string url);
    }
}
