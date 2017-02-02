using Xunit;

namespace ScreenTools.Capture.Tests
{
    public class UnitTest
    {
        const string filename = @"c:\users\22791\desktop\unit_test.gif";

        [Fact]
        public void GetScreenImage()
        {
            var img = CaptureFacade.CaptureScreen();
            Assert.NotNull(img);
        }

        [Fact]
        public void GetWindowImage()
        {
            var img = CaptureFacade.CaptureActiveWindow();
            Assert.NotNull(img);
        }

        [Fact]
        public void TestAnimation()
        {
            var img = CaptureFacade.CaptureAnimation(5);
            Assert.NotNull(img);
        }

        [Fact]
        public void SaveImageToFile()
        {
            var img = CaptureFacade.CaptureAnimation(5, 5);
            CaptureFacade.SaveCapture(img, filename);
            Assert.True(System.IO.File.Exists(filename));
        }
    }
}
