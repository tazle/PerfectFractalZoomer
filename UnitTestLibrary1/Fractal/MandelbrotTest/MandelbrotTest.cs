using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using PerfectFractalZoomer.Fractal.MandelbrotLib;

namespace PerfectFractalZoomer.Fractal.MandelbrotTest
{
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
