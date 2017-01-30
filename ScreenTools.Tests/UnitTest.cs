using Xunit;

namespace ScreenTools.Tests
{
    public class UnitTest
    {
        const string filename = @"c:\users\22791\desktop\unit_test.png";

        [Fact]
        public void GetScreenImage()
        {
            var img = Capture.GetScreenImage();
            Assert.NotNull(img);
        }

        [Fact]
        public void GetWindowImage()
        {
            var img = Capture.GetActiveWindowImage();
            Assert.NotNull(img);
        }

        [Fact]
        public void SaveImageToFile()
        {
            System.Drawing.Image img = null;
            Capture.SaveImageToFile(img, filename);
            Assert.True(System.IO.File.Exists(filename));
        }
    }
}
