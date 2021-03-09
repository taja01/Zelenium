using MaterialAngular.PageObjects;
using NUnit.Framework;

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
            Assert.AreEqual("Sushi", this.inputFieldPage.PageThisInputField.Text);
        }

        [Test]
        public void WriteTest()
        {
            var newText = "apple";
            this.inputFieldPage.PageThisInputField.SendKeys(newText);
            Assert.AreEqual(newText, this.inputFieldPage.PageThisInputField.Text);
        }
    }
}
