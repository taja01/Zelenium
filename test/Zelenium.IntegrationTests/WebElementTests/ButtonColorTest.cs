﻿using MaterialAngular.PageObjects;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using Zelenium.Core.Utils;

namespace Zelenium.IntegrationTests.WebElementTests
{
    [TestFixture]
    public class ButtonColorTest : BaseTest
    {
        private ButtonPage buttonPage;
        private ILogger<ButtonPage> logger;

        [OneTimeSetUp]
        public void Setup()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
                .CreateLogger();

            var loggerFactory = new LoggerFactory().AddSerilog();
            logger = loggerFactory.CreateLogger<ButtonPage>();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Log.CloseAndFlush();
        }

        [SetUp]
        public void SetUp()
        {
            this.buttonPage = new ButtonPage(this.logger, this.driver);
            this.buttonPage.Load();
            Assertion.IsTrue(this.buttonPage.IsLoaded());
        }

        [Test]
        public void BasicContrastTest()
        {
            var basicButton = this.buttonPage.ButtonOverview.Basic;
            var calculatedContrast = ColorUtil.GetReadability(basicButton.Color, basicButton.BackgroundColor);
            Assert.That(calculatedContrast, Is.EqualTo(20.12).Within(0.01));
        }

        [Test]
        public void PrimaryContrastTest()
        {
            var primaryButton = this.buttonPage.ButtonOverview.Primary;
            var calculatedContrast = ColorUtil.GetReadability(primaryButton.Color, primaryButton.BackgroundColor);
            Assert.That(calculatedContrast, Is.EqualTo(6.58).Within(0.01));
        }

        [Test]
        public void AccentContrastTest()
        {
            var accentButton = this.buttonPage.ButtonOverview.Accentc;
            var calculatedContrast = ColorUtil.GetReadability(accentButton.Color, accentButton.BackgroundColor);
            Assert.That(calculatedContrast, Is.EqualTo(3.19).Within(0.01));
        }

        [Test]
        public void WarnContrastTest()
        {
            var warnButton = this.buttonPage.ButtonOverview.Warn;
            var calculatedContrast = ColorUtil.GetReadability(warnButton.Color, warnButton.BackgroundColor);
            Assert.That(calculatedContrast, Is.EqualTo(3.53).Within(0.01));
        }

        [Test]
        public void DisabledContrastTest()
        {
            var disabledButton = this.buttonPage.ButtonOverview.Disabled;
            var calculatedContrast = ColorUtil.GetReadability(disabledButton.Color, disabledButton.BackgroundColor);
            Assert.That(calculatedContrast, Is.EqualTo(2.62).Within(0.01));
        }

        [Test]
        public void LinkContrastTest()
        {
            var linkButton = this.buttonPage.ButtonOverview.Link;
            var calculatedContrast = ColorUtil.GetReadability(linkButton.Color, linkButton.BackgroundColor);
            Assert.That(calculatedContrast, Is.EqualTo(20.12).Within(0.01));
        }
    }
}
