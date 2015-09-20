using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TryWpf1
{
    class ContentFetcher
    {
        public List<string> Feeds { get; set; }

        //We fetch from Voat as a default
        public ContentFetcher() {
            var lstFeeds = new List<string>();
            //http://voat.co/rss has some kind of anti-DDoS mechanism that prevents us from reading
            lstFeeds.Add("https://www.reddit.com/.rss");
            Feeds = lstFeeds;
        }
        public ContentFetcher(List<string> aFeeds) { Feeds = aFeeds; }

        public List<SyndicationItem> GoFetch() {
            List<SyndicationItem> lstItems = new List<SyndicationItem>();
            foreach (var iterFeed in Feeds)
            {
                var rssReader = XmlReader.Create(iterFeed);
                var synFeed = SyndicationFeed.Load(rssReader);
                lstItems.AddRange(synFeed.Items);
            }
            return lstItems;
        }
    }
}
