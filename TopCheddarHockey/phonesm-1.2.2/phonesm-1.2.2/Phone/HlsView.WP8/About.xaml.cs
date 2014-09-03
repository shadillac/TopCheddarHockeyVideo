using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace HlsView
{
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();
        }

        private void Pivot_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void hlbReview_Click(object sender, RoutedEventArgs e)
        {
            var task = new MarketplaceReviewTask();
            task.Show();
        }

        private void hlbFeedBack_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask ect = new EmailComposeTask
            {
                To = "shadnickapps@outlook.com",
                Subject = "Top Cheddar Hockey Feedback : Windows Phone",
            };
            ect.Show();
        }
    }
}