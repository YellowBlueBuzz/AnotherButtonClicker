using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace RarClicker
{
    class Program
    {
        #region API-Declarations
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        public static uint WM_COMMAND = 0x111;

        public enum GWL
        {
            GWL_WNDPROC = (-4),
            GWL_HINSTANCE = (-6),
            GWL_HWNDPARENT = (-8),
            GWL_STYLE = (-16),
            GWL_EXSTYLE = (-20),
            GWL_USERDATA = (-21),
            GWL_ID = (-12)
        }

        public enum ButtonControlMessages : uint
        {
            BM_GETCHECK = 0x00F0,
            BM_SETCHECK = 0x00F1,
            BM_GETSTATE = 0x00F2,
            BM_SETSTATE = 0x00F3,
            BM_SETSTYLE = 0x00F4,
            BM_CLICK = 0x00F5,
            BM_GETIMAGE = 0x00F6,
            BM_SETIMAGE = 0x00F7
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        static IntPtr MakeWParam(int loWord, int hiWord)
        {
            return new IntPtr(loWord + hiWord * 65536);
        }
        #endregion

        public static IntPtr GetButtonHandle()
        {
            IntPtr ButtonHandle = FindWindow("#32770", null);

            IntPtr wndChild = IntPtr.Zero;

            if (ButtonHandle != IntPtr.Zero)
            {
                for (int i = 0; i < 3; i++)
                {
                    wndChild = FindWindowEx(ButtonHandle, wndChild, "Button", null);
                }

                return wndChild;
            }

            return wndChild;
        }

        public static void ClickButton(IntPtr AHandle)
        {
            SendMessage(GetParent(AHandle), WM_COMMAND, MakeWParam(GetWindowLong(AHandle, (int)GWL.GWL_ID), (int)ButtonControlMessages.BM_CLICK), AHandle);
        }

        static void Main(string[] args)
        {
            int count = 0;
            while (true)
            {
                count++;
                ClickButton(GetButtonHandle());

                System.Threading.Thread.Sleep(1500);

                Console.WriteLine("Try to Click Button: " + count.ToString());
            }
        }
    }
}