using MaterialAngular.PageObjects;
using NUnit.Framework;
using Zelenium.Core.Config;
using Zelenium.UnitTests.WebElementTests;

namespace Zelenium.UnitTestss.WebElementTests
{
    [TestFixture]
    class JavaScriptTests : BaseTest
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
        public void GetComputedStyleTest()
        {
            var basicButton = this.buttonPage.ButtonOverview.Basic;
            Assert.AreEqual("horizontal-tb", basicButton.GetComputedStyle("writing-mode"));
        }

        [Test]
        public void GetInnerHtmlTest()
        {
            var goodObject = this.buttonPage.ButtonOverview.ExecuteScript<object>(BaseQueries.GetInnerHtml);
            Assert.NotNull(goodObject);

            var myEnum = this.buttonPage.ButtonOverview.Basic.ExecuteScript<ButtonType>(BaseQueries.GetInnerHtml);
            Assert.AreEqual(ButtonType.Basic, myEnum);
        }

        [Test]
        public void SetStyleTest()
        {
            var style = "visibility:hidden";
            var basicButton = this.buttonPage.ButtonOverview.Basic;
            basicButton.ExecuteScript(BaseQueries.SetStyle(style));

            Assert.AreEqual(style, basicButton.ExecuteScript<string>(BaseQueries.GetStyle()));
            Assert.IsTrue(basicButton.Present);
            Assert.IsFalse(basicButton.DisplayedNow);
        }

        [Test]
        public void SetAttributeTest()
        {
            var attr = "id";
            var value = "goodId";
            var basicButton = this.buttonPage.ButtonOverview.Basic;
            basicButton.ExecuteScript(BaseQueries.AddAttribute(attr, value));

            Assert.AreEqual(value, basicButton.ExecuteScript<string>(BaseQueries.GetAttribute(attr)));
        }

        enum ButtonType
        {
            Basic
        }
    }
}
