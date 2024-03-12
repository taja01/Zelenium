using MaterialAngular.PageObjects;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Zelenium.Core.Config;

namespace Zelenium.IntegrationTests.WebElementTests
{
    [TestFixture]
    class JavaScriptTests : BaseTest
    {
        private ButtonPage buttonPage;
        private ILogger<ButtonPage> logger;

        [OneTimeSetUp]
        public void Setup()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
                .CreateLogger();

            var loggerFactory = new LoggerFactory().AddSerilog();
            logger = loggerFactory.CreateLogger<ButtonPage>();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Log.CloseAndFlush();
        }

        [SetUp]
        public void SetUp()
        {
            this.buttonPage = new ButtonPage(this.logger, this.driver);
            this.buttonPage.Load();
            Assert.That(this.buttonPage.IsLoaded().Passed, Is.True);
        }

        [Test]
        public void GetComputedStyleTest()
        {
            var basicButton = this.buttonPage.ButtonOverview.Basic;
            Assert.That(basicButton.GetComputedStyle("writing-mode"), Does.Contain("horizontal-tb"));
        }

        [Test]
        public void GetInnerHtmlTest()
        {
            var goodObject = this.buttonPage.ButtonOverview.ExecuteScript<string>(BaseQueries.GetInnerHtml);

            Assert.That(goodObject, Does.Not.Null.And.Contain("div"));
        }

        [Test]
        public void GetInnerTextTest()
        {
            var myEnum = this.buttonPage.ButtonOverview.Basic.ExecuteScript<ButtonType>(BaseQueries.GetInnerText);
            Assert.That(myEnum, Is.EqualTo(ButtonType.Basic));
        }

        [Test]
        public void SetStyleTest()
        {
            var style = "visibility:hidden";
            var basicButton = this.buttonPage.ButtonOverview.Basic;
            basicButton.ExecuteScript(BaseQueries.SetStyle(style));

            Assert.That(basicButton.ExecuteScript<string>(BaseQueries.GetStyle()), Is.EqualTo(style));
            Assert.That(basicButton.Present, Is.True);
            Assert.That(basicButton.DisplayedNow, Is.False);
        }

        [Test]
        public void SetAttributeTest()
        {
            var attr = "id";
            var value = "goodId";
            var basicButton = this.buttonPage.ButtonOverview.Basic;
            basicButton.ExecuteScript(BaseQueries.AddAttribute(attr, value));

            Assert.That(basicButton.ExecuteScript<string>(BaseQueries.GetAttribute(attr)), Is.EqualTo(value));
        }

        enum ButtonType
        {
            None,
            Basic
        }
    }
}
