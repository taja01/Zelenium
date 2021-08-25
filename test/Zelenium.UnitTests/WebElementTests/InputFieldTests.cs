using MaterialAngular.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Zelenium.UnitTests.WebElementTests
{
    [TestFixture]
    public class InputFieldTests : BaseTest
    {
        private InputFieldPage inputFieldPage;

        [SetUp]
        public void SetUp()
        {
            this.inputFieldPage = new InputFieldPage(this.driver);
            this.inputFieldPage.Load();
            Assert.IsTrue(this.inputFieldPage.IsLoaded().Passed);
        }

        [Test]
        public void ReadTest()
        {
            Assert.AreEqual("Sushi", this.inputFieldPage.InputField.Text);
        }

        [Test]
        public void WriteInputFieldTest()
        {
            var newText = "apple";
            this.inputFieldPage.InputField.SendKeys(newText);
            Assert.AreEqual(newText, this.inputFieldPage.InputField.Text);
        }

        [Test]
        public void WriteTextAreaTest()
        {
            var newText = "apple\nbananna";
            this.inputFieldPage.TextAreaField.SendKeys(newText);
        }

        [Test]
        public void ClearTest()
        {
            var newText = "apple";

            this.inputFieldPage.InputField.SendKeys(newText);
            this.inputFieldPage.InputField.Clear();

            Assert.AreEqual(string.Empty, this.inputFieldPage.InputField.Text);
        }

        [Test]
        public void WriteInputFieldExceptionTest()
        {
            var newText = "apple\napple";
            Assert.Throws<WebDriverTimeoutException>(() => this.inputFieldPage.InputField.SendKeys(newText));
        }
    }
}
