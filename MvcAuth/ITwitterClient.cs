using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAuth
{
   public interface ITwitterClient
    {
        List<TweetDetails> GetTweets(string TwitterName, string TweetCount, string HashTag);
        string PostTweet(string txtTweet);
        string ReTweet(string name);
    }
}
