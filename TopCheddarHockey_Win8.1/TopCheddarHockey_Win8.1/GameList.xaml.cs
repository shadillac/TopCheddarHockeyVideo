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
using Newtonsoft.Json.Linq;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace TopCheddarHockey_Win8._1
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class GameList : Page
    {
        string gameYear = "";
        string gameDate = "";
        int heightMargin = 125;
        int horizMargin = 500;
        JArray fullSchedule = new JArray();
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
            gameDate = e.Parameter.ToString();
            gameYear = gameDate[0].ToString() + gameDate[1].ToString() + gameDate[2].ToString() + gameDate[3].ToString();
            string gameYearPlus = gameDate[0].ToString() + gameDate[1].ToString() + gameDate[2].ToString() + (Convert.ToInt32(gameDate[3].ToString()) + 1).ToString();
            string videoPage = "http://live.nhl.com/GameData/SeasonSchedule-" + gameYear + gameYearPlus + ".json";
            GetXML(videoPage);
            GetScores();
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

                if (fullSchedule.Count == 0)
                {
                    JObject o = new JObject();
                    try
                    {
                        string fullString = "{games: " + response + "}";
                        o = JObject.Parse(fullString);
                        foreach (JToken game in o["games"])
                        {
                            fullSchedule.Add(game);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowMessageBox(ex.Message);
                    }
                }

                foreach (JToken gameID in fullSchedule)
                {
                    if (gameID["est"].ToString().Split(new Char[] { ' ' })[0] == gameDate)
                    {
                        string gameDetailID = gameID["id"].ToString()[4].ToString() + gameID["id"].ToString()[5].ToString() + "_" + gameID["id"].ToString()[6].ToString() + gameID["id"].ToString()[7].ToString() + gameID["id"].ToString()[8].ToString() + gameID["id"].ToString()[9].ToString();
                        string gameDetailPage = "http://smb.cdnak.neulion.com/fs/nhl/mobile/feed_new/data/streams/" + gameYear + "/ipad/" + gameDetailID + ".json";
                        HttpClient wClient = new HttpClient();
                        try
                        {
                            string gameResponse = await wClient.GetStringAsync(gameDetailPage);
                            string gametype = "";
                            string element = "";
                            string homeTagUrl = "";
                            string awayTagUrl = "";

                            //try
                            //{
                            //    gametype = (string)userSettings["GameType"];
                            //}
                            //catch (NullReferenceException)
                            //{
                            //    gametype = "Highlights";
                            //}
                            //catch (KeyNotFoundException)
                            //{
                            //    gametype = "Highlights";
                            //}

                            switch (gametype)
                            {
                                case "Highlights":
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

                            JObject o = new JObject();
                            try
                            {
                                o = JObject.Parse(gameResponse);
                            }
                            catch (Exception)
                            {

                            }
                            try
                            {
                                homeTagUrl = o["gameStreams"]["ipad"]["home"][element]["bitrate0"].ToString();
                            }
                            catch
                            {
                                homeTagUrl = "";
                            }

                            try
                            {
                                awayTagUrl = o["gameStreams"]["ipad"]["away"][element]["bitrate0"].ToString();
                            }
                            catch
                            {
                                awayTagUrl = "";
                            }

                            string homeTeam = "";
                            string awayTeam = "";

                            try
                            {

                                string contURL = o["gameStreams"]["ipad"]["home"]["vod-continuous"]["bitrate0"].ToString();
                                homeTeam = contURL.Split(new Char[] { '_' })[3].ToString();
                                awayTeam = contURL.Split(new Char[] { '_' })[2].ToString();
                            }
                            catch
                            {

                            }

                            CreateTeamButtons(awayTeam, homeTeam, awayTagUrl, homeTagUrl);
                            
                        }
                        catch (Exception ex)
                        {
                            ShowMessageBox(ex.Message);
                        }
                    }
                }
            }
            catch (Exception)
            {
                ShowMessageBox("No NHL Games found on this day.");
                Frame.GoBack();
            }

        }

        private async void GetScores()
        {
            string dateString = gameDate[4].ToString()+gameDate[5].ToString()+"/"+gameDate[6].ToString()+gameDate[7].ToString()+"/" + gameYear;
            HttpClient hc = new HttpClient();
            string response = await hc.GetStringAsync("https://api.hockeystreams.com/Scores?key=15d6fae8d55253b4789439bcccbdc206&date="+dateString);
            try
            {
                JObject o = new JObject();
                o = JObject.Parse(response);
                foreach (JToken score in o["scores"])
                {

                }
            }
            catch (Exception)
            {

            }

        }

        private void CreateTeamButtons(string awayTeam, string homeTeam, string awaytagdata, string hometagData)
        {
            Button hlinkAway = new Button();
            Button hlinkHome = new Button();
            Image imgAway = new Image();
            TextBlock awayGoals = new TextBlock();
            TextBlock at = new TextBlock();
            Image imgHome = new Image();
            TextBlock homeGoals = new TextBlock();


            if ((homeTeam.Length == 3) && (awayTeam.Length == 3))
            {

                //Set Away Team Image
                try
                {
                    BitmapImage awayBmp = new BitmapImage();
                    awayBmp.UriSource = new Uri(@"ms-appx:///Assets/Logos/" + awayTeam + ".png", UriKind.Absolute);
                    imgAway = new Image { Source = awayBmp };
                    imgAway.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                    imgAway.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                    imgAway.Margin = new Thickness(horizMargin + 0, heightMargin - 6, 0, 0);
                    imgAway.Height = 45;
                    imgAway.Width = 60;
                    imgAway.Stretch = Stretch.Fill;
                    ContentPanel.Children.Add(imgAway);
                }
                catch
                {
                    BitmapImage awayBmp = new BitmapImage();
                    imgAway = new Image();
                    imgAway.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                    imgAway.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                    imgAway.Margin = new Thickness(horizMargin + 5, heightMargin, 0, 0);
                    imgAway.Height = 30;
                    imgAway.Width = 60;
                }

                //Set Away Team Properties
                try
                {

                    hlinkAway = new Button { Content = awayTeam.ToUpper() };
                    hlinkAway.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                    hlinkAway.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                    hlinkAway.FontSize = 22.667;
                    hlinkAway.Margin = new Thickness(horizMargin + 75, heightMargin - 7, 0, 0);
                    SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.White);
                    hlinkAway.BorderBrush = brush;
                    hlinkAway.BorderThickness = new Thickness(1, 1, 1, 1);
                    hlinkAway.Width = 85;
                    if ((awaytagdata.EndsWith(".mp4")) || (awaytagdata.EndsWith(".m3u8")))
                    {
                        hlinkAway.Tag = awaytagdata;
                    }
                    else
                    {
                        hlinkAway.IsEnabled = false;
                    }
                    hlinkAway.Click += GameList_Click;
                    ContentPanel.Children.Add(hlinkAway);
                }
                catch (Exception)
                {

                }



                //Set Home Team Properties
                try
                {
                    hlinkHome = new Button { Content = homeTeam.ToUpper() };
                    hlinkHome.FontSize = 22.667;
                    hlinkHome.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                    hlinkHome.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                    hlinkHome.Margin = new Thickness(horizMargin + 218, heightMargin - 7, 0, 0);
                    SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.White);
                    hlinkHome.BorderBrush = brush;
                    hlinkHome.BorderThickness = new Thickness(1, 1, 1, 1);
                    hlinkHome.Width = 85;
                    hlinkHome.Click += GameList_Click;
                    ContentPanel.Children.Add(hlinkHome);
                    if ((hometagData.EndsWith(".mp4")) || (hometagData.EndsWith(".m3u8")))
                    {
                        hlinkHome.Tag = hometagData;
                    }
                    else
                    {
                        hlinkHome.IsEnabled = false;
                    }

                    hlinkHome.Click += GameList_Click;
                    ContentPanel.Children.Add(hlinkHome);
                }
                catch (Exception)
                {

                }

                //Home Team Picture
                try
                {
                    BitmapImage homeBmp = new BitmapImage();
                    homeBmp.UriSource = new Uri(@"ms-appx:///Assets/Logos/" + homeTeam + ".png", UriKind.Absolute);
                    imgHome = new Image { Source = homeBmp };
                    imgHome.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                    imgHome.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                    imgHome.Margin = new Thickness(horizMargin + 313, heightMargin-6, 0, 0);
                    imgHome.Height = 45;
                    imgHome.Width = 60;
                    imgHome.Stretch = Stretch.Fill;
                    ContentPanel.Children.Add(imgHome);
                }
                catch
                {
                    BitmapImage homeBmp = new BitmapImage();

                    imgHome = new Image();
                    imgHome.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                    imgHome.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                    imgHome.Margin = new Thickness(horizMargin + 313, heightMargin-6, 0, 0);
                    imgHome.Height = 45;
                    imgHome.Width = 60;
                    imgHome.Stretch = Stretch.Fill;
                    ContentPanel.Children.Add(imgHome);
                }

                heightMargin = heightMargin + 55;
            }

            //if (ContentPanel.Children.Count == 0)
            //{
            //    txtNoGames.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    txtNoGames.Visibility = Visibility.Collapsed;
            //}

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
