using MaterialAngular.PageObjects;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Serilog;
using Zelenium.Core.Utils;

namespace Zelenium.Core.IntegrationTests.WebElementTests
{
    [TestFixture]
    public class ButtonColorTest : BaseTest
    {
        private ButtonPage buttonPage;
        private ILogger<ButtonColorTest> logger;

        [OneTimeSetUp]
        public void TestSetup()
        {
            this.loggerFactory = new LoggerFactory().AddSerilog();
            this.logger = this.loggerFactory.CreateLogger<ButtonColorTest>();
        }


        [SetUp]
        public void SetUp()
        {
            this.buttonPage = new ButtonPage(this.logger, this.driver);
            this.buttonPage.Load();
            Assertion.IsTrue(this.buttonPage.IsLoaded());
        }

        [Test]
        public void BasicContrastTest()
        {
            var basicButton = this.buttonPage.ButtonOverview.Basic;
            var calculatedContrast = ColorUtil.GetReadability(basicButton.Color, basicButton.BackgroundColor);
            Assert.That(calculatedContrast, Is.EqualTo(6.28).Within(0.01));
        }

        [Test]
        public void PrimaryContrastTest()
        {
            var primaryButton = this.buttonPage.ButtonOverview.Primary;
            var calculatedContrast = ColorUtil.GetReadability(primaryButton.Color, primaryButton.BackgroundColor);
            Assert.That(calculatedContrast, Is.EqualTo(6.28).Within(0.01));
        }

        [Test]
        public void AccentContrastTest()
        {
            var accentButton = this.buttonPage.ButtonOverview.Accentc;
            var calculatedContrast = ColorUtil.GetReadability(accentButton.Color, accentButton.BackgroundColor);
            Assert.That(calculatedContrast, Is.EqualTo(6.26).Within(0.01));
        }

        [Test]
        public void WarnContrastTest()
        {
            var warnButton = this.buttonPage.ButtonOverview.Warn;
            var calculatedContrast = ColorUtil.GetReadability(warnButton.Color, warnButton.BackgroundColor);
            Assert.That(calculatedContrast, Is.EqualTo(6.28).Within(0.01));
        }

        [Test]
        public void DisabledContrastTest()
        {
            var disabledButton = this.buttonPage.ButtonOverview.Disabled;
            var calculatedContrast = ColorUtil.GetReadability(disabledButton.Color, disabledButton.BackgroundColor);
            Assert.That(calculatedContrast, Is.EqualTo(2.33).Within(0.01));
        }

        [Test]
        public void LinkContrastTest()
        {
            var linkButton = this.buttonPage.ButtonOverview.Link;
            var calculatedContrast = ColorUtil.GetReadability(linkButton.Color, linkButton.BackgroundColor);
            Assert.That(calculatedContrast, Is.EqualTo(6.28).Within(0.01));
        }
    }
}
