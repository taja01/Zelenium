using System.Linq;
using MaterialAngular.PageObjects;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Serilog;

namespace Zelenium.Core.IntegrationTests.WebElementTests
{
    internal class SelectElementTests : BaseTest
    {
        private SelectPage page;
        private ILogger<SelectElementTests> logger;


        [OneTimeSetUp]
        public void TestSetup()
        {
            this.loggerFactory = new LoggerFactory().AddSerilog();
            this.logger = this.loggerFactory.CreateLogger<SelectElementTests>();
        }

        [SetUp]
        public void SetUp()
        {
            this.page = new SelectPage(this.logger, this.driver);
            this.page.Load();
            Assert.That(this.page.IsLoaded().Passed, Is.True);
        }

        [Test]
        public void ReadOptionTest()
        {
            var options = this.page.NativeSelect.GetAllOptions();
            Assert.That(options, Does.ContainKey("saab"));
        }

        [Test]
        public void SetByIndex()
        {
            var options = this.page.NativeSelect.GetAllOptions();

            this.page.NativeSelect.SetByIndex(3);

            Assert.That(options.ElementAt(3).Value, Is.EqualTo(this.page.NativeSelect.SelectedText));
        }

        [Test]
        public void SetByValue()
        {
            var options = this.page.NativeSelect.GetAllOptions();

            this.page.NativeSelect.SetByValue("mercedes");

            Assert.That(options["mercedes"], Is.EqualTo(this.page.NativeSelect.SelectedText));
        }
    }
}
