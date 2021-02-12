using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver;
using Zelenium.Core.WebDriver.Interfaces;

namespace Zelenium.Core.Utils
{
    public class Assertion
    {
        public static ElementValidatorBuilder On(IElement element, string message)
        {
            return new ElementValidatorBuilder(element, message);
        }

        public static ElementValidatorBuilder On(ElementList<IElement> elements, string message)
        {
            foreach (var element in elements)
            {
                return new ElementValidatorBuilder(element, message);
            }

            return null;
        }

        public static void WaitColllectionCountAreEqual(int expectedNumber, ElementList<IElementContainer> list, string message, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Timeout(timeout)
                .Message(message)
                .Until(() => list.Count == expectedNumber);
        }

        public static void WaitColllectionCountAreNotEqual(int expectedNumber, ElementList<IElementContainer> list, string message, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Timeout(timeout)
                .Message(message)
                .Until(() => list.Count != expectedNumber);
        }

        public static void WaitColllectionCountAreLess(int expectedNumber, ElementList<IElementContainer> list, string message, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Timeout(timeout)
                .Message(message)
                .Until(() => list.Count < expectedNumber);
        }

        public static void WaitColllectionCountAreLessOrEqual(int expectedNumber, ElementList<IElementContainer> list, string message, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Timeout(timeout)
                .Message(message)
                .Until(() => list.Count <= expectedNumber);
        }

        public static void WaitColllectionCountAreGreater(int expectedNumber, ElementList<IElementContainer> list, string message, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Timeout(timeout)
                .Message(message)
                .Until(() => list.Count > expectedNumber);
        }

        public static void WaitColllectionCountAreGreaterOrEqual(int expectedNumber, ElementList<IElementContainer> list, string message, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Timeout(timeout)
                .Message(message)
                .Until(() => list.Count >= expectedNumber);
        }

        public static void IsDisplayed(IElementContainer element, string message)
        {
            Assert.IsTrue(element.Displayed, message);
        }

        public static void IsDisappeared(IElementContainer element, string elementName, TimeSpan? timeout = null)
        {
            Assert.IsTrue(element.IsDisappeared(timeout), $"'{elementName}' still present");
        }

        public static void IsTextValid(IElement element, string message)
        {
            IsTrue(TestTextValidity(element, message));
        }

        public static void IsReadable(IElement element, string message, double readabilityLevel)
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

        private static ValidationResult TestTextValidity(IElement element, string message)
        {
            var path = element.Path;
            var displayed = element.Displayed;
            if (!displayed)
            {
                return Fail($"{message} | {path} is not displayed");
            }
            var erros = new List<string>();
            var text = element.Text;

            // not only whitespace
            if (string.IsNullOrWhiteSpace(text))
            {
                erros.Add($"Text is empty or whitespace only");
            }

            // not contain interpolation: {{var}}
            if (text.HasInterpolation())
            {
                erros.Add("Text has interpolation");
            }

            if (text.IsCrsKeyLike())
            {
                erros.Add("Text is like a CRS key");
            }

            if (text.HasHtmlTag())
            {
                erros.Add("Text has Html tag");
            }

            if (erros.Count > 0)
            {
                return Fail($"{message} | Path: {path} has following errors: {string.Join(Environment.NewLine, erros)} | {Environment.NewLine}"
                    + $"Original text: '{text}'");
            }

            return Pass();
        }

        private static ValidationResult TestReadability(Color color, Color backgroundColor, string message, double readabilityLevel)
        {
            if (readabilityLevel < 1)
            {
                return new ValidationResult { Message = "Not applicable", Passed = false };
            }

            if (!ColorUtil.IsReadable(color, backgroundColor, readabilityLevel))
            {

                return Fail($"{message} | {Environment.NewLine}"
                            + $"Contrast ratio ({ColorUtil.GetReadability(color, backgroundColor)}) of the given color pair is below the limit ({readabilityLevel}) | {Environment.NewLine}"
                            + $"Color: {ColorUtil.ToRgbString(color)}, {ColorUtil.ToHexString(color)} | {Environment.NewLine}"
                            + $"Background color: {ColorUtil.ToRgbString(backgroundColor)}, {ColorUtil.ToHexString(backgroundColor)}");
            }
            return Pass();
        }

        private static ValidationResult Fail(string message) => new ValidationResult { Message = message, Passed = false };

        private static ValidationResult Pass() => new ValidationResult { Message = "OK", Passed = true };
    }
}

