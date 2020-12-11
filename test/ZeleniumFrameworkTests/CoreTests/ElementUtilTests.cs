using NUnit.Framework;
using OpenQA.Selenium;
using ZeleniumFramework.Enums;
using static ZeleniumFramework.Utils.ElementUtil;

namespace ZeleniumFrameworkTest.CoreTests
{
    [TestFixture]
    public class ElementUtilTests
    {

        [Test]
        public void TestMobile1()
        {
            Assert.AreEqual(".mobile", Locators(desktopSelector: ".desktop", mobileSelector: ".mobile", Device.Mobile));
        }

        [Test]
        public void TestMobile2()
        {
            Assert.AreEqual(By.Id("mobile"), Locators(desktopSelector: By.Id("desktop"), mobileSelector: By.Id("mobile"), Device.Mobile));
        }

        [Test]
        public void TestMobile3()
        {
            Assert.AreEqual(By.CssSelector("#mobile"), Locators(desktopSelector: By.Id("desktop"), mobileSelector: By.CssSelector("#mobile"), Device.Mobile));
        }

        [Test]
        public void TestDesktop()
        {
            Assert.AreEqual(By.Id("desktop"), Locators(desktopSelector: By.Id("desktop"), mobileSelector: By.CssSelector("#mobile"), Device.Desktop));
        }
    }
}
