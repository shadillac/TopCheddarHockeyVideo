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

namespace HlsView
{
    public partial class GameList : PhoneApplicationPage
    {
        private IsolatedStorageSettings userSettings = IsolatedStorageSettings.ApplicationSettings;
        //FOR BANDWIDTH SELECTION SUPPORT
        //private string hostBase;
        //private string defaultBase;

        public GameList()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            string gameDate = "";
            NavigationContext.QueryString.TryGetValue("date", out gameDate);
            string videoPage = "http://feeds.cdnak.neulion.com/fs/nhl/mobile/feeds/data/" + gameDate + ".xml";
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += HttpCompleted;
            wc.DownloadStringAsync(new Uri(videoPage));
        }

        private void HttpCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                XDocument xdoc = XDocument.Parse(e.Result, LoadOptions.None);
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
                int heightMargin = 15;
                int horizMargin = 40;
                string scores;
                string gametype;
                string element;

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

                if (xdoc.Descendants("game").Count() > 0)
                {

                    foreach (XElement xe in xdoc.Descendants("game"))
                    {
                        //Set Away Team Image
                        try
                        {
                            imgAway[i] = new Image { Source = (ImageSource)new ImageSourceConverter().ConvertFromString(@"\Assets\Logos\" + xe.Element("away-team").Element("team-abbreviation").Value + ".png") };
                            imgAway[i].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                            imgAway[i].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                            imgAway[i].Margin = new Thickness(horizMargin + 0, heightMargin - 6, 0, 0);
                            imgAway[i].Height = 45;
                            imgAway[i].Width = 60;
                            imgAway[i].Stretch = Stretch.Fill;
                            ContentPanel.Children.Add(imgAway[i]);
                        }
                        catch
                        {
                            imgAway[i] = new Image();
                            imgAway[i].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                            imgAway[i].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                            imgAway[i].Margin = new Thickness(horizMargin + 0, heightMargin - 6, 0, 0);
                            imgAway[i].Height = 45;
                            imgAway[i].Width = 60;
                            imgAway[i].Stretch = Stretch.Fill;
                            ContentPanel.Children.Add(imgAway[i]);
                        }


                        //Set Away Team Properties
                        try
                        {

                            hlinkAway[i] = new Button { Content = xe.Element("away-team").Element("team-abbreviation").Value };
                            hlinkAway[i].Tag = xe.Element("streams").Element("iphone").Element("away").Element(element).Value.ToString();
                            hlinkAway[i].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                            hlinkAway[i].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                            hlinkAway[i].Margin = new Thickness(horizMargin + 60, heightMargin - 20, 0, 0);
                            hlinkAway[i].Width = 100;
                            hlinkAway[i].Click += GameList_Click;
                            ContentPanel.Children.Add(hlinkAway[i]);
                        }
                        catch (Exception)
                        {
                            blkAway[i] = new TextBlock { Text = xe.Element("away-team").Element("team-abbreviation").Value };
                            blkAway[i].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                            blkAway[i].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                            blkAway[i].Margin = new Thickness(horizMargin + 87, heightMargin, 0, 0);
                            blkAway[i].FontSize = 22.667;
                            ContentPanel.Children.Add(blkAway[i]);
                        }

                        try
                        {
                            scores = (string)userSettings["HideScores"];
                        }
                        catch (NullReferenceException)
                        {
                            scores = "0";
                        }
                        catch (KeyNotFoundException)
                        {
                            scores = "0";
                        }

                        if (scores != "1")
                        {
                            try
                            {
                                //Set Away Goals
                                awayGoals[i] = new TextBlock { Text = xe.Element("away-team").Element("goals").Value };
                                awayGoals[i].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                                awayGoals[i].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                                awayGoals[i].Margin = new Thickness(horizMargin + 160, heightMargin, 0, 0);
                                ContentPanel.Children.Add(awayGoals[i]);

                                //Set Home Goals
                                homeGoals[i] = new TextBlock { Text = xe.Element("home-team").Element("goals").Value };
                                homeGoals[i].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                                homeGoals[i].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                                homeGoals[i].Margin = new Thickness(horizMargin + 193, heightMargin, 0, 0);
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
                            hlinkHome[i].Tag = xe.Element("streams").Element("iphone").Element("home").Element(element).Value.ToString();
                            hlinkHome[i].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                            hlinkHome[i].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                            hlinkHome[i].Margin = new Thickness(horizMargin + 205, heightMargin - 20, 0, 0);
                            hlinkHome[i].Width = 100;
                            hlinkHome[i].Click += GameList_Click;
                            ContentPanel.Children.Add(hlinkHome[i]);
                        }
                        catch (Exception)
                        {
                            blkHome[i] = new TextBlock { Text = xe.Element("home-team").Element("team-abbreviation").Value };
                            blkHome[i].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                            blkHome[i].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                            blkHome[i].Margin = new Thickness(horizMargin + 222, heightMargin, 0, 0);
                            blkHome[i].FontSize = 22.667;
                            ContentPanel.Children.Add(blkHome[i]);
                        }

                        //Home Team Picture
                        try
                        {
                            imgHome[i] = new Image { Source = (ImageSource)new ImageSourceConverter().ConvertFromString(@"\Assets\Logos\" + xe.Element("home-team").Element("team-abbreviation").Value + ".png") };
                            imgHome[i].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                            imgHome[i].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                            imgHome[i].Margin = new Thickness(horizMargin + 308, heightMargin - 6, 0, 0);
                            imgHome[i].Height = 45;
                            imgHome[i].Width = 60;
                            imgHome[i].Stretch = Stretch.Fill;
                            ContentPanel.Children.Add(imgHome[i]);
                        }
                        catch
                        {
                            imgHome[i] = new Image();
                            imgHome[i].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                            imgHome[i].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                            imgHome[i].Margin = new Thickness(horizMargin + 308, heightMargin - 6, 0, 0);
                            imgHome[i].Height = 45;
                            imgHome[i].Width = 60;
                            imgHome[i].Stretch = Stretch.Fill;
                            ContentPanel.Children.Add(imgHome[i]);
                        }


                        heightMargin = heightMargin + 55;
                        i++;
                    }
                }
                else
                {
                    MessageBox.Show("No NHL games found on the chosen date.", "Videos not Found", MessageBoxButton.OK);
                    NavigationService.GoBack();
                }
            }
            catch (System.Reflection.TargetInvocationException)
            {
                MessageBox.Show("No NHL games found on the chosen date.", "Videos not Found", MessageBoxButton.OK);
                NavigationService.GoBack();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No NHL games found on the chosen date.", "Videos not Found", MessageBoxButton.OK);
                NavigationService.GoBack();
            }
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