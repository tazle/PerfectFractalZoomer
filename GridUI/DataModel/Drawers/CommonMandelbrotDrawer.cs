using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Concurrent;

// Colors
using SharpDX.Direct2D1;
using CommonDX;
using SharpDX;
using SharpDX.DXGI;

using PerfectFractalZoomer.Fractal.MandelbrotLib;


namespace GridUI.DataModel.Drawers
{
    abstract class CommonMandelbrotDrawer : DataItem
    {
        private Color color = new Color(255);
        private Trajectory trajectory;
        private int iters;

        private float scaling = 1;

        private MandelbrotView view;
        private int[] data;
        private DeviceContext context;
        private DrawingSize size;
        private DrawingSize drawingSize;
        private Bitmap buf;

        public CommonMandelbrotDrawer(String uniqueId, String title, String imagePath, Color color, Trajectory trajectory, int iters, 
            DataGroup group, float scaling = 1.0f)
            : base(uniqueId, title, imagePath, group)
        {
            this.color = color;
            this.trajectory = trajectory;
            this.iters = iters;
            this.scaling = scaling;
        }


        public override void initContent(SurfaceImageSourceTarget target, DrawingSize pixelSize)
        {
            this.drawingSize = pixelSize;
            this.size = new DrawingSize((int) (pixelSize.Width/scaling), (int) (pixelSize.Height/scaling));
            context = target.DeviceManager.ContextDirect2D;

            Mandelbrot engine = new BasicMandelbrot(iters);
            view = new TrajectoryMandelbrotView(engine, trajectory, size.Width, size.Height);

            data = new int[size.Width * size.Height];

            PixelFormat format = new PixelFormat(Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Ignore);
            BitmapProperties props = new BitmapProperties(format);

            buf = Bitmap.New<int>(context, size, data, props);
        }

        public override void drawContent(TargetBase target)
        {
            var rangePartitioner = Partitioner.Create(0, size.Height);
            Parallel.ForEach(rangePartitioner, (range, loopState) =>
            {
                for (int y = range.Item1 ; y < range.Item2 ; y++) {
                    for (int x = 0; x < size.Width; x++)
                    {
                        float val = view.pixelAt(x, y);
                        int intVal = (int)(255 * val);
                        data[y * size.Width + x] = 0 | intVal << 16 | intVal << 8 | intVal;
                    }
                }
            });

            buf.CopyFromMemory<int>(data, size.Width*sizeof(int));

            context.BeginDraw();
            context.DrawBitmap(buf, new RectangleF(0,0,drawingSize.Width, drawingSize.Height), 1.0f, BitmapInterpolationMode.Linear);
            context.EndDraw();

            view.stepTime();
        }
    }
}
