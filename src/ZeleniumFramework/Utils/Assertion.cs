using System;
using System.Drawing;
using NUnit.Framework;
using ZeleniumFramework.Model;
using ZeleniumFramework.WebDriver;

namespace ZeleniumFramework.Utils
{
    //TODO: write tests
    public class Assertion
    {
        public static ElementValidatorBuilder On(Element element, string message)
        {
            return new ElementValidatorBuilder(element, message);
        }

        public static ElementValidatorBuilder On(ElementList<Element> elements, string message)
        {
            foreach (var element in elements)
            {
                return new ElementValidatorBuilder(element, message);
            }

            return null;
        }

        public static void IsDisappeared(Element element, string message, TimeSpan? timeout = null)
        {
            Assert.IsTrue(element.IsDisappeared(timeout), message);
        }

        public static void IsTextValid(Element element, string message)
        {
            IsTrue(TestTextValidity(element, message));
        }

        public static void IsReadable(Element element, string message, double readabilityLevel)
        {
            IsTrue(TestReadability(element.Color, element.BackgroundColor, message, readabilityLevel));
        }

        public static void IsTrue(ValidationResult validationResult)
        {
            Assert.IsTrue(validationResult.Passed, validationResult.Message);
        }

        public static void IsTrue(ValidationResult validationResult, string message)
        {
            Assert.IsTrue(validationResult.Passed, $"{message}: {validationResult.Message}");
        }

        private static ValidationResult TestTextValidity(Element element, string message)
        {
            var path = element.Path;
            var displayed = element.Displayed;
            if (!displayed)
            {
                return Fail($"{message} | {path} is not displayed");
            }
            var text = element.Text;

            // not only whitespace
            if (string.IsNullOrWhiteSpace(text))
            {
                return Fail($"{message} | Text of {path} is empty or whitespace only");
            }

            // not contain interpolation: {{var}}
            if (text.HasInterpolation())
            {
                return Fail($"{message} | Text of {path} has interpolation: {text}");
            }

            if (text.IsCrsKeyLike())
            {
                return Fail($"{message} | Text of {path} is like a CRS key: {text}");
            }

            if (text.HasHtmlTag())
            {
                return Fail($"{message} | Text of {path} has Html tag: {text}");
            }

            return Pass();
        }

        private static ValidationResult TestReadability(Color color, Color backgroundColor, string message, double readabilityLevel)
        {
            if (readabilityLevel < 1)
            {
                return new ValidationResult { Message = "Not applicable", Passed = true };
            }

            if (!ColorUtil.IsReadable(color, backgroundColor, readabilityLevel))
            {

                return Fail($"{message} | {Environment.NewLine}"
                            + $"Contrast ratio ({ColorUtil.GetReadability(color, backgroundColor)}) of the given color pair is below the limit ({readabilityLevel}) | {Environment.NewLine}"
                            + $"Color: {ColorUtil.ToRgbString(color)},{ColorUtil.ToHexString(color)} | {Environment.NewLine}"
                            + $"Background color: {ColorUtil.ToRgbString(backgroundColor)},{ColorUtil.ToHexString(backgroundColor)}");
            }
            return Pass();
        }

        private static ValidationResult Fail(string message) => new ValidationResult { Message = message, Passed = false };

        private static ValidationResult Pass() => new ValidationResult { Message = "OK", Passed = true };
    }
}
