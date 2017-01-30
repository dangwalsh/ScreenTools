using Xunit;

namespace ScreenTools.Tests
{
    public class UnitTest
    {
        const string screenfile = @"c:\users\22791\desktop\screen_test.png";
        const string windowfile = @"c:\users\22791\desktop\window_test.png";

        [Fact]
        public void GetScreenImage()
        {
            Capture.ScreenToFile(screenfile);
            bool exists = System.IO.File.Exists(screenfile);
            Assert.True(exists);
        }

        [Fact]
        public void GetWindowImage()
        {
            Capture.ActiveWindowToFile(windowfile);
            bool exists = System.IO.File.Exists(windowfile);
            Assert.True(exists);
        }
    }
}
