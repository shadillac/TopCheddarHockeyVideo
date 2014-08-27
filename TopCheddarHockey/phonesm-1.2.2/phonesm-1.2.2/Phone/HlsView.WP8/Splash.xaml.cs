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
    public partial class Splash : PhoneApplicationPage
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }
    }
}