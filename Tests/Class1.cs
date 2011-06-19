using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToTwitter;
using Xunit;

namespace Tests
{
    public class Class1
    {
        [Fact]
        public void FactMethodName()
        {
            var twitterCtx = new TwitterContext();

            var publicTweets =
                from tweet in twitterCtx.Status
                where tweet.Type == StatusType.Public
                select tweet;

            var t = from publicTweet in publicTweets 
                let user = publicTweet.User 
            select new {user.ProfileImageUrl, user.Name, publicTweet.Text};

            t.ToList().ForEach(Console.WriteLine);
//
//            publicTweets.ToList().ForEach(
//                tweet => Console.WriteLine(
//                    "User Name: {0}, Tweet: {1}",
//                    tweet.User.Name,
//                    tweet.Text));
        }
    }
}
