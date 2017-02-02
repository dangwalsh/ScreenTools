using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ScreenTools.Capture
{
    /// <summary>
    /// Exposes the public methods of the assembly
    /// </summary>
    public class CaptureFacade
    {
        public static object CaptureAnimation(int seconds, int framerate=15)
        {
            int frames = seconds * framerate;
            Image[] images = new Image[frames];
            for (int i = 0; i < frames; ++i)
            {
                images[i] = CaptureLogic.CaptureScreen();
                Thread.Sleep(1000 / framerate);
            }
            return images;
        }

        /// <summary>
        /// Captures an image of the active window
        /// </summary>
        /// <search>capture, window, image</search>
        /// <returns>The capture image</returns>
        public static object CaptureActiveWindow()
        {
            IntPtr handle = CaptureLogic.GetWindowHandle();
            Image image = CaptureLogic.CaptureWindow(handle);
            return image;
        }

        /// <summary>
        /// Captures an image of the main screen
        /// </summary>
        /// <search>capture, screen, image</search>
        /// <returns>The capture image</returns>
        public static object CaptureScreen()
        {
            Image image = CaptureLogic.CaptureScreen();
            return image;
        }

        /// <summary>
        /// Saves an image to a file of a specified type
        /// </summary>
        /// <param name="data">The image to be saved</param>
        /// <param name="filename">The output file path (must contain a valid extension)</param>
        /// <search>capture, window, image</search>
        public static void SaveCapture(object data, string filename)
        {
            ImageFormat format = CaptureLogic.GetImageFormat(filename);

            Image image = data as Image; // TODO: verify that this returns null if it's an array.
            if (image == null)
            {
                var images = data as Image[];
                if (images == null)
                {
                    throw new Exception("File is not a valid animation");
                }

                var gifEnconder = new GifBitmapEncoder();
                foreach (Bitmap bitmap in images)
                {
                    var source = Imaging.CreateBitmapSourceFromHBitmap(
                        bitmap.GetHbitmap(), 
                        IntPtr.Zero, 
                        Int32Rect.Empty, 
                        BitmapSizeOptions.FromEmptyOptions());
                    gifEnconder.Frames.Add(BitmapFrame.Create(source));
                }

                using (FileStream stream = new FileStream(filename, FileMode.Create))
                {
                    gifEnconder.Save(stream);
                }

                return;
            }
            image.Save(filename, format);
        }

        /// <summary>
        /// Encapsulates the default constructor in the assembly
        /// </summary>
        internal CaptureFacade()
        {

        }
    }
}
