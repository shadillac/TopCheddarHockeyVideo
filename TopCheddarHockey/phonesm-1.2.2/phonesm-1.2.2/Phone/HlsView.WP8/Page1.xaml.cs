using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HlsView
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
            gameDate.Value = DateTime.Now.AddDays(-1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            NavigationService.RemoveBackEntry();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            int year = gameDate.Value.Value.Year;
            int month = gameDate.Value.Value.Month;
            int day = gameDate.Value.Value.Day;
            string dateString = year.ToString() + month.ToString("00") + day.ToString("00");
            NavigationService.Navigate(new Uri("/GameList.xaml?date=" + dateString, UriKind.Relative));
        }

        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarMenuItem appbarmenuitem = new ApplicationBarMenuItem("Settings...");
            ApplicationBarMenuItem appbarmenuitem2 = new ApplicationBarMenuItem("About");
            ApplicationBar.MenuItems.Add(appbarmenuitem);
            ApplicationBar.MenuItems.Add(appbarmenuitem2);
            appbarmenuitem.Click += appbarmenuitem_Click;
            appbarmenuitem2.Click += appbarmenuitem2_Click;
        }

        void appbarmenuitem2_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }

        void appbarmenuitem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }
    }
}