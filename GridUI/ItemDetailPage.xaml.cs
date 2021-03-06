﻿using GridUI.DataModel;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Graphics.Display;

// Bitmap
using CommonDX;
using SharpDX;
using SharpDX.IO;



// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace GridUI
{
    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to
    /// flip through other items belonging to the same group.
    /// </summary>
    public sealed partial class ItemDetailPage : GridUI.Common.LayoutAwarePage
    {
        // Writable bitmap
        private DataItem item;
        private SurfaceImageSourceTarget d2dTarget;

        public ItemDetailPage()
        {
            this.InitializeComponent();

            // Show fps counter
            Application.Current.DebugSettings.EnableFrameRateCounter = true;

            this.Loaded += load;
            this.Unloaded += unload;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // Allow saved page state to override the initial item to display
            if (pageState != null && pageState.ContainsKey("SelectedItem"))
            {
                navigationParameter = pageState["SelectedItem"];
            }

            item = DataSource.GetItem((String)navigationParameter);
            this.DefaultViewModel["Group"] = item.Group;
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            pageState["SelectedItem"] = item.UniqueId;
        }

        private void load(Object sender, RoutedEventArgs e)
        {
            // Create bitmap and set it to black
            ImageBrush d2dBrush = new ImageBrush();
            d2dRectangle.Fill = d2dBrush;

            DeviceManager deviceManager = new DeviceManager();

            DrawingSize size = new DrawingSize((int)d2dContainer.ActualWidth, (int)d2dContainer.ActualHeight);

            d2dTarget = new SurfaceImageSourceTarget(size.Width, size.Height);
            d2dBrush.ImageSource = d2dTarget.ImageSource;

            deviceManager.OnInitialize += d2dTarget.Initialize;
            deviceManager.Initialize(DisplayProperties.LogicalDpi);

            d2dTarget.OnRender += item.drawContent;
            item.initContent(d2dTarget, size);

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void unload(Object sender, RoutedEventArgs e)
        {
            d2dTarget.Dispose();
            item.destroyContent();
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
        }

        void CompositionTarget_Rendering(object sender, object e)
        {
            d2dTarget.RenderAll();
        }
    }
}
