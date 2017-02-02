using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;


namespace ScreenTools.Capture
{
    /// <summary>
    /// Exposes the public methods of the assembly
    /// </summary>
    public class CaptureFacade
    {
        /// <summary>
        /// Captures a series of images of the screen over a specified time
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="fps"></param>
        /// <returns></returns>
        public static object CaptureAnimation(int seconds, int fps=5)
        {
            int frames = seconds * fps;
            Image[] images = new Image[frames];
            for (int i = 0; i < frames; ++i)
            {
                images[i] = CaptureLogic.CaptureScreen();
                Thread.Sleep(1000 / fps);
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
            CaptureLogic.SaveImage(data, filename, format);      
        }

        /// <summary>
        /// Encapsulates the default constructor in the assembly
        /// </summary>
        internal CaptureFacade()
        {

        }
    }
}
