using NUnit.Framework;
using Zelenium.Core.Utils;

namespace Zelenium.Core.IntegrationTests.WebElementTests
{
    [TestFixture]
    public class ContrastTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            this.mainPage.ContrastsSection.Click();
        }

        [Test]
        public void ReadGoodTest()
        {
            Assertion.IsReadable(this.mainPage.ContrastsSection.TextWithGoodContrast, "TextWithGoodContrast", Config.ContrastConfig.NORMAL_AA);
        }

        [Test]
        public void ReadBadTest()
        {
            var expectedMessage = "TextWithBadContrast Contrast ratio (2.48) of the given color pair is below the limit (4.5) Color: RGBA(0,0,0,255), #000000FF Background color: RGBA(0,0,0,178), #000000B2";
            Assert.That(() => Assertion.IsReadable(this.mainPage.ContrastsSection.TextWithBadContrast, "TextWithBadContrast", Config.ContrastConfig.NORMAL_AA), Throws.TypeOf<AssertionException>().With.Message.Contain(expectedMessage));
        }
    }
}
