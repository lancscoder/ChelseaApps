using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.SyndicationFeed;
using System.Xml;
using Microsoft.SyndicationFeed.Rss;

namespace WebApp.Services.Readers
{
    public class RssReader : IRssReader
    {
        public async Task<List<SyndicationItem>> GetItems(string url)
        {
            var items = new List<SyndicationItem>();

            using (var xmlReader = XmlReader.Create(url, new XmlReaderSettings { Async = true }))
            {
                var feedReader = new RssFeedReader(xmlReader);

                while (await feedReader.Read())
                {
                    switch (feedReader.ElementType)
                    {
                        case SyndicationElementType.Item:
                            ISyndicationItem item = await feedReader.ReadItem();
                            items.Add(new SyndicationItem(item));
                            break;
                        default:
                            break;
                    }
                }
            }

            return items;
        }
    }
}
