using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Exercise.WindowPosition
{
    public static class WindowPositionManager
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
        internal static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll")]
        internal static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);
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
