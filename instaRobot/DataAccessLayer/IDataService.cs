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
       Task<UserFindModel> getUsersByChar(string s);
       Task<UserAttributes> getUserAttributes(string id);
    }
}
