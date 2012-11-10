using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Bitmap
using SharpDX.Direct2D1;
using CommonDX;
using SharpDX;

namespace GridUI.DataModel.Drawers
{
    class PixelDrawer : DataItem
    {
        private Color color = new Color(255);

        public PixelDrawer(String uniqueId, String title, String imagePath, Color color, DataGroup group)
            : base(uniqueId, title, imagePath, group)
        {
            this.color = color;
        }

        public override void drawContent(TargetBase target)
        {
            // Add something http://writeablebitmapex.codeplex.com
            //   # #
            //   # #
            // #     #
            //  #####
            /*context.DrawLine(new );
            bmp.SetPixel(10, 12, color);
            bmp.SetPixel(11, 12, color);

            bmp.SetPixel(10, 14, color);
            bmp.SetPixel(11, 14, color);

            bmp.SetPixel(12, 10, color);
            bmp.SetPixel(12, 16, color);
            bmp.SetPixel(13, 11, color);
            bmp.SetPixel(13, 12, color);
            bmp.SetPixel(13, 13, color);
            bmp.SetPixel(13, 14, color);
            bmp.SetPixel(13, 15, color);*/
        }
    }
}
