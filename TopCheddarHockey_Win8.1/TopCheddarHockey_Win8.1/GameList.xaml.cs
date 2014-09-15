using TopCheddarHockey_Win8._1.Common;
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
using Windows.UI.Popups;
using System.Net.Http;
using System.Xml.Linq;
using Windows.UI.Xaml.Media.Imaging;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace TopCheddarHockey_Win8._1
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class GameList : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public GameList()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
            string videoPage = "http://feeds.cdnak.neulion.com/fs/nhl/mobile/feeds/data/" + e.Parameter.ToString() + ".xml";
            GetXML(videoPage);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void GetXML(string url)
        {
            HttpClient hc = new HttpClient();
            string scores;
            try
            {
                string response = await hc.GetStringAsync(url);

                XDocument xdoc = XDocument.Parse(response, LoadOptions.None);
                Image[] imgAway = new Image[xdoc.Descendants("game").Count()];
                Button[] hlinkAway = new Button[xdoc.Descendants("game").Count()];
                TextBlock[] blkAway = new TextBlock[xdoc.Descendants("game").Count()];
                TextBlock[] awayGoals = new TextBlock[xdoc.Descendants("game").Count()];
                TextBlock[] at = new TextBlock[xdoc.Descendants("game").Count()];
                Image[] imgHome = new Image[xdoc.Descendants("game").Count()];
                Button[] hlinkHome = new Button[xdoc.Descendants("game").Count()];
                TextBlock[] blkHome = new TextBlock[xdoc.Descendants("game").Count()];
                TextBlock[] homeGoals = new TextBlock[xdoc.Descendants("game").Count()];
                int i = 0;
                int heightMargin = 125;
                int horizMargin = 500;
                string gametype;
                string element;

                try
                {
                    gametype = localSettings.Values["GameType"].ToString();
                }
                catch (NullReferenceException)
                {
                    gametype = "Highlights";
                }
                catch (KeyNotFoundException)
                {
                    gametype = "Highlights";
                }

                switch (gametype)
                {
                    case "Hightlights":
                        element = "vod-continuous";
                        break;
                    case "FullGame":
                        element = "vod-whole";
                        break;
                    case "Condensed":
                        element = "vod-condensed";
                        break;
                    default:
                        element = "vod-continuous";
                        break;
                }
                if (xdoc.Descendants("game").Count() > 0)
                {

                    foreach (XElement xe in xdoc.Descendants("game"))
                    {
                        //Set Away Team Image
                        try
                        {
                            BitmapImage awayBmp = new BitmapImage();
                            awayBmp.UriSource = new Uri(@"ms-appx:///Assets/Logos/" + xe.Element("away-team").Element("team-abbreviation").Value + ".png", UriKind.Absolute);
                            imgAway[i] = new Image { Source = awayBmp };
                            imgAway[i].VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                            imgAway[i].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                            imgAway[i].Margin = new Thickness(horizMargin + 5, heightMargin, 0, 0);
                            imgAway[i].Height = 30;
                            imgAway[i].Width = 60;
                            try
                            {
                                imgAway[i].Tag = xe.Element("streams").Element("ipad").Element("away").Element(element).Value;
                                imgAway[i].Tapped += GameList_Click;
                            }
                            catch (Exception)
                            { }
                        }
                        catch (Exception)
                        {
                            BitmapImage awayBmp = new BitmapImage();
                            imgAway[i] = new Image();
                            imgAway[i].VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                            imgAway[i].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                            imgAway[i].Margin = new Thickness(horizMargin + 5, heightMargin, 0, 0);
                            imgAway[i].Height = 30;
                            imgAway[i].Width = 60;
                        }
                        ContentPanel.Children.Add(imgAway[i]);

                        //Set Away Team Properties
                        try
                        {
                            hlinkAway[i] = new Button { Content = xe.Element("away-team").Element("team-abbreviation").Value };
                            hlinkAway[i].Tag = xe.Element("streams").Element("ipad").Element("away").Element(element).Value;
                            hlinkAway[i].VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                            hlinkAway[i].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                            hlinkAway[i].FontSize = 22.667;
                            hlinkAway[i].Margin = new Thickness(horizMargin + 75, heightMargin - 7, 0, 0);
                            SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.White);
                            hlinkAway[i].BorderBrush = brush;
                            hlinkAway[i].BorderThickness = new Thickness(1, 1, 1, 1);
                            hlinkAway[i].Width = 85;
                            hlinkAway[i].Click += GameList_Click;
                            ContentPanel.Children.Add(hlinkAway[i]);
                        }
                        catch (Exception)
                        {
                            blkAway[i] = new TextBlock { Text = xe.Element("away-team").Element("team-abbreviation").Value };
                            blkAway[i].FontSize = 22.667;
                            blkAway[i].VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                            blkAway[i].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                            blkAway[i].Margin = new Thickness(horizMargin + 100, heightMargin + 5, 0, 0);
                            blkAway[i].FontSize = 22.667;
                            ContentPanel.Children.Add(blkAway[i]);
                        }


                        try
                        {
                            scores = localSettings.Values["HideScores"].ToString();
                        }
                        catch (NullReferenceException)
                        {
                            scores = "0";
                        }

                        if (scores != "1")
                        {
                            try
                            {
                                //Set Away Goals
                                awayGoals[i] = new TextBlock { Text = xe.Element("away-team").Element("goals").Value };
                                awayGoals[i].FontSize = 22.667;
                                awayGoals[i].VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                                awayGoals[i].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                                awayGoals[i].Margin = new Thickness(horizMargin + 170, heightMargin + 5, 0, 0);
                                ContentPanel.Children.Add(awayGoals[i]);

                                //Set Home Goals
                                homeGoals[i] = new TextBlock { Text = xe.Element("home-team").Element("goals").Value };
                                homeGoals[i].FontSize = 22.667;
                                homeGoals[i].VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                                homeGoals[i].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                                homeGoals[i].Margin = new Thickness(horizMargin + 195, heightMargin + 5, 0, 0);
                                ContentPanel.Children.Add(homeGoals[i]);
                            }
                            catch (Exception)
                            {

                            }

                        }

                        //Set Home Team Properties
                        try
                        {
                            hlinkHome[i] = new Button { Content = xe.Element("home-team").Element("team-abbreviation").Value };
                            hlinkHome[i].Tag = xe.Element("streams").Element("ipad").Element("home").Element(element).Value;
                            hlinkHome[i].FontSize = 22.667;
                            hlinkHome[i].VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                            hlinkHome[i].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                            hlinkHome[i].Margin = new Thickness(horizMargin + 218, heightMargin - 7, 0, 0);
                            SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.White);
                            hlinkHome[i].BorderBrush = brush;
                            hlinkHome[i].BorderThickness = new Thickness(1, 1, 1, 1);
                            hlinkHome[i].Width = 85;
                            hlinkHome[i].Click += GameList_Click;
                            ContentPanel.Children.Add(hlinkHome[i]);
                        }
                        catch (Exception)
                        {
                            blkHome[i] = new TextBlock { Text = xe.Element("home-team").Element("team-abbreviation").Value };
                            blkHome[i].FontSize = 22.667;
                            blkHome[i].VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                            blkHome[i].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                            blkHome[i].Margin = new Thickness(horizMargin + 220, heightMargin + 5, 0, 0);
                            blkHome[i].FontSize = 22.667;
                            ContentPanel.Children.Add(blkHome[i]);
                        }

                        //Home Team Picture
                        try
                        {
                            BitmapImage homeBmp = new BitmapImage();
                            homeBmp.UriSource = new Uri(@"ms-appx:///Assets/Logos/" + xe.Element("home-team").Element("team-abbreviation").Value + ".png", UriKind.Absolute);
                            imgHome[i] = new Image { Source = homeBmp };
                            imgHome[i].VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                            imgHome[i].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                            imgHome[i].Margin = new Thickness(horizMargin + 310, heightMargin, 0, 0);
                            imgHome[i].Height = 30;
                            imgHome[i].Width = 60;
                        }
                        catch (Exception)
                        {
                            BitmapImage homeBmp = new BitmapImage();

                            imgHome[i] = new Image();
                            imgHome[i].VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                            imgHome[i].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                            imgHome[i].Margin = new Thickness(horizMargin + 310, heightMargin, 0, 0);
                            imgHome[i].Height = 30;
                            imgHome[i].Width = 60;
                            try
                            {
                                imgHome[i].Tag = xe.Element("streams").Element("ipad").Element("home").Element(element).Value;
                                imgHome[i].Tapped += GameList_Click;
                            }
                            catch (Exception)
                            { }
                        }
                        ContentPanel.Children.Add(imgHome[i]);

                        heightMargin = heightMargin + 42;
                        i++;
                    }
                }
                else
                {
                    ShowMessageBox("No NHL Games found on this day.");
                    Frame.GoBack();
                }
            }
            catch (Exception)
            {
                ShowMessageBox("No NHL Games found on this day.");
                Frame.GoBack();
            }

        }
        void GameList_Click(object sender, RoutedEventArgs e)
        {
            if (sender.ToString() == "Windows.UI.Xaml.Controls.Image")
            {
                Image target = sender as Image;
                if (target.Tag.ToString().EndsWith("m3u8"))
                {
                    Frame.Navigate(typeof(FullGameViewer), target.Tag.ToString());
                }
                else
                {
                    Frame.Navigate(typeof(HighLightViewer), target.Tag.ToString());
                }
            }
            else
            {
                Button target = sender as Button;
                if (target.Tag.ToString().EndsWith("m3u8"))
                {
                    Frame.Navigate(typeof(FullGameViewer), target.Tag.ToString());
                }
                else
                {
                    Frame.Navigate(typeof(HighLightViewer), target.Tag.ToString());
                }
            }
        }

        private async void ShowMessageBox(string text)
        {
            MessageDialog msgDia = new MessageDialog(text);
            await msgDia.ShowAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings));
        }
    }
}
