using NUnit.Framework;
using OpenQA.Selenium;
using Zelenium.Core.Enums;
using static Zelenium.Core.Utils.ElementUtil;

namespace Zelenium.Core.UnitTests.CoreTests
{
    [TestFixture]
    public class ElementUtilTests
    {

        [Test]
        public void TestMobile1()
        {
            Assert.That(Locators(desktopSelector: ".desktop", mobileSelector: ".mobile", Device.Mobile), Is.EqualTo(".mobile"));
        }

        [Test]
        public void TestMobile2()
        {
            Assert.That(Locators(desktopSelector: By.Id("desktop"), mobileSelector: By.Id("mobile"), Device.Mobile), Is.EqualTo(By.Id("mobile")));
        }

        [Test]
        public void TestMobile3()
        {
            Assert.That(Locators(desktopSelector: By.Id("desktop"), mobileSelector: By.CssSelector("#mobile"), Device.Mobile), Is.EqualTo(By.CssSelector("#mobile")));
        }

        [Test]
        public void TestDesktop()
        {
            Assert.That(Locators(desktopSelector: By.Id("desktop"), mobileSelector: By.CssSelector("#mobile"), Device.Desktop), Is.EqualTo(By.Id("desktop")));
        }
    }
}
