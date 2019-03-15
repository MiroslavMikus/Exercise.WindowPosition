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

    public class WindowPositionManager
    {
        public static void MoveWindow(int pid, Rect position)
        {
            MoveWindow(GetWindow(pid), position.Top, position.Left, position.Right, position.Bottom, true);
        }

        public static Rect GetWindowPosition(int pid)
        {
            Rect position = new Rect();
            GetWindowRect(GetWindow(pid), ref position);
            return position;
        }

        private static IntPtr GetWindow(int pid)
        {
            Process process = Process.GetProcessById(pid);
            return process.MainWindowHandle;
        }

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

    }
    public struct Rect
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }

        public override string ToString()
        {
            return $"Left:{Left};Top:{Top};Right:{Right};Bottom:{Bottom}";
        }
    }
}
