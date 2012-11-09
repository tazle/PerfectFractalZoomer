using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectFractalZoomer.Fractal.MandelbrotLib
{
    public interface MandelbrotView
    {
        float pixelAt(int x, int y);
        void stepTime();
    }

    public class StaticMandelbrotView : MandelbrotView
    {
        private readonly Mandelbrot engine;
        private readonly float rCenter;
        private readonly float iCenter;
        private readonly float viewWidth;
        private readonly int screenWidth;
        private readonly int screenHeight;

        // derivative values
        private readonly float viewLeft;
        private readonly float viewTop;
        private readonly float rStep;
        private readonly float iStep;

        public StaticMandelbrotView(Mandelbrot engine, float rCenter, float iCenter, float viewWidth, int screenWidth, int screenHeight)
        {
            this.engine = engine;
            this.rCenter = rCenter;
            this.iCenter = iCenter;
            this.viewWidth = viewWidth;
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;


            this.rStep = viewWidth / screenWidth;
            this.iStep = -rStep;

            this.viewLeft = rCenter - viewWidth/2;
            this.viewTop = iCenter - (iStep * screenHeight / 2);
        }

        public float pixelAt(int x, int y)
        {
            return engine.valueAt(viewLeft + x * rStep, viewTop + y * iStep);
        }

        public void stepTime()
        {
            // Do nothing
        }
    }

    public interface Mandelbrot
    {
        /**
         * Calculate mandelbrot fractal value at Mandelbrot space point (real, imag). The value returnes is in range [0,1] where 0 means definitely not part of Mandelbrot set and 1 means that the value had not diverged until end of iteration was reached.
         */
        float valueAt(float real, float imag);
    }
    public class BasicMandelbrot : Mandelbrot
    {
        private readonly int maxIters;
        public BasicMandelbrot(int maxIters)
        {
            this.maxIters = maxIters;
        }

        public float valueAt(float r_0, float i_0)
        {
            float r = 0;
            float i = 0;
            for (int n = 0; n < maxIters; n++)
            {
                float r_temp = r * r - i * i + r_0;
                i = 2 * r * i + i_0;
                r = r_temp;
                if (r * r + i * i > 2 * 2)
                {
                    return (float)n / maxIters;
                }
            }
            return 1;
        }
    }
}
