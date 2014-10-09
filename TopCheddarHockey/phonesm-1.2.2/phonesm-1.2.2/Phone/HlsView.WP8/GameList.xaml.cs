using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Xml.Linq;
using System.Xml;
using System.Windows.Media;
using System.IO.IsolatedStorage;
using Newtonsoft.Json.Linq;

namespace HlsView
{
    public partial class GameList : PhoneApplicationPage
    {
        private IsolatedStorageSettings userSettings = IsolatedStorageSettings.ApplicationSettings;
        private ProgressIndicator _progressIndicator = new ProgressIndicator(); //Create progress indicator to indicate system busy.
        JArray fullSchedule = new JArray();
        string gameDate = "";
        string gameYear = "";
        int heightMargin = 15;
        int horizMargin = 40;

        //FOR BANDWIDTH SELECTION SUPPORT
        //private string hostBase;
        //private string defaultBase;

        public GameList()
        {
            InitializeComponent();
            _progressIndicator.IsVisible = false;
            _progressIndicator.IsIndeterminate = false;
            _progressIndicator.Text = "Getting Data...";
            SystemTray.SetProgressIndicator(this, _progressIndicator);

        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            ShowProgressIndicator();
            RemoveContent();
            NavigationContext.QueryString.TryGetValue("date", out gameDate);
            
            gameYear = gameDate[0].ToString() + gameDate[1].ToString() + gameDate[2].ToString() + gameDate[3].ToString();
            string gameYearPlus = gameDate[0].ToString() + gameDate[1].ToString() + gameDate[2].ToString() + (Convert.ToInt32(gameDate[3].ToString()) + 1).ToString();
            string videoPage = "http://live.nhl.com/GameData/SeasonSchedule-"+gameYear + gameYearPlus+".json";
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += GetAllGames;
            wc.DownloadStringAsync(new Uri(videoPage));
        }

