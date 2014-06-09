using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instaRobot.Model
{
 public    interface IHeadModel
    {
        void SetImageFoCollage(IList<Datum> _dt);
        IList<Datum> GetImagesForCollage();
    }
}
