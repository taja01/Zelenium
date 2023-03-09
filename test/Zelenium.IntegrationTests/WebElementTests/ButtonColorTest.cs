﻿using MaterialAngular.PageObjects;
using NUnit.Framework;
using Zelenium.Core.Utils;

namespace Zelenium.IntegrationTests.WebElementTests
{
    [TestFixture]
    public class ButtonColorTest : BaseTest
    {
        private ButtonPage buttonPage;

        [SetUp]
        public void SetUp()
        {
            this.buttonPage = new ButtonPage(this.driver);
            this.buttonPage.Load();
            Assertion.IsTrue(this.buttonPage.IsLoaded());
        }

        [Test]
        public void BasicContrastTest()
        {
            var basicButton = this.buttonPage.ButtonOverview.Basic;
            var calcukatedContrast = ColorUtil.GetReadability(basicButton.Color, basicButton.BackgroundColor);
            Assert.AreEqual(20.12, calcukatedContrast, 0.01);
        }

        [Test]
        public void PrimaryContrastTest()
        {
            var primaryButton = this.buttonPage.ButtonOverview.Primary;
            var calcukatedContrast = ColorUtil.GetReadability(primaryButton.Color, primaryButton.BackgroundColor);
            Assert.AreEqual(6.58, calcukatedContrast, 0.01);
        }

        [Test]
        public void AccentContrastTest()
        {
            var accentButton = this.buttonPage.ButtonOverview.Accentc;
            var calcukatedContrast = ColorUtil.GetReadability(accentButton.Color, accentButton.BackgroundColor);
            Assert.AreEqual(3.19, calcukatedContrast, 0.01);
        }

        [Test]
        public void WarnContrastTest()
        {
            var warnButton = this.buttonPage.ButtonOverview.Warn;
            var calcukatedContrast = ColorUtil.GetReadability(warnButton.Color, warnButton.BackgroundColor);
            Assert.AreEqual(3.53, calcukatedContrast, 0.01);
        }

        [Test]
        public void DisabledContrastTest()
        {
            var disabledButton = this.buttonPage.ButtonOverview.Disabled;
            var calcukatedContrast = ColorUtil.GetReadability(disabledButton.Color, disabledButton.BackgroundColor);
            Assert.AreEqual(2.62, calcukatedContrast, 0.01);
        }

        [Test]
        public void LinkContrastTest()
        {
            var linkButton = this.buttonPage.ButtonOverview.Link;
            var calcukatedContrast = ColorUtil.GetReadability(linkButton.Color, linkButton.BackgroundColor);
            Assert.AreEqual(20.12, calcukatedContrast, 0.01);
        }
    }
}