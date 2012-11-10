using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Colors
using SharpDX.Direct2D1;
using CommonDX;
using SharpDX;



namespace GridUI.DataModel.Drawers
{
    class LineDrawer : DataItem
    {
        private Color color = new Color(255);

        public LineDrawer(String uniqueId, String title, String imagePath, Color color, DataGroup group)
            : base(uniqueId, title, imagePath, group)
        {
            this.color = color;
        }

        public override void drawContent(TargetBase target)
        {
            DeviceContext context = target.DeviceManager.ContextDirect2D;
            context.BeginDraw();
            context.Clear(Color.Black);

            // Add something http://writeablebitmapex.codeplex.com
            for (int i = 0; i < context.PixelSize.Height; i += 4)
            {
                context.DrawLine(new DrawingPointF(0,i), new DrawingPointF(context.PixelSize.Width, i), new SolidColorBrush(context, color));
            }
            context.EndDraw();
        }
    }
}
