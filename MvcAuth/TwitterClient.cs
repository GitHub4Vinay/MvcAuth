using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using TweetSharp;

namespace MvcAuth
{
    public class TwitterClient : ITwitterClient
    {

        TwitterService service;
        public TwitterClient()
        {
            string consumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString();

            string consumerSecret = ConfigurationManager.AppSettings["consumerSecret"].ToString(); // "rms13aB3voQYlFBFTcwwGviGK3cs7hheMN2nnlb5vryLeA4i9t";

            service = new TwitterService(consumerKey, consumerSecret);
            
           //var sample= service.GetAccessTokenWithXAuth(string username, )
           //     (new OAuthRequestToken());

        }

        public List<TweetDetails> GetTweets(string TwitterName, string TweetCount, string HashTag)
        {

            TwitterServiceAuthenticate();

            List<TweetDetails> tweetlist ;

            if (string.IsNullOrEmpty(TweetCount))
            {
                TweetCount = "10";
            }

            int tweetcount = Int16.Parse(TweetCount);
            IEnumerable<TwitterStatus> tweets = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions { ScreenName = TwitterName, Count = tweetcount, });
            if (tweets == null || tweets.Count() < 1)
            {
                tweetlist = null;
                return tweetlist;


            }

            else

            {
                tweetlist = new List<TweetDetails>();

                if (HashTag == null)
                {
                    foreach (var tweet in tweets)
                    {


                        TweetDetails details = new TweetDetails();
                        details.UserName = tweet.User.Name.ToString();
                        details.Tweet = tweet.Text.ToString();
                        details.ScreenName = tweet.User.ScreenName.ToString();
                        details.ProfileImageUrl = tweet.User.ProfileImageUrl.ToString();
                        details.Location = tweet.User.Location.ToString();
                        details.Description = tweet.User.Description.ToString();
                        details.CreateDate = tweet.CreatedDate.ToUniversalTime();
                        details.Id = Convert.ToInt32(tweet.Id);

                        tweetlist.Add(details);

                    }

                }
                else
                {

                    foreach (var tweet in tweets)
                    {
                        tweet.Text = tweet.Text.ToLower();
                        HashTag = HashTag.ToLower();
                        if (tweet.Text.Contains(HashTag))
                        {

                            TweetDetails details = new TweetDetails();
                            details.UserName = tweet.User.Name.ToString();
                            details.Tweet = tweet.Text.ToString();
                            details.ScreenName = tweet.User.ScreenName.ToString();
                            details.ProfileImageUrl = tweet.User.ProfileImageUrl.ToString();
                            details.Location = tweet.User.Location.ToString();
                            details.Description = tweet.User.Description.ToString();
                            details.CreateDate = tweet.CreatedDate.ToUniversalTime();
                            details.Id = tweet.Id;

                            tweetlist.Add(details);

                        };
                    }

                }

                return tweetlist;
            }



        }

        public string PostTweet(string txtTweet)
        {
            string tweetstatus = "";

            TwitterServiceAuthenticate();
            
            var twitterStatus = service.SendTweet(new SendTweetOptions { Status = txtTweet, });
            var responseText = service.Response.Response;
            var response = service.Response.StatusCode;
            if (HttpStatusCode.OK == response)
            {
                tweetstatus = "Tweeted Successfully ";
            }
            else
            {
                tweetstatus = "Tweet Not Posted, Please re-try ";
            }


            return tweetstatus;

        }
        public string ReTweet(string name)
        {

            TwitterServiceAuthenticate();
            var tweets = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions { ScreenName=name,  });
            
           // long retweetid=tweets
            //service.BeginRetweet(new RetweetOptions { Id=tweets.Id, });
            return "abc";
        }

        private void TwitterServiceAuthenticate()
        {

            string token1 = ConfigurationManager.AppSettings["token"].ToString();
            string tokenSecret1 = ConfigurationManager.AppSettings["tokenSecret"].ToString();

            service.AuthenticateWith(token1, tokenSecret1);
        }

    }
}