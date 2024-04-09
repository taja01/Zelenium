using System.Drawing;
using NUnit.Framework;
using Zelenium.Core.Utils;

namespace Zelenium.Core.UnitTests.CoreTests
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
            Assert.That(ColorUtil.GetReadability(Color.White, Color.Black), Is.EqualTo(21));
            Assert.That(ColorUtil.GetReadability("rgb(128,128,128)", "rgb(255,255,255)"), Is.EqualTo(3.95));
            Assert.That(ColorUtil.GetReadability("rgb(136,183,90)", "rgb(0,0,0)"), Is.EqualTo(8.97));
        }
    }
}
