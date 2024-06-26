﻿using System;
using System.Drawing;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using Zelenium.Core.Interfaces;
using Zelenium.Core.Utils;
using Zelenium.Core.WebDriver.Types;
using ValidationResult = Zelenium.Core.Model.ValidationResult;

namespace Zelenium.Core.UnitTests.CoreTests
{
    [TestFixture]
    public class AssertionTests
    {
        private readonly ValidationResult testResultOk = new() { Passed = true, Message = "Ok" };
        private readonly ValidationResult testResultFailed = new() { Passed = false, Message = "Not ok" };
        private Mock<ElementList<IElementContainer>> mockList;

        [SetUp]
        public void Setup()
        {
            this.mockList = new Mock<ElementList<IElementContainer>>(null, null, null, null, null, null);
        }

        [Test]
        public void IsTrueTestPassed()
        {
            Assertion.IsTrue(this.testResultOk);
        }

        [Test]
        public void IsTrueTestPassedWithMessage()
        {
            Assertion.IsTrue(this.testResultOk, "Extra message");
        }

        [Test]
        public void IsTrueTestFailed()
        {
            Assert.That(() => Assertion.IsTrue(this.testResultFailed),
                Throws.TypeOf<AssertionException>()
                .With.Message.Contains("Not ok"));
        }

        [Test]
        public void IsTrueTestFailedWithMessage()
        {
            Assert.That(() => Assertion.IsTrue(this.testResultFailed, "Extra message"),
               Throws.TypeOf<AssertionException>()
               .With.Message.Contains("Extra message: Not ok"));
        }

        [Test]
        public void IsReadableBasicTest()
        {
            var mockElement = new Mock<IElement>();
            mockElement.SetupGet(x => x.Color).Returns(Color.White);
            mockElement.SetupGet(e => e.BackgroundColor).Returns(Color.Black);

            Assertion.IsReadable(mockElement.Object, "message", 21.0);
        }

        [Test]
        public void IsReadableLowContrastTest()
        {
            var mockElement = new Mock<IElement>();
            mockElement.SetupGet(x => x.Color).Returns(Color.White);
            mockElement.SetupGet(e => e.BackgroundColor).Returns(Color.Black);

            Assert.That(() => Assertion.IsReadable(mockElement.Object, "message", 0.9),
                Throws.TypeOf<AssertionException>()
                .With.Message.Contains("Not applicable"));
        }

        [Test]
        public void IsReadableContrasFailedTest()
        {
            var mockElement = new Mock<IElement>();
            mockElement.SetupGet(x => x.Color).Returns(Color.FromArgb(161, 161, 161));
            mockElement.SetupGet(e => e.BackgroundColor).Returns(Color.FromArgb(255, 255, 255));

            Assert.That(() => Assertion.IsReadable(mockElement.Object, "some kind of element", 5.7),
            Throws.TypeOf<AssertionException>()
            .And.Message.Contains("  some kind of element | \r\nContrast ratio (2.58) of the given color pair is below the limit (5.7) | " +
            "\r\nColor: RGB(161,161,161), #A1A1A1 | \r\nBackground color: RGB(255,255,255), #FFFFFF\nAssert.That(validationResult.Passed, Is.True)\r\n  " +
            "Expected: True\r\n  But was:  False\r\n"));
        }

        [Test]
        public void TextIsValidTest()
        {
            var mockElement = new Mock<IElement>();
            mockElement.SetupGet(x => x.Text).Returns("lorem ipsum");
            mockElement.SetupGet(x => x.Displayed).Returns(true);

            Assertion.IsTextValid(mockElement.Object, "some kind of element");
        }

        [Test]
        public void TextIsValidNotDisplayedTest()
        {
            var mockElement = new Mock<IElement>();
            mockElement.SetupGet(x => x.Text).Returns("lorem ipsum");
            mockElement.SetupGet(x => x.Displayed).Returns(false);
            mockElement.SetupGet(x => x.Path).Returns("#id");

            Assert.That(() => Assertion.IsTextValid(mockElement.Object, "some kind of element"),
                Throws.TypeOf<AssertionException>()
                .With.Message.Contains("some kind of element | #id is not displayed")
                .With.Message.Contains("Expected: True")
                .With.Message.Contains("But was:  False"));
        }

        [Test]
        public void TextIsValidHasInterpolationTest()
        {
            var mockElement = new Mock<IElement>();
            mockElement.SetupGet(x => x.Text).Returns("lorem {{ipsum}}");
            mockElement.SetupGet(x => x.Displayed).Returns(true);
            mockElement.SetupGet(x => x.Path).Returns("#id");

            Assert.That(() => Assertion.IsTextValid(mockElement.Object, "some kind of element"),
                Throws.TypeOf<AssertionException>()
                .With.Message.Contains("some kind of element | Path: #id has following errors: Text has interpolation")
                .With.Message.Contains("Original text: 'lorem {{ipsum}}'")
                .With.Message.Contains("Expected: True")
                .With.Message.Contains("But was:  False"));
        }

