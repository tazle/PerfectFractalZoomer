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

// Bitmap
using Windows.UI.Xaml.Media.Imaging;
// Colors
using Windows.UI;


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
        private WriteableBitmap bmp;
        private DataItem item;

        public ItemDetailPage()
        {
            this.InitializeComponent();

            // Show fps counter
            Application.Current.DebugSettings.EnableFrameRateCounter = true;
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

            // Create bitmap and set it to black
            bmp = BitmapFactory.New((int)ViewPortContainer.Width, (int)ViewPortContainer.Height);
            bmp.Clear(Colors.Black);
            ImageViewport.Source = bmp;

            // Allow saved page state to override the initial item to display
            if (pageState != null && pageState.ContainsKey("SelectedItem"))
            {
                navigationParameter = pageState["SelectedItem"];
            }

            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            item = DataSource.GetItem((String)navigationParameter);
            this.DefaultViewModel["Group"] = item.Group;
            
            item.drawContent(bmp);
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
    }
}