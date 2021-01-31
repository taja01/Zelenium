using System.Diagnostics;
using MaterialAngular.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using ZeleniumFramework.Config;
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

        [Test]
        public void ElementNotExistColorTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Color,
                Throws.InstanceOf<MissingElementException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistAttributeTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Attributes.Get("test-id"),
                Throws.InstanceOf<MissingElementException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistClassTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Class.Has(".warning"),
                Throws.InstanceOf<MissingElementException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistBackgroundColoraTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.BackgroundColor,
                Throws.InstanceOf<MissingElementException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistClickTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Click(),
                Throws.InstanceOf<ElementNotVisibleException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistJavaScriptClickTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Click(ZeleniumFramework.Enums.ClickMethod.Javascript),
                Throws.InstanceOf<ElementNotVisibleException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistOpenNewTabTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Click(ZeleniumFramework.Enums.ClickMethod.NewTab),
                Throws.InstanceOf<ElementNotVisibleException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistTextTest()
        {
            var s = string.Empty;
            var sw = new Stopwatch();
            sw.Start();
            var exception = Assert.Throws<MissingElementException>(() => _ = this.buttonPage.ButtonOverview.NotExist.Text);
            sw.Stop();

            Assert.AreEqual(exception.GetType(), typeof(MissingElementException));
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistScriptTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.ExecuteScript(BaseQueries.GetInnerHtml),
                Throws.InstanceOf<MissingElementException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistScriptReturnTypedTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.ExecuteScript<bool>(BaseQueries.GetInnerHtml, out _),
                Throws.InstanceOf<MissingElementException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistScriptReturnTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.ExecuteScript(BaseQueries.GetInnerHtml, out _),
                Throws.InstanceOf<MissingElementException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistDragAndDropTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.DragAndDrop(100, 100),
                Throws.InstanceOf<MissingElementException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistSwipeTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Swipe(100),
                Throws.InstanceOf<MissingElementException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistWaitForTextTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.WaitForText("text"),
                Throws.InstanceOf<MissingElementException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistHasTextWithinTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.HasTextWithin("text"),
                Throws.InstanceOf<MissingElementException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }
    }
}
