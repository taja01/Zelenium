using System.Linq;
using MaterialAngular.PageObjects;
using NUnit.Framework;

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
            Assert.IsTrue(this.selectPage.IsLoaded().Passed);
        }

        [Test]
        public void GetAllOptionsTest()
        {
            var allOptions = this.selectPage.Language.GetAllOptions();

            Assert.AreEqual(9, allOptions.Count);
            Assert.AreEqual(this.selectPage.Language.SelectedText, this.selectPage.Language.GetSelectedOptionsTexts()[0]);
            Assert.AreEqual(0, this.selectPage.Language.GetCurrentIndex());
            Assert.AreEqual("en-US", this.selectPage.Language.GetSelectedOptionsValues()[0]);
            Assert.AreEqual(this.selectPage.Language.SelectedValue, this.selectPage.Language.GetSelectedOptionsValues()[0]);

        }

        [Test]
        public void SetByIndexTest()
        {
            var allOptions = this.selectPage.Language.GetAllOptions();

            this.selectPage.Language.SetByIndex(allOptions.Count - 1);
            Assert.AreEqual(allOptions.Count - 1, this.selectPage.Language.GetCurrentIndex());
        }

        [Test]
        public void SetByValueTest()
        {
            var option = this.selectPage.Language.GetAllOptions().ElementAt(5);

            this.selectPage.Language.SetByValue(option.Key);
            Assert.AreEqual(option.Value, this.selectPage.Language.SelectedText);
            Assert.AreEqual(option.Key, this.selectPage.Language.SelectedValue);
        }

        [Test]
        public void SetByTextTest()
        {
            var option = this.selectPage.Language.GetAllOptions().ElementAt(3);

            this.selectPage.Language.SetByText(option.Value);
            Assert.AreEqual(option.Value, this.selectPage.Language.SelectedText);
            Assert.AreEqual(option.Key, this.selectPage.Language.SelectedValue);
        }

        [Test]
        public void CountTest()
        {
            Assert.AreEqual(9, this.selectPage.Language.GetAllOptions().Count);
        }
    }
}
