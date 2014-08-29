using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace HlsView
{
    public partial class HighlightViewer : PhoneApplicationPage
    {
        static readonly TimeSpan FFStepSize = TimeSpan.FromSeconds(30);
        static readonly TimeSpan RewStepSize = TimeSpan.FromSeconds(5);

        public HighlightViewer()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //this.navigationHelper.OnNavigatedTo(e);
            string source = "";
            NavigationContext.QueryString.TryGetValue("source", out source);
            mdaHighView.Source = new Uri(source,UriKind.Absolute);
            btnPlay.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mdaHighView.Play();
            btnPlay.IsEnabled = false;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mdaHighView.Stop();
            btnPlay.IsEnabled = true;
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if (mdaHighView.CanPause)
            {
                mdaHighView.Pause();
                btnPlay.IsEnabled = true;
            }
        }

        private void btnRew_Click(object sender, RoutedEventArgs e)
        {
            if (null == mdaHighView || mdaHighView.CurrentState != MediaElementState.Playing)
                return;
            var position = mdaHighView.Position;

            if (position < RewStepSize)
                position = TimeSpan.Zero;
            else
                position -= RewStepSize;

            mdaHighView.Position = position - RewStepSize;

        }

        private void btnFF_Click(object sender, RoutedEventArgs e)
        {
            if (null == mdaHighView || mdaHighView.CurrentState != MediaElementState.Playing)
                return;
            var position = mdaHighView.Position;
            mdaHighView.Position = position + FFStepSize;
        }
    }
}