using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;


namespace Tools
{
    /// <summary>
    /// This class contains methods to capture screen images
    /// </summary>
    public class Capture
    {
        /// <summary>
        /// Captures an image of the active window
        /// </summary>
        /// <search>capture, window, image</search>
        /// <returns>The capture image</returns>
        public static object Window()
        {
            var handle = GetWindowHandle();
            var image = DoCaptureWindow(handle);
            return image;
        }

        /// <summary>
        /// Captures an image of the main screen
        /// </summary>
        /// <search>capture, screen, image</search>
        /// <returns>The capture image</returns>
        public static object Screen()
        {
            var image = DoCaptureScreen();
            return image;
        }

        /// <summary>
        /// Captures a series of images of the screen over a specified time
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="fps"></param>
        /// <returns></returns>
        internal static object Timed(int seconds, int fps = 5)
        {
            var frames = seconds * fps;
            var images = new Image[frames];
            for (int index = 0; index < frames; ++index)
            {
                images[index] = DoCaptureScreen();
                Thread.Sleep(1000 / fps);
            }
            return images;
        }

        /// <summary>
        /// Encapsulates the default constructor in the assembly
        /// </summary>
        internal Capture()
        {

        }


        /// <summary>
        /// Creates an Image object containing a screen shot of the entire desktop
        /// </summary>
        /// <returns></returns>
        internal static Image DoCaptureScreen()
        {
            return DoCaptureWindow(User32.GetDesktopWindow());
        }

        /// <summary>
        /// Exposes the User32 window method to the assembly
        /// </summary>
        /// <returns></returns>
        internal static IntPtr GetWindowHandle()
        {
            return User32.GetForegroundWindow();
        }

        /// <summary>
        /// Creates an Image object containing a screen shot of a specific window
        /// </summary>
        /// <param name="handle">The handle to the window. (In windows forms, this is obtained by the Handle property)</param>
        /// <returns></returns>
        internal static Image DoCaptureWindow(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            // restore selection
            GDI32.SelectObject(hdcDest, hOld);
            // clean up 
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image image = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);

            return image;
        }

        /// <summary>
        /// Determines the image format from the desired file extension
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        internal static ImageFormat GetImageFormat(string filename)
        {
            ImageFormat format;
            string extension = Path.GetExtension(filename);
            switch (extension)
            {
                case ".gif":
                    format = ImageFormat.Gif;
                    break;
                case ".jpg":
                    format = ImageFormat.Jpeg;
                    break;
                case ".jpeg":
                    format = ImageFormat.Jpeg;
                    break;
                case ".png":
                    format = ImageFormat.Png;
                    break;
                case ".tiff":
                    format = ImageFormat.Tiff;
                    break;
                case ".bmp":
                    format = ImageFormat.Bmp;
                    break;
                default:
                    throw new ArgumentException("File extension is not related to a valid image format!");
            }
            return format;
        }

        /// <summary>
        /// Helper class containing Gdi32 API functions
        /// </summary>
        private class GDI32
        {
            public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter

            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
                int nWidth, int nHeight, IntPtr hObjectSource,
                int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
                int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }

        /// <summary>
        /// Helper class containing User32 API functions
        /// </summary>
        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
            [DllImport("user32.dll")]
            public static extern IntPtr GetForegroundWindow();
        }
    }
}
