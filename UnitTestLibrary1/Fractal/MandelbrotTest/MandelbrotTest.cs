using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using PerfectFractalZoomer.Fractal.MandelbrotLib;

namespace PerfectFractalZoomer.Fractal.MandelbrotTest
{
    [TestClass]
    public class StaticMandelbrotViewTest
    {
        [TestMethod]
        public void TestSimple()
        {
            Mandelbrot m = new BasicMandelbrot(64);
            MandelbrotView view = new TrajectoryMandelbrotView(m, new StaticTrajectory(0, 0, 4), 640, 480);
            Assert.AreEqual(1.0f, view.pixelAt(320, 240));
            Assert.AreEqual(0.0f, view.pixelAt(0, 0));
            Assert.AreEqual(0.0f, view.pixelAt(640, 480));
        }
    }
    [TestClass]
    public class BasicMandelbrotTest
    {
        [TestMethod]
        public void TestValueInsideSet()
        {
            Mandelbrot m = new BasicMandelbrot(64);
            Assert.AreEqual(1.0f, m.valueAt(0, 0));
        }

        [TestMethod]
        public void TestValueOutsideSet()
        {
            Mandelbrot m = new BasicMandelbrot(64);
            Assert.AreEqual(0.0f, m.valueAt(3, 0));
        }
    }
}
