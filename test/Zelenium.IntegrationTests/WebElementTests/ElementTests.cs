using System.Diagnostics;
using MaterialAngular.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using Zelenium.Core.Config;

namespace Zelenium.IntegrationTests.WebElementTests
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
            Assert.That(this.buttonPage.IsLoaded().Passed, Is.True);
        }

        [Test]
        public void ElementNotExistDisplayTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            _ = this.buttonPage.ButtonOverview.NotExist.Displayed;
            sw.Stop();

            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(5, 6));
        }

        [Test]
        public void ElementNotExistScrollTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Scroll(),
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(5, 6));
        }

        [Test]
        public void ElementNotExistColorTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Color,
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(5, 6));
        }

        [Test]
        public void ElementNotExistAttributeTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Attributes.Get("test-id"),
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(5, 6));
        }

        [Test]
        public void ElementNotExistClassTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Class.Has(".warning"),
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(5, 6));
        }

        [Test]
        public void ElementNotExistBackgroundColoraTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.BackgroundColor,
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(5, 6));
        }

        [Test]
        public void ElementNotExistClickTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Click(),
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(5, 6));
        }

        [Test]
        public void ElementNotExistJavaScriptClickTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Click(Zelenium.Core.Enums.ClickMethod.JavaScript),
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(5, 6));
        }

        [Test]
        public void ElementNotExistOpenNewTabTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Click(Zelenium.Core.Enums.ClickMethod.NewTab),
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(5, 6));
        }

        [Test]
        public void ElementNotExistTextTest()
        {
            var s = string.Empty;
            var sw = new Stopwatch();
            sw.Start();
            var exception = Assert.Throws<NoSuchElementException>(() => _ = this.buttonPage.ButtonOverview.NotExist.Text);
            sw.Stop();

            Assert.That(exception, Is.TypeOf<NoSuchElementException>());
            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(5, 6));
        }

        [Test]
        public void ElementNotExistScriptTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.ExecuteScript(BaseQueries.GetInnerHtml),
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(5, 6));
        }

        [Test]
        public void ElementNotExistScriptReturnTypedTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.ExecuteScript<bool>(BaseQueries.GetInnerHtml),
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(5, 6));
        }

        [Test]
        public void ElementNotExistScriptReturnTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.ExecuteScript(BaseQueries.GetInnerHtml),
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(5, 6));
        }

        [Test]
        public void ElementNotExistDragAndDropTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.DragAndDrop(100, 100),
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(5, 6));
        }

        [Test]
        public void ElementNotExistSwipeTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.Swipe(100),
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(5, 6));
        }

        [Test]
        public void ElementNotExistWaitForTextTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.WaitForText("text"),
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(5, 6));
        }

        [Test]
        public void ElementNotExistHasTextWithinTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(() => this.buttonPage.ButtonOverview.NotExist.HasTextWithin("text"),
                Throws.InstanceOf<NoSuchElementException>());
            sw.Stop();
            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(5, 6));
        }
    }
}
