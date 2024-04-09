using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using Zelenium.Extension.Screenshot;

namespace Zelenium.Extension.Screenshot.Tests
{
    public class FirefoxTests
    {

        FirefoxDriver driver;

        [SetUp]
        public void Setup()
        {
            var firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArgument("--no-sandbox");
            firefoxOptions.AddArgument("--allow-running-insecure-content");
            firefoxOptions.AddArgument("--ignore-gpu-blocklis");
            firefoxOptions.AddArgument("--headless");
            firefoxOptions.AcceptInsecureCertificates = true;
            this.driver = new FirefoxDriver(firefoxOptions);

        }

        [TearDown]
        public void AfterTest()
        {
            this.driver.Close();
            this.driver.Dispose();
        }

        [Test]
        public void Test1()
        {

            this.driver.Url = "https://bank.codes/swift-code-search/";

            this.driver.GetFullPageScreenshot("test_long_page.png");
        }

        [Test]
        public void Test2()
        {
            this.driver.Url = "http://info.cern.ch/";

            this.driver.GetFullPageScreenshot("test_simple_page.png");
        }
    }
}
