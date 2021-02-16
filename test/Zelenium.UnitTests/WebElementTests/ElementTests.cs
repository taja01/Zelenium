using System.Diagnostics;
using MaterialAngular.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using Zelenium.Core.Config;
using Zelenium.UnitTests.WebElementTests;

namespace Zelenium.UnitTestss.WebElementTests
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
                Throws.InstanceOf<NoSuchElementException>());
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
                Throws.InstanceOf<NoSuchElementException>());
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
                Throws.InstanceOf<NoSuchElementException>());
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
                Throws.InstanceOf<NoSuchElementException>());
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
                Throws.InstanceOf<NoSuchElementException>());
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
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistJavaScriptClickTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Click(Zelenium.Core.Enums.ClickMethod.Javascript),
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistOpenNewTabTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Click(Zelenium.Core.Enums.ClickMethod.NewTab),
                Throws.InstanceOf<NoSuchElementException>());
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
            var exception = Assert.Throws<NoSuchElementException>(() => _ = this.buttonPage.ButtonOverview.NotExist.Text);
            sw.Stop();

            Assert.AreEqual(exception.GetType(), typeof(NoSuchElementException));
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }

        [Test]
        public void ElementNotExistScriptTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.ExecuteScript(BaseQueries.GetInnerHtml),
                Throws.InstanceOf<NoSuchElementException>());
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
                Throws.InstanceOf<NoSuchElementException>());
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
                Throws.InstanceOf<NoSuchElementException>());
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
                Throws.InstanceOf<NoSuchElementException>());
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
                Throws.InstanceOf<NoSuchElementException>());
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
                Throws.InstanceOf<NoSuchElementException>());
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
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.GreaterOrEqual(sw.Elapsed.TotalSeconds, 5);
            Assert.Less(sw.Elapsed.TotalSeconds, 6);
        }
    }
}
