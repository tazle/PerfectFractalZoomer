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

// Colors
using Windows.UI;
// Bitmap
using Windows.UI.Xaml.Media.Imaging;

// FPS
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PerfectFractalZoomer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Writable bitmap
        WriteableBitmap bmp;
                
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = base.MeasureOverride(availableSize);
            return size;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            initBitmap(new Size(ViewPortContainer.Width, ViewPortContainer.Height));
        }

        private void initBitmap(Size size) {
            // Show fps counter
            Application.Current.DebugSettings.EnableFrameRateCounter = true;

            // Create bitmap and set it to black
            bmp = BitmapFactory.New((int) size.Width, (int) size.Height);
            bmp.Clear(Colors.Black);
            ImageViewport.Source = bmp;
            
            // Render callback
            //CompositionTarget.Rendering += CompositionTarget_Rendering;

            // Add something http://writeablebitmapex.codeplex.com
            //   # #
            //   # #
            // #     #
            //  #####
            bmp.SetPixel(10, 12, Colors.White);
            bmp.SetPixel(11, 12, Colors.White);
            
            bmp.SetPixel(10, 14, Colors.White);
            bmp.SetPixel(11, 14, Colors.White);

            bmp.SetPixel(12, 10, Colors.White);
            bmp.SetPixel(12, 16, Colors.White);
            bmp.SetPixel(13, 11, Colors.White);
            bmp.SetPixel(13, 12, Colors.White);
            bmp.SetPixel(13, 13, Colors.White);
            bmp.SetPixel(13, 14, Colors.White);
            bmp.SetPixel(13, 15, Colors.White);
        }
    }
}
