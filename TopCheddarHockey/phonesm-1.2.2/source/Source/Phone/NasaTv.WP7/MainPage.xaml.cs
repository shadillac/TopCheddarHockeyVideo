﻿// -----------------------------------------------------------------------
//  <copyright file="MainPage.xaml.cs" company="Henric Jungheim">
//  Copyright (c) 2012-2014.
//  <author>Henric Jungheim</author>
//  </copyright>
// -----------------------------------------------------------------------
// Copyright (c) 2012-2014 Henric Jungheim <software@henric.org>
// 
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SM.Media;
using SM.Media.Utility;
using SM.Media.Web;

namespace NasaTv
{
    public partial class MainPage : PhoneApplicationPage
    {
        static readonly IApplicationInformation ApplicationInformation = ApplicationInformationFactory.Default;
        readonly HttpClients _httpClients;
        readonly MediaElementManager _mediaElementManager;
        readonly MediaManagerParameters _mediaManagerParameters;
        IMediaStreamFascade _mediaStreamFascade;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            _httpClients = new HttpClients(userAgent: ApplicationInformation.CreateUserAgent());

            _mediaElementManager = new MediaElementManager(Dispatcher,
                () =>
                {
                    var me = new MediaElement
                             {
                                 Margin = new Thickness(0)
                             };

                    me.MediaFailed += mediaElement1_MediaFailed;
                    me.MediaEnded += mediaElement1_MediaEnded;
                    me.CurrentStateChanged += mediaElement1_CurrentStateChanged;

                    ContentPanel.Children.Add(me);

                    mediaElement1 = me;

                    UpdateState(MediaElementState.Opening);

                    return me;
                },
                me =>
                {
                    if (null != mediaElement1)
                    {
                        Debug.Assert(ReferenceEquals(me, mediaElement1));

                        ContentPanel.Children.Remove(me);

                        me.MediaFailed -= mediaElement1_MediaFailed;
                        me.MediaEnded -= mediaElement1_MediaEnded;
                        me.CurrentStateChanged -= mediaElement1_CurrentStateChanged;

                        mediaElement1 = null;
                    }

                    UpdateState(MediaElementState.Closed);
                });

            foreach (ApplicationBarIconButton ib in ApplicationBar.Buttons)
            {
                switch (ib.Text)
                {
                    case "stop":
                        stopButton = ib;
                        break;
                    case "play":
                        playButton = ib;
                        break;
                }
            }

            OnStop();
        }

        void OnPlay()
        {
            errorBox.Visibility = Visibility.Collapsed;
            playButton.IsEnabled = false;
            LayoutRoot.Background.Opacity = 0.25;
        }

        void OnStop()
        {
            playButton.IsEnabled = true;
            stopButton.IsEnabled = false;
            ApplicationBar.IsVisible = true;
            LayoutRoot.Background.Opacity = 1;
        }

        void playButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Play clicked");

            // Try to avoid shattering MediaElement's glass jaw.
            if (null != mediaElement1 && MediaElementState.Closed != mediaElement1.CurrentState)
            {
                CleanupMedia();

                return;
            }

            OnPlay();

            if (null != _mediaStreamFascade)
            {
                _mediaStreamFascade.StateChange -= TsMediaManagerOnStateChange;
                _mediaStreamFascade.DisposeSafe();
            }

            _mediaStreamFascade = MediaStreamFascadeSettings.Parameters.Create(_httpClients, _mediaElementManager.SetSourceAsync);

            _mediaStreamFascade.SetParameter(_mediaElementManager);

            _mediaStreamFascade.Source = new Uri(
                "http://www.nasa.gov/multimedia/nasatv/NTV-Public-IPS.m3u8"
                //"http://devimages.apple.com/iphone/samples/bipbop/bipbopall.m3u8"
                );

            _mediaStreamFascade.StateChange += TsMediaManagerOnStateChange;

            _mediaStreamFascade.Play();
        }

        void mediaElement1_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            errorBox.Text = e.ErrorException.Message;
            errorBox.Visibility = Visibility.Visible;

            CleanupMedia();

            playButton.IsEnabled = true;
        }

        void CleanupMedia()
        {
            if (null != _mediaStreamFascade)
                _mediaStreamFascade.RequestStop();
        }

        void mediaElement1_MediaEnded(object sender, RoutedEventArgs e)
        {
            CleanupMedia();
        }

        void stopButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Stop clicked");

            CleanupMedia();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            CleanupMedia();

            if (null != _mediaElementManager)
                _mediaElementManager.CloseAsync().Wait();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (null != _mediaElementManager)
                _mediaElementManager.CloseAsync().Wait();
        }

        void PhoneApplicationPageTap(object sender, GestureEventArgs e)
        {
            Debug.WriteLine("Tapped");

            var state = null == mediaElement1 ? MediaElementState.Closed : mediaElement1.CurrentState;

            if (MediaElementState.Closed == state)
            {
                ApplicationBar.IsVisible = true;
                SystemTray.IsVisible = true;
            }
            else
            {
                ApplicationBar.IsVisible = !ApplicationBar.IsVisible;
                SystemTray.IsVisible = ApplicationBar.IsVisible;
            }
        }

        void PhoneApplicationPageLoaded(object sender, RoutedEventArgs e)
        {
            SystemTray.IsVisible = ApplicationBar.IsVisible;
        }

        void about_Click(object sender, EventArgs e)
        {
            NavigateTo("/Views/About.xaml");
        }

        void settings_Click(object sender, EventArgs e)
        {
            NavigateTo("/Views/Settings.xaml");
        }

        static void NavigateTo(string viewsAboutXaml)
        {
            var root = Application.Current.RootVisual as PhoneApplicationFrame;
            if (null == root)
                return;

            root.Navigate(new Uri(viewsAboutXaml, UriKind.Relative));
        }

        void mediaElement1_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            UpdateState();
        }

        void TsMediaManagerOnStateChange(object sender, TsMediaManagerStateEventArgs tsMediaManagerStateEventArgs)
        {
            Dispatcher.InvokeAsync(() =>
                                   {
                                       var message = tsMediaManagerStateEventArgs.Message;

                                       if (!string.IsNullOrWhiteSpace(message))
                                       {
                                           errorBox.Text = message;
                                           errorBox.Visibility = Visibility.Visible;
                                       }

                                       UpdateState();
                                   });
        }

        void UpdateState()
        {
            var state = MediaElementState.Closed;

            if (null != mediaElement1)
                state = mediaElement1.CurrentState;

            UpdateState(state);
        }

        void UpdateState(MediaElementState state)
        {
            Debug.WriteLine("MediaElement State: " + state);

            if (MediaElementState.Closed == state)
                OnStop();
            else
                stopButton.IsEnabled = true;
        }
    }
}
