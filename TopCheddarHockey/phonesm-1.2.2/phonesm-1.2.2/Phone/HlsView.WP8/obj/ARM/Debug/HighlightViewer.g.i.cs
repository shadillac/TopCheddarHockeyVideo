﻿#pragma checksum "C:\Users\shmorris\Desktop\phonesm-1.2.2\phonesm-1.2.2\Phone\HlsView.WP8\HighlightViewer.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "376279B782CE903767F2275DC19B1461"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace HlsView {
    
    
    public partial class HighlightViewer : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid ContentRoot;
        
        internal System.Windows.Controls.MediaElement mdaHighView;
        
        internal System.Windows.Controls.Button btnPlay;
        
        internal System.Windows.Controls.Button btnStop;
        
        internal System.Windows.Controls.Button btnPause;
        
        internal System.Windows.Controls.Button btnRew;
        
        internal System.Windows.Controls.Button btnFF;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/HlsView8;component/HighlightViewer.xaml", System.UriKind.Relative));
            this.ContentRoot = ((System.Windows.Controls.Grid)(this.FindName("ContentRoot")));
            this.mdaHighView = ((System.Windows.Controls.MediaElement)(this.FindName("mdaHighView")));
            this.btnPlay = ((System.Windows.Controls.Button)(this.FindName("btnPlay")));
            this.btnStop = ((System.Windows.Controls.Button)(this.FindName("btnStop")));
            this.btnPause = ((System.Windows.Controls.Button)(this.FindName("btnPause")));
            this.btnRew = ((System.Windows.Controls.Button)(this.FindName("btnRew")));
            this.btnFF = ((System.Windows.Controls.Button)(this.FindName("btnFF")));
        }
    }
}

