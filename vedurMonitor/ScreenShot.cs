﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VedurClassLibrary
{
    class ScreenShot1
    {
        public Rectangle Bounds { get; private set; }

        
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        public static Image CaptureDesktop()
        {
            return CaptureWindow(GetDesktopWindow());
        }

        public static Bitmap CaptureActiveWindow()
        {
            return CaptureWindow(GetForegroundWindow());
        }

        public static Bitmap CaptureWindow(IntPtr handle)
        {
            var rect = new Rect();
            GetWindowRect(handle, ref rect);
            var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            }

            return result;
        }
        public void SS()
        {
            try
            {
                string mynd = Interaction.InputBox("Nafn myndar", "Title", "Jpg");
                Thread.Sleep(1000);

                var image = ScreenShot1.CaptureActiveWindow();

                image.Save(@"C:\Users\kolli\Desktop\vedurMonitor\vedurMonitor\Screenshots\" + mynd + ".jpg", ImageFormat.Jpeg);
                System.Windows.MessageBox.Show("Mynd vistuð");
            }

            catch(Exception)
            {
                System.Windows.MessageBox.Show("KerfisVilla Reyndu aftur");
            }

        }
    }
}
