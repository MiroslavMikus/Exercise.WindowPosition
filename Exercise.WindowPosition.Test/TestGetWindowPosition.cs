using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercise.WindowPosition.Test
{
    [TestClass]
    public class TestGetWindowPosition
    {
        [TestMethod]
        public void GetPositionTest()
        {
            var pos = WindowPositionManager.GetWindowPosition(15692);
            Console.WriteLine(pos.ToString());
        }

        [TestMethod]
        public void SetPositionTest()
        {
            WindowPositionManager.MoveWindow(15692, new Rect
            {
                Top = 10,
                Left= 10,
                Bottom = 500,
                Right = 500
            });
        }

        [TestMethod]
        public void GetProcesses()
        {
            var allProcesses = Process.GetProcesses();
        }
    }
}
