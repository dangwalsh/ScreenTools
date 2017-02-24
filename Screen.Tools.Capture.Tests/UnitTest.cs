using Xunit;

namespace Tools.Tests
{
    public class UnitTest
    {
        const string filename = @"c:\users\22791\desktop\unit_test.gif";

        [Fact]
        public void GetScreenImage()
        {
            var img = Capture.Screen();
            Assert.NotNull(img);
        }

        [Fact]
        public void GetWindowImage()
        {
            var img = Capture.Window();
            Assert.NotNull(img);
        }

        //[Fact]
        //public void TestAnimation()
        //{
        //    var img = Capture.MainDisplayTimed(5);
        //    Assert.NotNull(img);
        //}

        //[Fact]
        //public void SaveImageToFile()
        //{
        //    var obj = Capture.MainDisplayTimed(5, 5);
        //    object[] objArr = new object[] { obj };
        //    Capture.Save(objArr, filename);
        //    Assert.True(System.IO.File.Exists(filename));
        //}
    }
}
