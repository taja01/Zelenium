using MaterialAngular.PageObjects;
using NUnit.Framework;
using Zelenium.Core.Config;
using Zelenium.Core.Exceptions;
using Zelenium.Core.WebDriver.Types;

namespace Zelenium.IntegrationTests.WebElementTests
{
    [TestFixture]
    public class AttributeTests : BaseTest
    {
        private ButtonPage buttonPage;
        private Element basicButton;
        private const string ATTIRBUTE_NAME = "attributeTestName";
        private const string ATTRIBUTE_VALUE = "attributeTestValue";

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
            this.basicButton.ExecuteScript(BaseQueries.AddAttribute(ATTIRBUTE_NAME, ATTRIBUTE_VALUE));
            Assert.AreEqual(ATTRIBUTE_VALUE, this.basicButton.Attributes.Get(ATTIRBUTE_NAME));
        }

        [Test]
        public void HasAttributeNotExistTest()
        {
            Assert.False(this.basicButton.Attributes.Has("notexistAttribute"));
        }

        [Test]
        public void HasAttributeTest()
        {
            Assert.False(this.basicButton.Attributes.Has(ATTIRBUTE_NAME));
            this.basicButton.ExecuteScript(BaseQueries.AddAttribute(ATTIRBUTE_NAME, ATTRIBUTE_VALUE));
            Assert.True(this.basicButton.Attributes.HasWithin(ATTIRBUTE_NAME));
        }

        [Test]
        public void WaitForAttributeNotExistTest()
        {
            Assert.Throws<AttributeNotExistException>(() => this.basicButton.Attributes.WaitFor("notexistAttribute"));
        }

        [Test]
        public void WaitForAttributeTest()
        {
            Assert.Throws<AttributeNotExistException>(() => this.basicButton.Attributes.WaitFor(ATTIRBUTE_NAME));
            this.basicButton.ExecuteScript(BaseQueries.AddAttribute(ATTIRBUTE_NAME, ATTRIBUTE_VALUE));
            this.basicButton.Attributes.WaitFor(ATTIRBUTE_NAME);
        }

        [Test]
        public void HasWithinAttributeNotExistTest()
        {
            Assert.False(this.basicButton.Attributes.HasWithin("notexistAttribute"));
        }

        [Test]
        public void HasWithinAttributeTest()
        {
            Assert.False(this.basicButton.Attributes.HasWithin(ATTRIBUTE_VALUE));
            this.basicButton.ExecuteScript(BaseQueries.AddAttribute(ATTIRBUTE_NAME, ATTRIBUTE_VALUE));
            this.basicButton.Attributes.HasWithin(ATTIRBUTE_NAME);
        }
    }
}
