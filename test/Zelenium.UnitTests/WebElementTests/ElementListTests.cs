using MaterialAngular.PageObjects;
using NUnit.Framework;

namespace Zelenium.UnitTests.WebElementTests
{
    [TestFixture]
    public class ElementListTests : BaseTest
    {
        private SelectPage selectPage;

        [SetUp]
        public void SetUp()
        {
            this.selectPage = new SelectPage(this.driver);
            this.selectPage.Load();
            Assert.IsTrue(this.selectPage.IsLoaded().Passed);
        }


        [Test]
        public void ListTest()
        {
            var elements = this.selectPage.Tabs;
            Assert.AreEqual(3, elements.Count);
        }

        [Test]
        public void ShadowListTest()
        {
            var elements = this.selectPage.Container.Title;
            Assert.AreEqual(1, elements.Count);
            Assert.AreEqual("Choose a pet:", elements[0].Text);
        }
    }
}
