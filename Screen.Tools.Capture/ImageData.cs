using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Tools
{
    /// <summary>
    /// This class processes raw image data into image files
    /// </summary>
    public class ImageData
    {
        /// <summary>
        /// Returns a collection of images in a folder
        /// </summary>
        /// <param name="foldername"></param>
        /// <returns></returns>
        public static object Load(string foldername)
        {
            var files = GetFileInfo(foldername);
            var images = LoadImagesFromFiles(files, files.Count());
            return images;
        }

        /// <summary>
        /// Saves an image to a file of a specified type
        /// </summary>
        /// <param name="data">The image to be saved</param>
        /// <param name="filename">The output file path (must contain a valid extension)</param>
        /// <search>capture, window, image</search>
        public static void Save(object[] data, string filename)
        {
            var format = Capture.GetImageFormat(filename);
            SaveImage(data, filename, format);
        }

        /// <summary>
        /// Encapsulates the default constructor in the assembly
        /// </summary>
        internal ImageData()
        {

        }

        /// <summary>
        /// Given a collection of files this attempts to load each one as an image
        /// </summary>
        /// <param name="files"></param>
        /// <param name="count">Number of files in the collection</param>
        /// <returns>Image array</returns>
        internal static Image[] LoadImagesFromFiles(FileInfo[] files, int count)
        {
            var images = new Image[count];
            for (int index = 0; index < count; ++index)
            {
                var image = LoadImage(files[index].FullName);
                if (image == null) continue;
                images[index] = image;
            }
            return images;
        }

        /// <summary>
        /// Gets a collection of FileInfo objects in the specified directory
        /// </summary>
        /// <param name="foldername">the path to the folder</param>
        /// <returns>FileInfo array</returns>
        internal static FileInfo[] GetFileInfo(string foldername)
        {
            var folder = new DirectoryInfo(foldername);
            var files = folder.GetFiles();
            return files;
        }

        /// <summary>
        /// Loads an image from a path name
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        internal static Image LoadImage(string filename)
        {
            var image = Image.FromFile(filename);
            return image;
        }

        /// <summary>
        /// Saves an image to the filesystem.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        internal static void SaveImage(object[] data, string filename, ImageFormat format)
        {
            switch (data.Count())
            {
                case 0:
                    break;
                case 1:
                    var image = data[0] as Image;
                    if (image != null)
                        image.Save(filename, format);
                    break;
                default:
                    var images = data.Cast<Image>().ToArray();
                    if (images != null)
                        SaveGif(filename, CreateGifEncoder(images, filename));
                    break;
            }
        }

        /// <summary>
        /// Converts an array of images to .gif and saves them to the filesystem.
        /// </summary>
        /// <param name="images"></param>
        /// <param name="filename"></param>
        internal static GifBitmapEncoder CreateGifEncoder(IEnumerable<Image> images, string filename)
        {
            var gifEnconder = new GifBitmapEncoder();
            foreach (Bitmap bitmap in images)
            {
                var source = Imaging.CreateBitmapSourceFromHBitmap(
                    bitmap.GetHbitmap(),
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
                for (int i = 0; i < 3; ++i)
                    gifEnconder.Frames.Add(BitmapFrame.Create(source));
            }
            return gifEnconder;
        }

        /// <summary>
        /// Saves a Gif image to the file system
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="gifEnconder"></param>
        internal static void SaveGif(string filename, GifBitmapEncoder gifEnconder)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Create))
            {
                gifEnconder.Save(stream);
            }
        }
    }
}
