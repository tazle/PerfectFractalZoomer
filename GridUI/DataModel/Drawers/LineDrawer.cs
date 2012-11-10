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
        private Color color = Color.White;
        Random random = new Random();

        public LineDrawer(String uniqueId, String title, String imagePath, Color color, DataGroup group)
            : base(uniqueId, title, imagePath, group)
        {
            this.color = color;
        }

        public override void drawContent(TargetBase target)
        {
            DeviceContext context = target.DeviceManager.ContextDirect2D;
            Brush lineBrush = new SolidColorBrush(context, color);
            Brush dimmingBrush = new SolidColorBrush(context, new Color(0, 0, 0, 10));

            int y = random.Next(0, context.PixelSize.Height);

            context.BeginDraw();

            // Dim
            context.FillRectangle(new RectangleF(0, 0, context.PixelSize.Width, context.PixelSize.Width), dimmingBrush);

            context.DrawLine(new DrawingPointF(0, y), new DrawingPointF(context.PixelSize.Width, y), lineBrush);

            context.EndDraw();

        }
    }
}
