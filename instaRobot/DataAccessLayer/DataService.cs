using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using instaRobot.Model;
using RestSharp;
using instaRobot.Extensions;
namespace instaRobot.DataAccessLayer
{
    public class DataService : IDataService
    {
        public async Task<UserFindModel> getUsersByChar(string s)
        {

            TaskCompletionSource<UserFindModel> task = new TaskCompletionSource<UserFindModel>();

            string s1 = "https://api.instagram.com/v1/users/search?q=lina_inlakesh&access_token=1381075633.f59def8.4266cf4cabbd4618a8e0a60bcab5fc5e";

            var t = new RestClient();

            var req = await t.ExecuteAwait(new RestRequest(String.Format("https://api.instagram.com/v1/users/search?q={0}&access_token=1381075633.ca61e20.44a4dc7bd0ae43069548b5a73163a92a", s)));

            if (req != null)
            {
                task.SetResult(SimpleJson.DeserializeObject<UserFindModel>(req.Content));
            }
            return await task.Task;


        }


        public async Task<UserAttributes> getUserAttributes(string id)
        {
            string s = "https://api.instagram.com/v1/users/328837544/media/recent/?access_token=1381075633.ca61e20.44a4dc7bd0ae43069548b5a73163a92a";
            TaskCompletionSource<UserAttributes> task = new TaskCompletionSource<UserAttributes>();
            var t = new RestClient();

            var req = await t.ExecuteAwait(new RestRequest(String.Format("https://api.instagram.com/v1/users/{0}/media/recent/?access_token=1381075633.ca61e20.44a4dc7bd0ae43069548b5a73163a92a", id)));

            if (req != null)
            {
                task.SetResult(SimpleJson.DeserializeObject<UserAttributes>(req.Content));
            }
            return await task.Task;
        }
    }
}
