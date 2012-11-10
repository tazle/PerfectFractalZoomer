using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Bitmap
using Windows.UI.Xaml.Media.Imaging;
// Colors
using Windows.UI;


namespace GridUI.DataModel.Drawers
{
    class LineDrawer : DataItem
    {
        private Color color = Colors.White;

        public LineDrawer(String uniqueId, String title, String imagePath, Color color, DataGroup group)
            : base(uniqueId, title, imagePath, group)
        {
            this.color = color;
        }

        public override void drawContent(Windows.UI.Xaml.Media.Imaging.WriteableBitmap bmp)
        {
            // Add something http://writeablebitmapex.codeplex.com

            for (int i = 0; i < bmp.PixelHeight; i += 4)
            {
                bmp.DrawLine(0, i, bmp.PixelWidth, i, color);
                bmp.DrawLine(0, i, bmp.PixelWidth, i+1, color);
            }
        }
    }
}
