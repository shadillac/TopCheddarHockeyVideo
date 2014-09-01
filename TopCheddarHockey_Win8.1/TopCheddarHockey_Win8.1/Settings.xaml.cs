﻿using TopCheddarHockey_Win8._1.Common;
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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace TopCheddarHockey_Win8._1
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Settings : Page
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


        public Settings()
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
            string hideScores = "";
            string gameType = "";
            try
            {
                hideScores = localSettings.Values["HideScores"].ToString();
                gameType = localSettings.Values["GameType"].ToString();
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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (chkHideScores.IsChecked == true)
            {
                try
                {
                    localSettings.Values["HideScores"] = "1";
                }
                catch (ArgumentException)
                {
                    localSettings.Values["HideScores"] = "1";
                }

            }
            else
            {
                try
                {
                    localSettings.Values["HideScores"] = "0";
                }
                catch (ArgumentException)
                {
                    localSettings.Values["HideScores"] = "0";
                }
            }
            if (rdoHighlights.IsChecked == true)
            {
                try
                {
                    localSettings.Values["GameType"] = "Highlights";
                }
                catch (ArgumentException)
                {
                    localSettings.Values["GameType"] = "Highlights";
                }
            }
            else if (rdoFullGames.IsChecked == true)
            {
                try
                {
                    localSettings.Values["GameType"] = "FullGame";
                }
                catch (ArgumentException)
                {
                    localSettings.Values["GameType"] = "FullGame";
                }
            }
            else if (rdoCondensed.IsChecked == true)
            {
                try
                {
                    localSettings.Values["GameType"] = "Condensed";
                }
                catch (ArgumentException)
                {
                    localSettings.Values["GameType"] = "Condensed";
                }
            }

            Frame.GoBack();
        }

        private void rdoFullGames_Checked(object sender, RoutedEventArgs e)
        {
            rdoHighlights.IsChecked = false;
            rdoCondensed.IsChecked = false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            Frame.GoBack();
        }

        private void rdoCondensed_Checked(object sender, RoutedEventArgs e)
        {
            rdoHighlights.IsChecked = false;
            rdoFullGames.IsChecked = false;
        }

        private void chkHideScores_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rdoHighlights_Checked(object sender, RoutedEventArgs e)
        {
            rdoFullGames.IsChecked = false;
            rdoCondensed.IsChecked = false;
        }

    }
}