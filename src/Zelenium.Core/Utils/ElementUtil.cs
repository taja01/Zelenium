using System;
using System.Linq;
using OpenQA.Selenium;
using Zelenium.Core.Config;
using Zelenium.Core.Enums;
using Zelenium.Core.WebDriver;
using Zelenium.Core.Interfaces;
using Zelenium.Shared;

namespace Zelenium.Core.Utils
{
    public class ElementUtil
    {
        public static Shown ShownElement(IElementContainer first, IElementContainer second, IElementContainer third = null, IElementContainer fourth = null, TimeSpan? timeout = null)
        {
            try
            {
                return ShownElementOrError(first, second, third, fourth, timeout);
            }
            catch (WebDriverTimeoutException)
            {
                return Shown.None;
            }
        }

        public static Shown ShownElementOrError(IElementContainer first, IElementContainer second, IElementContainer third = null, IElementContainer fourth = null, TimeSpan? timeout = null)
        {
            var shownElement = Shown.None;

            Wait.Initialize()
                .Message($"None of the elements can be found: {first.Finder.Path} OR {second.Finder.Path}")
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Until(() =>
                {
                    if (first.PresentNow)
                    {
                        shownElement = Shown.FirstElement;
                        return true;
                    }

                    if (second.PresentNow)
                    {
                        shownElement = Shown.SecondElement;
                        return true;
                    }

                    if (third != null && third.PresentNow)
                    {
                        shownElement = Shown.ThirdElement;
                        return true;
                    }

                    if (fourth != null && fourth.PresentNow)
                    {
                        shownElement = Shown.FourthElement;
                        return true;
                    }

                    return false;
                });

            return shownElement;
        }

        /// <summary>
        /// Iterates over Element pairs.
        /// If the first Element of the pair is present,
        /// then returns the second part of the pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elementPairs"></param>
        /// <returns></returns>
        public static T GetShowChild<T>((IElementContainer, T)[] elementPairs)
        {
            var paths = elementPairs.Select(ep => ep.Item1).Select(e => e.Finder.Path).Aggregate((i, j) => $"{i},  {j}");
            var index = -1;
            Wait.Initialize()
                .Message($"None of the elements can be found: {paths}")
                .Until(() =>
                {
                    index = elementPairs
                         .Select(ep => ep.Item1).ToList()
                         .FindIndex(e => e.PresentNow);
                    return index != -1;
                });
            return elementPairs[index].Item2;
        }

        public static object Locators(object desktopSelector, object mobileSelector, Device device)
        {
            return device == Device.Desktop ? desktopSelector : mobileSelector;
        }
    }
}
