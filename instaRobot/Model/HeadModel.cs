using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instaRobot.Model
{





   public class HeadModel:IHeadModel
    {


       private IList<Datum> _imagesForCollage;


       public void SetImageFoCollage(IList<Datum> _dt)
       {

           _imagesForCollage = _dt;

       }



       public IList<Datum> GetImagesForCollage()
       {
           return _imagesForCollage;
       }

    }
}
