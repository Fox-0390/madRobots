using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instaRobot.Strings
{
    public static class UrlStrings
    {
        public static string AccessToken = "1381075633.ca61e20.44a4dc7bd0ae43069548b5a73163a92a";

        public static string SearchUserURL = "https://api.instagram.com/v1/users/search?q={0}&access_token={1}";

        public static string SearchMediaUser = "https://api.instagram.com/v1/users/{0}/media/recent/?access_token={1}";

    }
}
