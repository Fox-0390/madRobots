using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instaRobot.Model
{
   

    public class UserDatum
    {
        public string username { get; set; }
        public string bio { get; set; }
        public string website { get; set; }
        public string profile_picture { get; set; }
        public string full_name { get; set; }
        public string id { get; set; }
    }

    public class UserFindModel
    {
        public Meta meta { get; set; }
        public List<UserDatum> data { get; set; }
    }
}
