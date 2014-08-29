using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace HlsView
{
    public partial class Settings : PhoneApplicationPage
    {
        private IsolatedStorageSettings userSettings = IsolatedStorageSettings.ApplicationSettings;

        public Settings()
        {
            InitializeComponent();
        }

        private void chkHideScores_Checked(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string hideScores = "";
            string gameType = "";
            try
            {
                hideScores = (string)userSettings["HideScores"];
                gameType = (string)userSettings["GameType"];
            }
            catch
            {
                hideScores = "null";
                gameType = "null";
            }

            if (hideScores == "1")
            {
                chkHideScores.IsChecked = true;
            }
            
            switch (gameType)
            {
                case "FullGame":
                    rdoFullGames.IsChecked = true;
                    rdoHighlights.IsChecked = false;
                    rdoCondensed.IsChecked = false;
                    break;
                case "Highlights":
                    rdoFullGames.IsChecked = false;
                    rdoHighlights.IsChecked = true;
                    rdoCondensed.IsChecked = false;
                    break;
                case "Condensed":
                    rdoFullGames.IsChecked = false;
                    rdoHighlights.IsChecked = false;
                    rdoCondensed.IsChecked = true;
                    break;
                default:
                    rdoFullGames.IsChecked = false;
                    rdoHighlights.IsChecked = true;
                    rdoCondensed.IsChecked = false;
                    break;
            }
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (chkHideScores.IsChecked == true)
            {
                try
                {
                    userSettings.Add("HideScores", "1");
                }
                catch(ArgumentException)
                {
                    userSettings["HideScores"] = "1";
                }
                
            }
            else
            {
                try
                {
                    userSettings.Add("HideScores", "0");
                }
                catch (ArgumentException)
                {
                    userSettings["HideScores"] = "0";
                }
            }
            if (rdoHighlights.IsChecked == true)
            {
                try
                {
                    userSettings.Add("GameType", "Highlights");
                }
                catch (ArgumentException)
                {
                    userSettings["GameType"] = "Highlights";
                }                
            }
            else if (rdoFullGames.IsChecked == true)
            {
                try
                {
                    userSettings.Add("GameType", "FullGame");
                }
                catch (ArgumentException)
                {
                    userSettings["GameType"] = "FullGame";
                }
            }
            else if (rdoCondensed.IsChecked == true)
            {
                try
                {
                    userSettings.Add("GameType", "Condensed");
                }
                catch (ArgumentException)
                {
                    userSettings["GameType"] = "Condensed";
                }
            }
            
            NavigationService.GoBack();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            rdoFullGames.IsChecked = false;
            rdoCondensed.IsChecked = false;
        }

        private void rdoFullGames_Checked(object sender, RoutedEventArgs e)
        {
            rdoHighlights.IsChecked = false;
            rdoCondensed.IsChecked = false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.GoBack();
        }

        private void rdoCondensed_Checked(object sender, RoutedEventArgs e)
        {
            rdoHighlights.IsChecked = false;
            rdoFullGames.IsChecked = false;
        }
    }
}