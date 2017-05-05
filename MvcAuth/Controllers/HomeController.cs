using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAuth.Controllers
{
    //[RequireHttps]
    public class HomeController : Controller
    {
        ITwitterClient _client;


        public HomeController(ITwitterClient twitterClient)
        {
            _client = twitterClient;
        }

        [HttpGet]
        [AllowAnonymous]
       
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Home/
        [HttpGet]
      
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult Search(string txtTwitterName, string txtTweetCount, string txtHashtag)

        {

            ViewBag.Tweets = _client.GetTweets(txtTwitterName, txtTweetCount, txtHashtag);
            if (ViewBag.Tweets == null)
            {
                ViewBag.Result = "Use Name Doesn't exist";
            }
            else
            {
                ViewBag.Result = "";
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";


            return View();
        }

        [HttpGet]
        public ActionResult Post()
        {
            

            return View();
        }

        [HttpPost]
        public ActionResult Post(string txtTweet)
        {

            //TwitterClient tc = new TwitterClient();
            ViewBag.Status = _client.PostTweet(txtTweet);
            return View();
        }
    }
}