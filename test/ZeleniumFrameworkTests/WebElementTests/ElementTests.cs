using System.Diagnostics;
using MaterialAngular.PageObjects;
using NUnit.Framework;
using ZeleniumFramework.Exceptions;
using ZeleniumFrameworkTest.WebElementTests;

namespace ZeleniumFrameworkTests.WebElementTests
{
    [TestFixture]
    public class ElementTests : BaseTest
    {
        private ButtonPage buttonPage;

        [SetUp]
        public void SetUp()
        {
            this.buttonPage = new ButtonPage(this.driver);
            this.buttonPage.Load();
            Assert.IsTrue(this.buttonPage.IsLoaded().Passed);
        }

        [Test]
        public void ElementNotExistDisplayTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            var a = this.buttonPage.ButtonOverview.NotExist.Displayed;
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistScrollTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Scroll(),
                Throws.InstanceOf<MissingElementException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }
    }
}
