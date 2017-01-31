using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ScreenTools.Capture
{
    public class CaptureFacade
    {
        /// <summary>
        /// Captures an image of the active window
        /// </summary>
        /// <search>capture, window, image</search>
        /// <returns>The capture image</returns>
        public static Image GetActiveWindowImage()
        {
            IntPtr handle = CaptureClass.GetWindowHandle();
            Image img = CaptureClass.CaptureWindow(handle);
            return img;
        }

        /// <summary>
        /// Captures an image of the main screen
        /// </summary>
        /// <search>capture, screen, image</search>
        /// <returns>The capture image</returns>
        public static Image GetScreenImage()
        {
            Image img = CaptureClass.CaptureScreen();
            return img;
        }

        /// <summary>
        /// Saves an image to a file of a specified type
        /// </summary>
        /// <param name="img">The image to be saved</param>
        /// <param name="filename">The output file path (must contain a valid extension)</param>
        /// <search>capture, window, image</search>
        public static void SaveImageToFile(Image img, string filename)
        {
            ImageFormat format = CaptureClass.GetImageFormat(filename);
            img.Save(filename, format);
        }

        /// <summary>
        /// Encapsulates the default constructor in the assembly
        /// </summary>
        internal CaptureFacade()
        {

        }
    }
}
