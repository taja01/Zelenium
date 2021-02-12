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
            this.buttonPage.ButtonOverview.ExecuteScript(BaseQueries.GetInnerHtml, out var goodObject);
            Assert.NotNull(goodObject);

            this.buttonPage.ButtonOverview.Basic.ExecuteScript<ButtonType>(BaseQueries.GetInnerHtml, out var myEnum);
            Assert.AreEqual(ButtonType.Basic, myEnum);
        }

        enum ButtonType
        {
            Basic
        }
    }
}