        private void GetAllGames(object sender, DownloadStringCompletedEventArgs e)
        {
            if (fullSchedule.Count == 0)
            {
                JObject o = new JObject();
                try
                {
                    string fullString = "{games: " + e.Result + "}";
                    o = JObject.Parse(fullString);
                    foreach (JToken game in o["games"])
                    {
                        fullSchedule.Add(game);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            foreach (JToken gameID in fullSchedule)
            {
                if (gameID["est"].ToString().Split(new Char[] {' '})[0] == gameDate )
                {
                    string gameDetailID = gameID["id"].ToString()[4].ToString() + gameID["id"].ToString()[5].ToString() + "_" + gameID["id"].ToString()[6].ToString() + gameID["id"].ToString()[7].ToString() + gameID["id"].ToString()[8].ToString() + gameID["id"].ToString()[9].ToString();
                    string gameDetailPage = "http://smb.cdnak.neulion.com/fs/nhl/mobile/feed_new/data/streams/" + gameYear + "/iphone/"+ gameDetailID +".json";
                    ShowProgressIndicator();
                    WebClient wClient = new WebClient();
                    wClient.DownloadStringCompleted += linksClient_DownloadStringCompleted;
                    wClient.DownloadStringAsync(new Uri(gameDetailPage));
                }
            }


            System.Threading.Thread.Sleep(1500);

            HideProgressIndicator();

        }

        void ShowProgressIndicator()
        {
            _progressIndicator.IsVisible = true;
            _progressIndicator.IsIndeterminate = true;
        }

        void HideProgressIndicator()
        {
            _progressIndicator.IsVisible = false;
            _progressIndicator.IsIndeterminate = false;
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

            if ((homeTeam.Length == 3) && (awayTeam.Length ==3))
            {

                //Set Away Team Image
                try
                {
                    imgAway.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(@"\Assets\Logos\" + awayTeam + ".png");
                    imgAway.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    imgAway.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    imgAway.Margin = new Thickness(horizMargin + 0, heightMargin - 6, 0, 0);
                    imgAway.Height = 45;
                    imgAway.Width = 60;
                    imgAway.Stretch = Stretch.Fill;
                    ContentPanel.Children.Add(imgAway);
                }
                catch
                {
                    imgAway.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    imgAway.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    imgAway.Margin = new Thickness(horizMargin + 0, heightMargin - 6, 0, 0);
                    imgAway.Height = 45;
                    imgAway.Width = 60;
                    imgAway.Stretch = Stretch.Fill;
                    ContentPanel.Children.Add(imgAway);
                }

                //Set Away Team Properties
                try
                {

                    hlinkAway = new Button { Content = awayTeam.ToUpper() };
                    hlinkAway.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    hlinkAway.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    hlinkAway.Margin = new Thickness(horizMargin + 60, heightMargin - 20, 0, 0);
                    hlinkAway.Width = 100;
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
                    hlinkHome.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    hlinkHome.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    hlinkHome.Margin = new Thickness(horizMargin + 205, heightMargin - 20, 0, 0);
                    hlinkHome.Width = 100;
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
                    imgHome.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(@"\Assets\Logos\" + homeTeam + ".png");
                    imgHome.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    imgHome.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    imgHome.Margin = new Thickness(horizMargin + 308, heightMargin - 6, 0, 0);
                    imgHome.Height = 45;
                    imgHome.Width = 60;
                    imgHome.Stretch = Stretch.Fill;
                    ContentPanel.Children.Add(imgHome);
                }
                catch
                {
                    imgHome.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    imgHome.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    imgHome.Margin = new Thickness(horizMargin + 308, heightMargin - 6, 0, 0);
                    imgHome.Height = 45;
                    imgHome.Width = 60;
                    imgHome.Stretch = Stretch.Fill;
                    ContentPanel.Children.Add(imgHome);
                }

                heightMargin = heightMargin + 55;
            }



        }

        private void RemoveContent()
        {
            heightMargin = 15;
            horizMargin = 40;
            int contentLength = ContentPanel.Children.Count;
            for (int child = 0; child < contentLength; child++)
            {
                ContentPanel.Children.RemoveAt(0);

            }
        }

        void linksClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string gametype;
            string element = "";
            string homeTagUrl = "";
            string awayTagUrl = "";

            try
            {
                gametype = (string)userSettings["GameType"];
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
                o = JObject.Parse(e.Result);
            }
            catch (Exception)
            {
                
            }
            try
            {
                homeTagUrl = o["gameStreams"]["iphone"]["home"][element]["bitrate0"].ToString();
            }
            catch
            {
                homeTagUrl = "";
            }

            try
            {
                awayTagUrl = o["gameStreams"]["iphone"]["away"][element]["bitrate0"].ToString();
            }
            catch
            {
                awayTagUrl = "";
            }

            string homeTeam = "";
            string awayTeam = "";

            try
            {

                string contURL = o["gameStreams"]["iphone"]["home"]["vod-continuous"]["bitrate0"].ToString();
                homeTeam = contURL.Split(new Char[] { '_' })[3].ToString();
                awayTeam = contURL.Split(new Char[] { '_' })[2].ToString();
            }
            catch
            {

            }

            CreateTeamButtons(awayTeam, homeTeam, awayTagUrl, homeTagUrl);
            
        }

        void GameList_Click(object sender, RoutedEventArgs e)
        {
            Button target = sender as Button;
            if (target.Tag.ToString().EndsWith("m3u8"))
            {
                //defaultBase = target.Tag.ToString();
                //GetBandwidth(target.Tag.ToString());
                NavigationService.Navigate(new Uri("/MainPage.xaml?source=" + target.Tag.ToString(), UriKind.Relative));
            }
            else
            {
                NavigationService.Navigate(new Uri("/HighlightViewer.xaml?source=" + target.Tag.ToString(), UriKind.Relative));
            }

        }

        //FOR BANDWIDTH SELECTION SUPPORT
        //private void GetBandwidth(string url)
        //{
        //    WebClient wClient = new WebClient();
        //    wClient.DownloadStringCompleted += wClient_DownloadStringCompleted;
        //    Uri targetSource = new Uri(url, UriKind.Absolute);
        //    hostBase = "http://" + targetSource.Host;
        //    for (int s = 0; s < targetSource.Segments.Length - 1; s++)
        //    {
        //        hostBase = hostBase + targetSource.Segments[s].ToString();
        //    }
        //    wClient.DownloadStringAsync(targetSource);
            
        //}

        //void wClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        //{
        //    string[] returnData = e.Result.Split(new Char[] { ',', '\n' });

        //    //Search array of bandwidths for proper bandwidth url
        //    int strIndex = 0;
        //    for (int strNumber = 0; strNumber < returnData.Length; strNumber++)
        //    {
        //        if (returnData[strNumber] == @"BANDWIDTH=1200000")
        //        {
        //            if (strIndex >= 0)
        //            {
        //                strIndex = strNumber;
        //                break;
        //            }
                    
        //        }
                
        //    }
        //    hostBase = hostBase + returnData[strIndex + 1].ToString();
        //    if (hostBase.EndsWith(".m3u8"))
        //    {
        //        NavigationService.Navigate(new Uri("/MainPage.xaml?source=" + hostBase, UriKind.Relative));
        //    }
        //    else
        //    {
        //        NavigationService.Navigate(new Uri("/MainPage.xaml?source=" + defaultBase, UriKind.Relative));
        //    }
            
            
        //}


        private void Image_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
    }
}