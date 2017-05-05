using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAuth
{
    public class TweetDetails
    {
        public string ProfileImageUrl { get; set; }
        public string ScreenName { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Tweet { get; set; }
        public DateTime CreateDate { get; set; }
        public long Id { get; set; }
    }
}