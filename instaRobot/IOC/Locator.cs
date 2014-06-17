using instaRobot.DataAccessLayer;
using instaRobot.Model;
using instaRobot.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instaRobot.IOC
{
    public class Locator
    {
        ServiceContainer container;
        public Locator()
        {

            container = new ServiceContainer();

            container.Register<IDataService, DataService>();
            container.Register<IHeadModel, HeadModel>(new PerContainerLifetime());
            container.Register<MainViewModel>();
            container.Register<DetailViewModel>();


        }

        public MainViewModel MainPage
        {

            get
            {
                return container.GetInstance<MainViewModel>();
            }
        }


        public DetailViewModel DetailPage
        {
            get
            {
                return container.GetInstance<DetailViewModel>();
            }
        }
    }
}
