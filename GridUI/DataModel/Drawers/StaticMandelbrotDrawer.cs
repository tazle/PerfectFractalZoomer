using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

using System.Collections.Concurrent;


// Colors
using SharpDX.Direct2D1;
using CommonDX;
using SharpDX;
using SharpDX.DXGI;

using PerfectFractalZoomer.Fractal.MandelbrotLib;

namespace GridUI.DataModel.Drawers
{
    class StaticMandelbrotDrawer : CommonMandelbrotDrawer
    {
        public StaticMandelbrotDrawer(String uniqueId, String title, String imagePath, Color color, float rCenter, float iCenter, float width, int iters, DataGroup group)
            : base(uniqueId, title, imagePath, color, new StaticTrajectory(rCenter, iCenter, width), iters, group)
        {
        }
    }
}
