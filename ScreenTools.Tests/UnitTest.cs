using Xunit;

namespace ScreenTools.Capture.Tests
{
    public class UnitTest
    {
        const string filename = @"c:\users\22791\desktop\unit_test.png";

        [Fact]
        public void GetScreenImage()
        {
            var img = CaptureFacade.GetScreenImage();
            Assert.NotNull(img);
        }

        [Fact]
        public void GetWindowImage()
        {
            var img = CaptureFacade.GetActiveWindowImage();
            Assert.NotNull(img);
        }

        [Fact]
        public void SaveImageToFile()
        {
            System.Drawing.Image img = null;
            CaptureFacade.SaveImageToFile(img, filename);
            Assert.True(System.IO.File.Exists(filename));
        }
    }
}
