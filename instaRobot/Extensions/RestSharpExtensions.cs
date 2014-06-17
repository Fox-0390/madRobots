using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instaRobot.Extensions
{
    public static class RestSharpExtensions
    {
        public static Task<RestSharp.IRestResponse> ExecuteAwait(this RestClient client, RestRequest request)
        {

            TaskCompletionSource<IRestResponse> taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            client.ExecuteAsync(request, (response, asyncHandle) =>
            {
                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    taskCompletionSource.SetException(response.ErrorException);
                }
                else
                {
                    taskCompletionSource.SetResult(response);
                }
            });
            return taskCompletionSource.Task;
        }
    }
}
