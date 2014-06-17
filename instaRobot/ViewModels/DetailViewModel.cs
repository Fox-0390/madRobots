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

        WriteableBitmap merge;
        private IHeadModel headModel;
        List<BitmapImage> _listBmp = new List<BitmapImage>();
        public DetailViewModel(IHeadModel _headmodel)
        {
            headModel = _headmodel;
            sendCommand = new DelegateCommand(ExecuteSendCommand);
            loaded = new DelegateCommand(ExecuteLoaded);
        }


        #region Command Loaded
        private ICommand loaded;

        public ICommand Loaded
        {
            get { return loaded; }
            set { loaded = value; }
        }

        private async void ExecuteLoaded(object obj)
        {
            SetHeaderAttributes(true, "Составляем коллаж");
            merge = BitmapFactory.New(300, ((headModel.GetImagesForCollage().Count / 2) + (headModel.GetImagesForCollage().Count % 2)) * 150);
            var t = await createCollate();
            if (t == true)
            {
                for (int i = 0; i < _listBmp.Count; i++)
                {
                    if (i % 2 == 1)
                        merge.Blit(new Rect(150, 150 * (i / 2), 150, 150), new WriteableBitmap(_listBmp[i]), new Rect(0, 0, _listBmp[i].PixelWidth, _listBmp[i].PixelHeight)); //draw the photo first
                    else if (i % 2 == 0)
                        merge.Blit(new Rect(0, 150 * (i / 2), 150, 150), new WriteableBitmap(_listBmp[i]), new Rect(0, 0, _listBmp[i].PixelWidth, _listBmp[i].PixelHeight)); //draw the photo first
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    merge.SaveJpeg(ms, merge.PixelWidth, merge.PixelHeight, 0, 100);
                    BitmapImage bmp = new BitmapImage();
                    bmp.SetSource(ms);
                    Collage = bmp;
                    SetHeaderAttributes(false, "Коллаж готов");
                }
            }
        } 
        #endregion


       





        private async Task<bool> createCollate()
        {
            TaskCompletionSource<bool> tsc = new TaskCompletionSource<bool>();

            for (int i = 0; i < headModel.GetImagesForCollage().Count; i++)
            {
                WebClient client = new WebClient();
                var stream = await client.OpeningTask(new Uri(headModel.GetImagesForCollage()[i].images.standard_resolution.url, UriKind.Absolute));
                if (stream != null)
                    using (stream)
                    {
                        var t = new BitmapImage();
                        t.SetSource(stream);
                        _listBmp.Add(t);

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






        #region Send to Email
        private ICommand sendCommand;

        public ICommand SendCommand
        {
            get { return sendCommand; }
            set { sendCommand = value; }
        }

        private void ExecuteSendCommand(object obj)
        {

            byte[] b = Extensions.Extensions.ImageToBytes(Collage);
            MediaLibrary lib = new MediaLibrary();
            Picture picture = lib.SavePicture("test.jpg", b);
            ShareMediaTask task = new ShareMediaTask();
            task.FilePath = picture.GetPath();
            task.Show();



        } 
        #endregion

        #region Header Page
        private string progressText;

        public string ProgressText
        {
            get { return progressText; }
            set { progressText = value; NotifyPropertyChanged("ProgressText"); }
        }


        private bool progress;

        public bool Progress
        {
            get { return progress; }
            set { progress = value; NotifyPropertyChanged("Progress"); }
        }

        /// <summary>
        /// Set systemTray properties
        /// </summary>
        /// <param name="f">isVisible progress indicator</param>
        /// <param name="text">text for progress indicator</param>
        private void SetHeaderAttributes(bool f, string text)
        {
            Progress = f;
            ProgressText = text;
        }
        #endregion

    }
}
