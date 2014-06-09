using instaRobot.Extensions;
using instaRobot.Model;
using Microsoft.Phone;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Media.PhoneExtensions;
namespace instaRobot.ViewModels
{
    public class DetailViewModel : ViewModelBase
    {

        WriteableBitmap merge = new WriteableBitmap(600, 450); 
        private IHeadModel headModel;

        public DetailViewModel(IHeadModel _headmodel)
        {
            headModel = _headmodel;
            sendCommand = new DelegateCommand(ExecuteSendCommand);
            loaded = new DelegateCommand(ExecuteLoaded);




        }

        private async void ExecuteLoaded(object obj)
        {
            var t = await createCollate();
            if (t == true)
            {


                for (int i = 0; i < _listBmp.Count; i++)
                {

                    if (i % 2 == 1)
                        merge.Blit(new Rect(150, 150 * (i / 2), 150, 150), _listBmp[i], new Rect(0, 0, _listBmp[i].PixelWidth, _listBmp[i].PixelHeight)); //draw the photo first
                    else if (i % 2 == 0)
                        merge.Blit(new Rect(0, 150 * (i / 2), 150, 150), _listBmp[i], new Rect(0, 0, _listBmp[i].PixelWidth, _listBmp[i].PixelHeight)); //draw the photo first


                }

                using (MemoryStream ms = new MemoryStream())
                {
                    merge.SaveJpeg(ms, 600, 450, 0, 100);
                    BitmapImage bmp = new BitmapImage();
                    bmp.SetSource(ms);
                    Collage = bmp;
                }

            }
        }


        private ICommand loaded;

        public ICommand Loaded
        {
            get { return loaded; }
            set { loaded = value; }
        }




        List<WriteableBitmap> _listBmp = new List<WriteableBitmap>();
        private async Task<bool> createCollate()
        {


            TaskCompletionSource<bool> tsc = new TaskCompletionSource<bool>();

            for (int i = 0; i < headModel.GetImagesForCollage().Count; i++)
            {

                WebClient client = new WebClient();


                var stream = await client.OpeningTask(new Uri(headModel.GetImagesForCollage()[i].images.standard_resolution.url));
                if (stream != null)
                    using (stream)
                    {
                        _listBmp.Add((WriteableBitmap)PictureDecoder.DecodeJpeg(stream));

                        if (i == headModel.GetImagesForCollage().Count - 1)
                        {
                            tsc.SetResult(true);
                        }
                    }


            }
            return await tsc.Task;





        }


        private BitmapImage collage;

        public BitmapImage Collage
        {
            get { return collage; }
            set { collage = value; NotifyPropertyChanged("Collage"); }
        }



        


        private ICommand sendCommand;

        public ICommand SendCommand
        {
            get { return sendCommand; }
            set { sendCommand = value; }
        }

        private void ExecuteSendCommand(object obj)
        {

            byte[] b=  Extensions.Extensions.ImageToBytes(collage);
            MediaLibrary lib = new MediaLibrary();
            Picture picture = lib.SavePicture("test.jpg", b);
            ShareMediaTask task = new ShareMediaTask();
            task.FilePath = picture.GetPath();
            task.Show();



        }


       
    }
}
