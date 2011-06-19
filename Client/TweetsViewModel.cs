using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LinqToTwitter;

namespace Client
{
    public class TweetsViewModel
    {
        private readonly ObservableCollection<Tweet> tweets = new ObservableCollection<Tweet>();

        public ObservableCollection<Tweet> Tweets
        {
            get { return tweets; }
        }

        static TweetsViewModel()
        {
            Mapper.CreateMap<Status, Tweet>();
        }


        public void Load()
        {
            var synchronizationContext = SynchronizationContext.Current;
            
            Action<Task<IEnumerable<Tweet>>> continuationFunction = t => t.Result.ToList().ForEach(e => { } /*tweets.Add*/);
            Task<IEnumerable<Tweet>>.Factory.StartNew(() =>
                                                          {
                                                              var twitterCtx = new TwitterContext();
                                                              return from tweet in twitterCtx.Status where tweet.Type == StatusType.Public select Mapper.Map<Status, Tweet>(tweet);
                                                          })
                                                          .ContinueWith(t => synchronizationContext.Post(state => continuationFunction(t), null));
        }
    }

    public class Tweet
    {
        public string UserName { get; set; }
        public string UserProfileImageUrl { get; set; }
        public string Text { get; set; }
    }


}
