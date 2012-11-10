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
        private Color color = Color.White;
        Random random = new Random();

        public PixelDrawer(String uniqueId, String title, String imagePath, Color color, DataGroup group)
            : base(uniqueId, title, imagePath, group)
        {
            this.color = color;
        }

        public override void drawContent(TargetBase target)
        {
            DeviceContext context = target.DeviceManager.ContextDirect2D;
            Brush pixelBrush = new SolidColorBrush(context, color);
            Brush dimmingBrush = new SolidColorBrush(context, new Color(0,0,0,10));

            int x = random.Next(0, context.PixelSize.Width);
            int y = random.Next(0, context.PixelSize.Height);

            context.BeginDraw();

            // Dim
            context.FillRectangle(new RectangleF(0, 0, context.PixelSize.Width, context.PixelSize.Width), dimmingBrush);
            context.FillRectangle(new RectangleF(x,y,x+2,y+2), pixelBrush);

            context.EndDraw();
        }
    }
}