        [Test]
        public void TextIsValidOnlySpacesTest()
        {
            var mockElement = new Mock<IElement>();
            mockElement.SetupGet(x => x.Text).Returns(" ");
            mockElement.SetupGet(x => x.Displayed).Returns(true);
            mockElement.SetupGet(x => x.Path).Returns("#id");

            Assert.That(() => Assertion.IsTextValid(mockElement.Object, "some kind of element"),
                Throws.TypeOf<AssertionException>()
                .With.Message.Contains("some kind of element | Path: #id has following errors: Text is empty or whitespace only")
                .With.Message.Contains("Original text: ' '")
                .With.Message.Contains("Expected: True")
                .With.Message.Contains("But was:  False"));
        }

        [Test]
        public void TextIsValidEmptyTest()
        {
            var mockElement = new Mock<IElement>();
            mockElement.SetupGet(x => x.Text).Returns(string.Empty);
            mockElement.SetupGet(x => x.Displayed).Returns(true);
            mockElement.SetupGet(x => x.Path).Returns("#id");

            Assert.That(() => Assertion.IsTextValid(mockElement.Object, "some kind of element"),
                Throws.TypeOf<AssertionException>()
                .With.Message.Contains("some kind of element | Path: #id has following errors: Text is empty or whitespace only")
                .With.Message.Contains("Original text: ''")
                .With.Message.Contains("Expected: True")
                .With.Message.Contains("But was:  False"));
        }

        [Test]
        public void TextIsValidHasCrsKeyTest()
        {
            var mockElement = new Mock<IElement>();
            mockElement.SetupGet(x => x.Text).Returns("lorem.ipsum");
            mockElement.SetupGet(x => x.Displayed).Returns(true);
            mockElement.SetupGet(x => x.Path).Returns("#id");

            Assert.That(() => Assertion.IsTextValid(mockElement.Object, "some kind of element"),
                Throws.TypeOf<AssertionException>()
                .With.Message.Contains("some kind of element | Path: #id has following errors: Text is like a CRS key")
                .With.Message.Contains("Original text: 'lorem.ipsum'")
                .With.Message.Contains("Expected: True")
                .With.Message.Contains("But was:  False"));
        }

        [Test]
        public void TextIsValidHasHtmlTagTest()
        {
            var mockElement = new Mock<IElement>();
            mockElement.SetupGet(x => x.Text).Returns("lorem <br> ipsum");
            mockElement.SetupGet(x => x.Displayed).Returns(true);
            mockElement.SetupGet(x => x.Path).Returns("#id");

            Assert.That(() => Assertion.IsTextValid(mockElement.Object, "some kind of element"),
                Throws.TypeOf<AssertionException>()
                .With.Message.Contains("some kind of element | Path: #id has following errors: Text has Html tag")
                .With.Message.Contains("Original text: 'lorem <br> ipsum'")
                .With.Message.Contains("Expected: True")
                .With.Message.Contains("But was:  False"));
        }

        [Test]
        public void TextIsValidHascMultipleErrorsTest()
        {
            var mockElement = new Mock<IElement>();
            mockElement.SetupGet(x => x.Text).Returns("lorem <br> ipsum {{saka}}");
            mockElement.SetupGet(x => x.Displayed).Returns(true);
            mockElement.SetupGet(x => x.Path).Returns("#id");

            Assert.That(() => Assertion.IsTextValid(mockElement.Object, "some kind of element"),
                Throws.TypeOf<AssertionException>()
                .With.Message.Contains("some kind of element | Path: #id has following errors:")
                .With.Message.Contains("Text has interpolation")
                .With.Message.Contains("Text has Html tag")
                .With.Message.Contains("Original text: 'lorem <br> ipsum {{saka}}'")
                .With.Message.Contains("Expected: True")
                .With.Message.Contains("But was:  False"));
        }

        [Test]
        public void TextIsValidHascMultipleErrorsButNotDisplayedTest()
        {
            var mockElement = new Mock<IElement>();
            mockElement.SetupGet(x => x.Text).Returns("lorem <br> ipsum {{saka}}");
            mockElement.SetupGet(x => x.Displayed).Returns(false);
            mockElement.SetupGet(x => x.Path).Returns("#id");

            Assert.That(() => Assertion.IsTextValid(mockElement.Object, "some kind of element"),
                Throws.TypeOf<AssertionException>()
                .With.Message.Contains("some kind of element | #id is not displayed")
                .With.Message.Contains("Expected: True")
                .With.Message.Contains("But was:  False"));
        }

        [Test]
        public void IsDisappearedTest()
        {
            var mockElement = new Mock<IElement>();
            mockElement.SetReturnsDefault(true);
            Assertion.IsDisappeared(mockElement.Object, "mock element");
        }

        [Test]
        public void IsDisappearedFalseTest()
        {
            var mockElement = new Mock<IElementContainer>();

            mockElement.Setup(x => x.IsDisappeared(It.IsAny<TimeSpan>())).Returns(false);

            Assert.That(() => Assertion.IsDisappeared(mockElement.Object, "mock element"),
                Throws.TypeOf<AssertionException>()
                .With.Message.Contains("Element still visible")
                .With.Message.Contains("Type: IElementContainerProxy")
                .With.Message.Contains("Property name: mock element")
                .With.Message.Contains("Path: "));
        }

