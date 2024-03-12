using MaterialAngular.PageObjects;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Zelenium.Core.Config;
using Zelenium.Core.Exceptions;
using Zelenium.Core.WebDriver.Types;

namespace Zelenium.IntegrationTests.WebElementTests
{
    [TestFixture]
    public class AttributeTests : BaseTest
    {
        private ButtonPage buttonPage;
        private Element basicButton;
        private const string ATTIRBUTE_NAME = "attributeTestName";
        private const string ATTRIBUTE_VALUE = "attributeTestValue";
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

            this.basicButton = this.buttonPage.ButtonOverview.BasicWithoutDelay;
        }

        [Test]
        public void GetAttributeNotExistTest()
        {
            Assert.That(this.basicButton.Attributes.Get("notexistAttribute"), Is.Null);
        }

        [Test]
        public void GetAttributeTest()
        {
            this.basicButton.ExecuteScript(BaseQueries.AddAttribute(ATTIRBUTE_NAME, ATTRIBUTE_VALUE));
            Assert.That(this.basicButton.Attributes.Get(ATTIRBUTE_NAME), Is.EqualTo(ATTRIBUTE_VALUE));
        }

        [Test]
        public void HasAttributeNotExistTest()
        {
            Assert.That(this.basicButton.Attributes.Has("notexistAttribute"), Is.False);
        }

        [Test]
        public void HasAttributeTest()
        {
            Assert.That(this.basicButton.Attributes.Has(ATTIRBUTE_NAME), Is.False);
            this.basicButton.ExecuteScript(BaseQueries.AddAttribute(ATTIRBUTE_NAME, ATTRIBUTE_VALUE));
            Assert.That(this.basicButton.Attributes.HasWithin(ATTIRBUTE_NAME), Is.True);
        }

        [Test]
        public void WaitForAttributeNotExistTest()
        {
            Assert.Throws<AttributeNotExistException>(() => this.basicButton.Attributes.WaitFor("notexistAttribute"));
        }

        [Test]
        public void WaitForAttributeTest()
        {
            Assert.Throws<AttributeNotExistException>(() => this.basicButton.Attributes.WaitFor(ATTIRBUTE_NAME));
            this.basicButton.ExecuteScript(BaseQueries.AddAttribute(ATTIRBUTE_NAME, ATTRIBUTE_VALUE));
            this.basicButton.Attributes.WaitFor(ATTIRBUTE_NAME);
        }

        [Test]
        public void HasWithinAttributeNotExistTest()
        {
            Assert.That(this.basicButton.Attributes.HasWithin("notexistAttribute"), Is.False);
        }

        [Test]
        public void HasWithinAttributeTest()
        {
            Assert.That(this.basicButton.Attributes.HasWithin(ATTRIBUTE_VALUE), Is.False);
            this.basicButton.ExecuteScript(BaseQueries.AddAttribute(ATTIRBUTE_NAME, ATTRIBUTE_VALUE));
            this.basicButton.Attributes.HasWithin(ATTIRBUTE_NAME);
        }
    }
}
