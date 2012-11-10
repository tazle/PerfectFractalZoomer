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

    public interface Trajectory
    {
        float getRCenter();
        float getICenter();
        float getWidth();
        void stepTime();
    }

    public class StaticTrajectory : Trajectory
    {
        private float rCenter;
        private float iCenter;
        private float width;
        
        public StaticTrajectory(float rCenter, float iCenter, float width) {
            this.rCenter = rCenter;
            this.iCenter = iCenter;
            this.width = width;
        }

        public float getRCenter() { return rCenter; }
        public float getICenter() { return iCenter; }
        public float getWidth() { return width; }
        public void stepTime() { }
    }

    public class TrajectoryMandelbrotView : MandelbrotView
    {
        private readonly Mandelbrot engine;
        private readonly Trajectory trajectory;
        private readonly int screenWidth;
        private readonly int screenHeight;

        // derivative values
        private float viewLeft;
        private float viewTop;
        private float rStep;
        private float iStep;

        public TrajectoryMandelbrotView(Mandelbrot engine, Trajectory trajectory, int screenWidth, int screenHeight)
        {
            this.engine = engine;
            this.trajectory = trajectory;
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;

            updateState();

        }

        private void updateState() {
            this.rStep = trajectory.getWidth() / screenWidth;
            this.iStep = -rStep;

            this.viewLeft = trajectory.getRCenter() - trajectory.getWidth() / 2;
            this.viewTop = trajectory.getICenter() - (iStep * screenHeight / 2);
        }

        public float pixelAt(int x, int y)
        {
            return engine.valueAt(viewLeft + x * rStep, viewTop + y * iStep);
        }

        public void stepTime()
        {
            trajectory.stepTime();
            this.updateState();
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
