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


        private IDataService _dataService;

        private UserDatum selectedDataum;

    
        public  IHeadModel _headModel{get;set;}
        public MainViewModel(IDataService dataService)
        {
          
           
            selectionPicCommand = new DelegateCommand(ExecuteSelectionPhotos);
            getPhotos = new DelegateCommand(ExecuteGetPhotos);
            _dataService = dataService;
            createCollage = new DelegateCommand(ExecuteCreateCollage);
        }

        private async void  FindUser()
        {
            var res = await _dataService.getUsersByChar(Name);
            if (res != null && res.data != null)
            {
               
                foreach (var item in res.data)
                {
                    NameSuggests.Add(item);
                }
             
            }
        }





        #region props autocompleteBox
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if ((value != name) && !String.IsNullOrWhiteSpace(value))
                {
                    name = value;

                    NotifyPropertyChanged("Name");
                    FindUser();
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
            selectedDataum = NameSuggests.FirstOrDefault<UserDatum>(x => x.username == Name);
            
            var t= await _dataService.getUserAttributes(selectedDataum.id);
            if (t!=null&&t.data!=null)
            {
                UserPics.Clear();
                foreach (var item in t.data)
                {
                    UserPics.Add(item);
                }

            }
        }



        #region props and commands of multySelector
        private ObservableCollection<Datum> userPics = new ObservableCollection<Datum>();

        public ObservableCollection<Datum> UserPics
        {
            get { return userPics; }
            set { userPics = value; }
        }





        IList<Datum> Collection = new List<Datum>();


        private ICommand selectionPicCommand;

        public ICommand SelectionPicCommand
        {
            get { return selectionPicCommand; }
            set { selectionPicCommand = value; }
        }
        private void ExecuteSelectionPhotos(object obj)
        {
            Collection.Clear();
            for (int i = 0; i < (obj as IList<object>).Count; i++)
            {
                Collection.Add((obj as IList<object>)[i] as Datum);
            }

        }
        
        #endregion



        #region go to Send Page

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

        



    }
}