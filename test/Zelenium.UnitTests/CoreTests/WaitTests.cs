using System;
using NUnit.Framework;
using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver;

namespace Zelenium.UnitTests.CoreTests
{
    [TestFixture]
    public class WaitTests
    {
        [Test]
        public void TestDefaultException()
        {
            Assert.Catch<WebDriverTimeoutException>(() => new Wait().Timeout(TimeSpan.Zero)
                                                                    .Until(() => false));
        }

        [Test]
        public void DefaultTest()
        {
            Wait.Initialize()
                .Until(() => true);
        }

        [Test]
        public void DefaultTestErrorMessage()
        {
            var errorMessage = "test message";

            Assert.That(() => new Wait()
                                .Timeout(TimeSpan.Zero)
                                .Message(errorMessage)
                                .Until(() => false),
               Throws.TypeOf<WebDriverTimeoutException>()
                  .With.Message.Contains(errorMessage));
        }

        [Test]
        public void TestIgnoreExceptionTypes()
        {
            var throwEx = true;
            new Wait()
                .IgnoreExceptionTypes(typeof(NullReferenceException))
                .Timeout(TimeSpan.FromSeconds(5))
                .Until(() =>
                {
                    if (throwEx)
                    {
                        throwEx = false;
                        throw new NullReferenceException();
                    }
                    return true;
                });
        }

        [Test]
        public void TestSuppriseException()
        {
            Assert.That(() => new Wait()
                                    .Timeout(TimeSpan.FromSeconds(5))
                                    .Until(() =>
                                    {
                                        throw new NullReferenceException();
                                    }),
                                    Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void TestThrowCustomException()
        {
            var errorMessage = "test message";
            Assert.That(() => new Wait().Throw<IgnoreException>()
                                         .Timeout(TimeSpan.Zero)
                                         .Message(errorMessage)
                                         .Until(() => false),
                             Throws.TypeOf<IgnoreException>()
                             .With.Message.Contains(errorMessage));
        }

        [Test]
        public void TestSuccessTrue()
        {
            Assert.IsTrue(
                Wait.Initialize()
                    .Timeout(TimeSpan.Zero)
                    .Success(() => "a" == "a")
                    );
        }

        [Test]
        public void TestSuccessFalse()
        {
            Assert.IsFalse(
                Wait.Initialize()
                    .Timeout(TimeSpan.Zero)
                    .Success(() => "a" != "a")
            );
        }

        [Test]
        public void ExtendedUntilTest()
        {
            static ValidationResult Isloaded()
            {
                return new ValidationResult { Passed = false, Message = "Title missing" };
            }

            Assert.That(() => new Wait().Message("Cannot load page")
                                        .Timeout(TimeSpan.Zero)
                                        .Until(Isloaded, t => t.Passed, t => t.Message),
                Throws.TypeOf<WebDriverTimeoutException>()
                .With.Message.Contains("Cannot load page")
                .With.Message.Contains("Title missing"));
        }

        [Test]
        public void ExtendedUntilWithUnExceptionTest()
        {
            static ValidationResult IsLoaded()
            {
                throw new DivideByZeroException();
            }

            Assert.That(() => new Wait().Message("Cannot load page")
                                        .Timeout(TimeSpan.FromSeconds(5))
                                        .Until(IsLoaded, t => t.Passed, t => t.Message),
                Throws.TypeOf<DivideByZeroException>());
        }


    }
}
