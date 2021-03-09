using System.Linq;
using MaterialAngular.PageObjects;
using NUnit.Framework;
using Zelenium.Core.Utils;

namespace Zelenium.UnitTests.WebElementTests
{
    [TestFixture]
    public class SelectElementTests : BaseTest
    {
        private SelectPage selectPage;

        [SetUp]
        public void SetUp()
        {
            this.selectPage = new SelectPage(this.driver);
            this.selectPage.Load();
            Assertion.IsTrue(this.selectPage.IsLoaded());
            this.driver.SwitchTo().Frame(this.selectPage.IFrame.DisplayedWebElement);
        }

        [Test]
        public void GetAllOptionsTest()
        {
            var allOptions = this.selectPage.Container.Pets.GetAllOptions();

            Assert.AreEqual(7, allOptions.Count);
            Assert.AreEqual(this.selectPage.Container.Pets.SelectedText, this.selectPage.Container.Pets.GetSelectedOptionsTexts()[0]);
            Assert.AreEqual(0, this.selectPage.Container.Pets.GetCurrentIndex());
            Assert.IsEmpty(this.selectPage.Container.Pets.GetSelectedOptionsValues()[0]);
        }

        [Test]
        public void SetByIndexTest()
        {
            var allOptions = this.selectPage.Container.Pets.GetAllOptions();

            this.selectPage.Container.Pets.SetByIndex(allOptions.Count - 1);
            Assert.AreEqual(allOptions.Count - 1, this.selectPage.Container.Pets.GetCurrentIndex());
        }

        [Test]
        public void SetByValueTest()
        {
            var option = this.selectPage.Container.Pets.GetAllOptions().ElementAt(5);

            this.selectPage.Container.Pets.SetByValue(option.Key);
            Assert.AreEqual(option.Value, this.selectPage.Container.Pets.SelectedText);
            Assert.AreEqual(option.Key, this.selectPage.Container.Pets.SelectedValue);
        }

        [Test]
        public void SetByTextTest()
        {
            var option = this.selectPage.Container.Pets.GetAllOptions().ElementAt(3);

            this.selectPage.Container.Pets.SetByText(option.Value);
            Assert.AreEqual(option.Value, this.selectPage.Container.Pets.SelectedText);
            Assert.AreEqual(option.Key, this.selectPage.Container.Pets.SelectedValue);
        }

        [Test]
        public void CountTest()
        {
            Assert.AreEqual(7, this.selectPage.Container.Pets.GetAllOptions().Count);
        }
    }
}
