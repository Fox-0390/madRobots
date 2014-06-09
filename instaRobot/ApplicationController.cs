using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace instaRobot
{
    public enum ViewType { Main, Detail }

    public class ApplicationController
    {
        static ApplicationController m_instance;
        Dictionary<ViewType, Uri> m_Views;

        ApplicationController()
        {
            m_Views = new Dictionary<ViewType, Uri>();

            //register views with controller

            Register(ViewType.Main, new Uri("/MainPage.xaml", UriKind.Relative));
            Register(ViewType.Detail, new Uri("/DetailsPage.xaml", UriKind.Relative));



        }

        public static ApplicationController Default
        {
            get
            {
                if (m_instance == null)
                    m_instance = new ApplicationController();

                return m_instance;
            }
        }

        void Register(ViewType type, Uri address)
        {
            if (m_Views.ContainsKey(type)) //update
            {
                m_Views[type] = address;
                return;
            }

            m_Views.Add(type, address); //add
        }

        void UnRegister(ViewType type)
        {
            if (m_Views.ContainsKey(type))
                m_Views.Remove(type);
        }

        public void GoBack()
        {

            App.RootFrame.GoBack();
            //  root.RemoveBackEntry();
        }


        public void NavigateTo(ViewType type, string paramName = null, object param = null)
        {


            if (!m_Views.ContainsKey(type))
                return;

            Uri address = m_Views[type];

            PhoneApplicationFrame root = Application.Current.RootVisual as PhoneApplicationFrame;
            System.Diagnostics.Debug.Assert(root != null, "Root is null");
            if (param == null)
            {
                root.Navigate(address);
            }
            else
            {

                root.Navigate(new Uri(address.OriginalString + "?" + paramName + "=" + param, UriKind.Relative));
            }

        }
    }
}
