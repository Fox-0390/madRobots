using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using instaRobot.Model;
namespace instaRobot.DataAccessLayer
{
   public  interface IDataService
    {
       /// <summary>
       /// gets Suggest by string
       /// </summary>
       /// <param name="s"></param>
       /// <returns></returns>
       Task<UserFindModel> getUsersByChar(string s);
       /// <summary>
       /// get user media recent
       /// </summary>
       /// <param name="id">user id</param>
       /// <returns></returns>
       Task<UserAttributes> getUserAttributes(string id);


   
    }
}
