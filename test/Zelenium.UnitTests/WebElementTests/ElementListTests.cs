using System.Diagnostics;
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
            var elements = this.selectPage.LeftMenu;
            Assert.AreEqual(14, elements.Count);

            foreach (var item in elements)
            {
                Debug.WriteLine(item.Text);
                StringAssert.Contains("<", item.Text);
                StringAssert.Contains(">", item.Text);
            }
        }

        [Test]
        public void ShadowListTest()
        {
            this.driver.SwitchTo().Frame(this.selectPage.IFrame.DisplayedWebElement);

            var elements = this.selectPage.Container.Title;
            Assert.AreEqual(1, elements.Count);
            Assert.AreEqual("Choose a pet:", elements[0].Text);
        }
    }
}
