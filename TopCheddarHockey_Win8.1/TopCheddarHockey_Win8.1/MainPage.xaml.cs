using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TopCheddarHockey_Win8._1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            localSettings.Values["Date"] = gameDate.Date.Date.ToString();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DateTimeOffset newDate = DateTimeOffset.Now;
            try
            {
                DateTimeOffset.TryParse(localSettings.Values["Date"].ToString(), out newDate);
                gameDate.Date = newDate;
            }
            catch
            { }
            
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            int year = gameDate.Date.Year;
            int month = gameDate.Date.Month;
            int day = gameDate.Date.Day;
            string dateString = year.ToString() + month.ToString("00") + day.ToString("00");
            Frame.Navigate(typeof(GameList), dateString);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings));
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }
    }
}
