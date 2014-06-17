using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using instaRobot.Model;
using RestSharp;
using instaRobot.Extensions;
using instaRobot.Strings;
namespace instaRobot.DataAccessLayer
{
    public class DataService : IDataService
    {
        public async Task<UserFindModel> getUsersByChar(string s)
        {

            TaskCompletionSource<UserFindModel> task = new TaskCompletionSource<UserFindModel>();

            
            var t = new RestClient();

            var req = await t.ExecuteAwait(new RestRequest(String.Format(UrlStrings.SearchUserURL, s,UrlStrings.AccessToken)));

            if (req != null)
            {
                task.SetResult(SimpleJson.DeserializeObject<UserFindModel>(req.Content));
            }
            return await task.Task;


        }


        public async Task<UserAttributes> getUserAttributes(string id)
        {
           TaskCompletionSource<UserAttributes> task = new TaskCompletionSource<UserAttributes>();
            var t = new RestClient();

            var req = await t.ExecuteAwait(new RestRequest(String.Format(UrlStrings.SearchMediaUser, id,UrlStrings.AccessToken)));

            if (req != null)
            {
                task.SetResult(SimpleJson.DeserializeObject<UserAttributes>(req.Content));
            }
            return await task.Task;
        }


      
    }
}
