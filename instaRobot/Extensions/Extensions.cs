using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace instaRobot.Extensions
{

   




    public static class Extensions
    {


            public static void AddRange<T>(this ObservableCollection<T> oc, IEnumerable<T> collection)
            {
                if (collection == null)
                {
                    throw new ArgumentNullException("collection");
                }
                foreach (var item in collection)
                {
                    oc.Add(item);
                }


            }
    
        public static Task<Stream> OpeningTask(this WebClient webClient, Uri uri)
        {
            var tcs = new TaskCompletionSource<Stream>();

            webClient.OpenReadCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    tcs.SetException(e.Error);
                }
                else
                {
                    bool b= tcs.TrySetResult(e.Result);
                    
                    
                }
            };

            webClient.OpenReadAsync(uri);

            return tcs.Task;
        }



        public static byte[] ImageToBytes(BitmapImage img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                WriteableBitmap btmMap = new WriteableBitmap(img);
                System.Windows.Media.Imaging.Extensions.SaveJpeg(btmMap, ms, img.PixelWidth, img.PixelHeight, 0, 100);
                img = null;
                return ms.ToArray();
            }
        }
    }
}