        [Test]
        public void WaitForCollectionNumberAreEqualsTest()
        {
            this.mockList.Setup(x => x.Count).Returns(5);

            Assertion.WaitColllectionCountAreEqual(5, this.mockList.Object, "mock list");
        }

        [Test]
        public void WaitForCollectionNumberAreEqualsNegativeTest()
        {
            this.mockList.Setup(x => x.Count).Returns(1);

            Assert.That(() => Assertion.WaitColllectionCountAreEqual(5, this.mockList.Object, "mock list", TimeSpan.FromSeconds(1)),
                Throws.TypeOf<WebDriverTimeoutException>()
                .With.Message.Contains("mock list"));
        }

        [Test]
        public void WaitForCollectionNumberAreNotEqualsTest()
        {
            this.mockList.Setup(x => x.Count).Returns(5);

            Assertion.WaitColllectionCountAreNotEqual(1, this.mockList.Object, "mock list");
        }

        [Test]
        public void WaitForCollectionNumberAreNotEqualsNegativeTest()
        {
            this.mockList.Setup(x => x.Count).Returns(1);

            Assert.That(() => Assertion.WaitColllectionCountAreNotEqual(1, this.mockList.Object, "mock list", TimeSpan.FromSeconds(1)),
                Throws.TypeOf<WebDriverTimeoutException>()
                .With.Message.Contains("mock list"));
        }

        [Test]
        public void WaitColllectionCountAreGreaterTest()
        {
            this.mockList.Setup(x => x.Count).Returns(5);

            Assertion.WaitColllectionCountAreGreater(4, this.mockList.Object, "mock list");
        }

        [Test]
        public void WaitColllectionCountAreGreaterNegativeTest()
        {
            this.mockList.Setup(x => x.Count).Returns(5);

            Assert.That(() => Assertion.WaitColllectionCountAreGreater(5, this.mockList.Object, "mock list", TimeSpan.FromSeconds(1)),
                Throws.TypeOf<WebDriverTimeoutException>()
                .With.Message.Contains("mock list"));

            Assert.That(() => Assertion.WaitColllectionCountAreGreater(6, this.mockList.Object, "mock list", TimeSpan.FromSeconds(1)),
                Throws.TypeOf<WebDriverTimeoutException>()
                .With.Message.Contains("mock list"));
        }

        [Test]
        public void WaitColllectionCountAreLessTest()
        {
            this.mockList.Setup(x => x.Count).Returns(5);

            Assertion.WaitColllectionCountAreLess(6, this.mockList.Object, "mock list");
        }

        [Test]
        public void WaitColllectionCountAreLessNegativeTest()
        {
            this.mockList.Setup(x => x.Count).Returns(5);

            Assert.That(() => Assertion.WaitColllectionCountAreLess(5, this.mockList.Object, "mock list", TimeSpan.FromSeconds(1)),
                Throws.TypeOf<WebDriverTimeoutException>()
                .With.Message.Contains("mock list"));
            Assert.That(() => Assertion.WaitColllectionCountAreLess(4, this.mockList.Object, "mock list", TimeSpan.FromSeconds(1)),
               Throws.TypeOf<WebDriverTimeoutException>()
               .With.Message.Contains("mock list"));
        }

        [Test]
        public void WaitColllectionCountAreLessOrEqualTest()
        {
            this.mockList.Setup(x => x.Count).Returns(5);

            Assertion.WaitColllectionCountAreLessOrEqual(5, this.mockList.Object, "mock list");
            Assertion.WaitColllectionCountAreLessOrEqual(6, this.mockList.Object, "mock list");
        }

        [Test]
        public void WaitColllectionCountAreLessOrEqualNegativeTest()
        {
            this.mockList.Setup(x => x.Count).Returns(5);

            Assert.That(() => Assertion.WaitColllectionCountAreLess(4, this.mockList.Object, "mock list", TimeSpan.FromSeconds(1)),
                Throws.TypeOf<WebDriverTimeoutException>()
                .With.Message.Contains("mock list"));
        }

        [Test]
        public void WaitColllectionCountAreGreaterOrEqualTest()
        {
            this.mockList.Setup(x => x.Count).Returns(5);

            Assertion.WaitColllectionCountAreGreaterOrEqual(4, this.mockList.Object, "mock list");
            Assertion.WaitColllectionCountAreGreaterOrEqual(5, this.mockList.Object, "mock list");
        }

        [Test]
        public void WaitColllectionCountAreGreaterOrEqualNegativeTest()
        {
            this.mockList.Setup(x => x.Count).Returns(5);

            Assert.That(() => Assertion.WaitColllectionCountAreGreaterOrEqual(6, this.mockList.Object, "mock list", TimeSpan.FromSeconds(1)),
                Throws.TypeOf<WebDriverTimeoutException>()
                .With.Message.Contains("mock list"));
        }
    }
}
