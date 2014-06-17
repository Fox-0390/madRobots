using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using instaRobot.Resources;
using instaRobot.DataAccessLayer;
using System.Windows.Input;
using instaRobot.Extensions;
using instaRobot.Model;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
namespace instaRobot.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        /// <summary>
        /// Service networking
        /// </summary>
        private IDataService _dataService;

       
        /// <summary>
        /// selected image collection
        /// </summary>
        IList<Datum> Collection = new List<Datum>();
    
        public  IHeadModel _headModel{get;set;}
        public MainViewModel(IDataService dataService)
        {
          
           
            tapPicCommand = new DelegateCommand(ExecuteTapPhotos);
            getPhotos = new DelegateCommand(ExecuteGetPhotos);
            _dataService = dataService;
            createCollage = new DelegateCommand(ExecuteCreateCollage);
            findUsers = new DelegateCommand(FindUser);
            IsEnforce = false;
            IsVisiblePreviousButton = false;
        }


        #region Suggest Command
        private ICommand findUsers;

        public ICommand FindUSers
        {
            get { return findUsers; }
            set { findUsers = value; }
        }


        private async void FindUser(object obj)
        {
            if (!String.IsNullOrWhiteSpace(Name))
            {
                var res = await _dataService.getUsersByChar(Name);
                if (res != null && res.data != null)
                {
                    NameSuggests.Clear();

                    NameSuggests.AddRange(res.data);


                }
                else

                    SetHeaderAttributes(false, "нет такого человека");
            }


        } 
        #endregion





        #region props autocompleteBox
        private string name;
        /// <summary>
        /// User nickName
        /// </summary>
        public  string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                    FindUSers.Execute(null);
                }
            }
        }



        private ObservableCollection<UserDatum> nameSuggest = new ObservableCollection<UserDatum>();

        public ObservableCollection<UserDatum> NameSuggests
        {
            get { return nameSuggest; }
            set { nameSuggest = value; }
        }

        #endregion






        
        private ICommand getPhotos;

        public ICommand GetPhotos
        {
            get { return getPhotos; }
            set { getPhotos = value; }
        }

        private async void ExecuteGetPhotos(object obj)
        {
            IsEnforce = false;

            UserDatum selectedDataum = NameSuggests.FirstOrDefault<UserDatum>(x => x.username == Name);
            UserAttributes t;
            SetHeaderAttributes(true, String.Format("Собираем фотографии пользователя {0}", Name));
            if (selectedDataum != null)
            {

                t = await _dataService.getUserAttributes(selectedDataum.id);
                if (t != null && t.data != null)
                {
                    UserPics.Clear();
                    foreach (var item in t.data)
                    {
                        UserPics.Add(item);
                    }
                    IsEnforce = true;
                    SetHeaderAttributes(false, String.Empty);
                    IsVisiblePreviousButton = true;
                }
            }
            else
            {
               
                IsVisiblePreviousButton = false;
                SetHeaderAttributes(false, String.Format("Не удалось загрузить пользователя {0}", Name));
            }
           
        }



        #region props and commands of multySelector
        private ObservableCollection<Datum> userPics = new ObservableCollection<Datum>();
        /// <summary>
        /// Items collection for binding
        /// </summary>
        public ObservableCollection<Datum> UserPics
        {
            get { return userPics; }
            set { userPics = value; }
        }


        private bool isEnforce;
        /// <summary>
        /// Props LongListSelector for clearing selected collection
        /// </summary>
        public bool IsEnforce
        {
            get { return isEnforce; }
            set { isEnforce = value; NotifyPropertyChanged("IsEnforce"); }
        }



      



       


        private ICommand tapPicCommand;

        public ICommand TapPicCommand
        {
            get { return tapPicCommand; }
            set { tapPicCommand = value; }
        }
        private void ExecuteTapPhotos(object obj)
        {
            Collection.Clear();
            for (int i = 0; i < (obj as IList<object>).Count; i++)
            {
                Collection.Add((obj as IList<object>)[i] as Datum);
            }

        }
        
        #endregion



        #region  Appbar button commands 


        private bool isVisiblePreviousButton;

        public bool IsVisiblePreviousButton
        {
            get { return isVisiblePreviousButton; }
            set { isVisiblePreviousButton = value; NotifyPropertyChanged("IsVisiblePreviousButton"); }
        }
        



        private ICommand createCollage;

        public ICommand CreateCollage
        {
            get { return createCollage; }
            set { createCollage = value; }
        }

        private void ExecuteCreateCollage(object obj)
        {


            _headModel.SetImageFoCollage(Collection);

            ApplicationController.Default.NavigateTo(ViewType.Detail);


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
        private void SetHeaderAttributes(bool f,string text)
        {
            Progress = f;
            ProgressText = text;
        } 
        #endregion

    }
}