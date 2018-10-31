using RSSApp.DL;
using RSSApp.Exeptions;
using RSSApp.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RSSApp.BLL {
    class FeedsController {
        private static List<RSSFeed> PodcastFeeds = new List<RSSFeed>();
        private static CancellationTokenSource tokenSource = new CancellationTokenSource();

        public delegate void UpdatedFeedEventHandler(object sender, EventArgs e);

        // Declare the event.
        public static event UpdatedFeedEventHandler UpdatedFeed;

        public static async Task AddFeedAsync(Uri uri, Category category) {
            if (category == null) {
                throw new ArgumentException();
            }
            try {
                var feed = new RSSFetcher(uri).Fetch();
                feed.Category = category;
                await AddFeedAsync(feed);
            } catch (ValidationExeption ex) {
                throw ex;
            } 
        }
        public static async Task AddFeedAsync(RSSFeed feed) {
            try {
                Validation.ValidateFeed(feed);
            } catch (ValidationExeption ex) {
                throw ex;
            }
            
            PodcastFeeds.Add(feed);
            CancellationToken token = tokenSource.Token;
            await Task.Run(async () => {
                await UpdateFeedPodcastsInterervalStart(token, feed);
            });
        }
        public static async Task UpdateFeedPodcastsInterervalStart(CancellationToken cancellationToken, RSSFeed feed) {
            while (true) {
                await Task.Delay(feed.UpdateInterval);
                await UpdateFeedPodcasts(feed);
                OnUpdatedFeed();

                if (cancellationToken.IsCancellationRequested)
                    break;

            }
        }
        public static void RemoveFeed(RSSFeed feed) {
            PodcastFeeds.Remove(feed);
        }
        public static void CleanUp() {
            tokenSource.Cancel();
        }

        public static async Task UpdateFeedPodcasts(RSSFeed feed) {
            var fetcher = new RSSFetcher(feed.URI);

            var onlineFeed = fetcher.Fetch();

            feed.Podcasts = onlineFeed.Podcasts;
        }
        

        public static List<RSSFeed> GetFeeds() {
            return PodcastFeeds;
        }


        static void OnUpdatedFeed() {
            if (UpdatedFeed != null) {
                UpdatedFeed(typeof(FeedsController), EventArgs.Empty);
            }
        }
    }
}
