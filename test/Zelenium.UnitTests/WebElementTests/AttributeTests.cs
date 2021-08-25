using MaterialAngular.PageObjects;
using NUnit.Framework;
using Zelenium.Core.Config;
using Zelenium.Core.Exceptions;
using Zelenium.Core.WebDriver.Types;

namespace Zelenium.UnitTests.WebElementTests
{
    [TestFixture]
    public class AttributeTests : BaseTest
    {
        private ButtonPage buttonPage;
        private Element basicButton;
        private string attributeName = "attributeTestName";
        private string attributeValue = "attributeTestValue";

        [SetUp]
        public void SetUp()
        {
            this.buttonPage = new ButtonPage(this.driver);
            this.buttonPage.Load();
            Assert.IsTrue(this.buttonPage.IsLoaded().Passed);

            this.basicButton = this.buttonPage.ButtonOverview.BasicWithoutDelay;
        }

        [Test]
        public void GetAttributeNotExistTest()
        {
            Assert.Null(this.basicButton.Attributes.Get("notexistAttribute"));
        }

        [Test]
        public void GetAttributeTest()
        {
            this.basicButton.ExecuteScript(BaseQueries.AddAttribute(attributeName, attributeValue));
            Assert.AreEqual(attributeValue, this.basicButton.Attributes.Get(attributeName));
        }

        [Test]
        public void HasAttributeNotExistTest()
        {
            Assert.False(this.basicButton.Attributes.Has("notexistAttribute"));
        }

        [Test]
        public void HasAttributeTest()
        {
            Assert.False(this.basicButton.Attributes.Has(attributeName));
            this.basicButton.ExecuteScript(BaseQueries.AddAttribute(attributeName, attributeValue));
            Assert.True(this.basicButton.Attributes.HasWithin(attributeName));
        }

        [Test]
        public void WaitForAttributeNotExistTest()
        {
            Assert.Throws<AttributeNotExistException>(() => this.basicButton.Attributes.WaitFor("notexistAttribute"));
        }

        [Test]
        public void WaitForAttributeTest()
        {
            Assert.Throws<AttributeNotExistException>(() => this.basicButton.Attributes.WaitFor(attributeName));
            this.basicButton.ExecuteScript(BaseQueries.AddAttribute(attributeName, attributeValue));
            this.basicButton.Attributes.WaitFor(attributeName);
        }

        [Test]
        public void HasWithinAttributeNotExistTest()
        {
            Assert.False(this.basicButton.Attributes.HasWithin("notexistAttribute"));
        }

        [Test]
        public void HasWithinAttributeTest()
        {
            Assert.False(this.basicButton.Attributes.HasWithin(attributeValue));
            this.basicButton.ExecuteScript(BaseQueries.AddAttribute(attributeName, attributeValue));
            this.basicButton.Attributes.HasWithin(attributeName);
        }
    }
}
