using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Colors
using SharpDX.Direct2D1;
using CommonDX;
using SharpDX;
using SharpDX.DXGI;

using PerfectFractalZoomer.Fractal.MandelbrotLib;

namespace GridUI.DataModel.Drawers
{
    class TrajectoryMandelbrotDrawer : DataItem
    {
        private Color color = new Color(255);
        private Trajectory trajectory;
        private int iters;

        public TrajectoryMandelbrotDrawer(String uniqueId, String title, String imagePath, Color color, Trajectory trajectory, int iters, DataGroup group)
            : base(uniqueId, title, imagePath, group)
        {
            this.color = color;
            this.trajectory = trajectory;
            this.iters = iters;
        }

        public override void drawContent(TargetBase target)
        {
            DeviceContext context = target.DeviceManager.ContextDirect2D;

            int WIDTH = context.PixelSize.Width;
            int HEIGHT = context.PixelSize.Height;
            Mandelbrot engine = new BasicMandelbrot(iters);
            MandelbrotView view = new TrajectoryMandelbrotView(engine, trajectory, WIDTH, HEIGHT);

            int[] data = new int[WIDTH * HEIGHT];
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    float val = view.pixelAt(x, y);
                    int intVal = (int)(255 * val);
                    data[y * WIDTH + x] = 0 | intVal << 16 | intVal << 8 | intVal;
                }
            }

            DrawingSize size = new DrawingSize(WIDTH, HEIGHT);
            PixelFormat format = new PixelFormat(Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Ignore);
            BitmapProperties props = new BitmapProperties(format);
            Bitmap buf = Bitmap.New<int>(context, size, data, props);

            context.BeginDraw();
            context.Clear(Color.Black);

            context.DrawBitmap(buf, 1.0f, BitmapInterpolationMode.Linear);

            context.EndDraw();
            view.stepTime();
        }
    }
}
