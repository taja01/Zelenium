using System.Drawing;
using NUnit.Framework;
using ZeleniumFramework.Utils;

namespace ZeleniumFrameworkTest.CoreTests
{
    /// <summary>
    /// Values from https://contrastchecker.com/
    /// </summary>
    [TestFixture]
    public class ColorParseTests
    {
        [Test]
        public void Test()
        {
            Assert.AreEqual(21, ColorUtil.GetReadability(Color.White, Color.Black));
            Assert.AreEqual(3.95, ColorUtil.GetReadability("rgb(128,128,128)", "rgb(255,255,255)"));
            Assert.AreEqual(8.97, ColorUtil.GetReadability("rgb(136,183,90)", "rgb(0,0,0)"));
        }
    }
}
