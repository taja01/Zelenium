using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Zelenium.Core.WebDriver.Types
{
    public class SelectElement : Element
    {
        private const string Attribute = "value";
        public SelectElement(IWebDriver webDriver, By by = null) : base(webDriver, by)
        {

        }

        private OpenQA.Selenium.Support.UI.SelectElement selectElement => new OpenQA.Selenium.Support.UI.SelectElement(this.WebElement);

        /// <summary>
        /// Deselect all options
        /// </summary>
        public void DeselectAll()
        {
            this.selectElement.DeselectAll();
        }

        /// <summary>
        /// Set by Index
        /// </summary>
        /// <param name="index"></param>
        public void SetByIndex(int index)
        {
            if (index >= 0 || index < this.Count)
            {
                this.selectElement.SelectByIndex(index);
            }
            else
            {
                throw new IndexOutOfRangeException($"Index was out of range. Must be non-negative and less than the size of the collection. Index: {index}, Max index: {this.Count} Path: {this.Path}");
            }
        }

        /// <summary>
        /// Set by translated value
        /// </summary>
        /// <param name="text"></param>
        public void SetByText(string text)
        {
            Wait.Initialize()
                .Message($"Text '{text}' not exist in options")
                .Until(() => this.selectElement.Options.FirstOrDefault(x => x.Text == text));

            this.selectElement.SelectByText(text);

            var currentText = this.SelectedText.Trim();
            if (!currentText.Equals(text.Trim()))
            {
                throw new Exception($"'{nameof(this.SetByText)}' failed to set value to '{text}', it was: '{currentText}'");
            }
        }

        /// <summary>
        /// Set by value attribute
        /// </summary>
        /// <param name="value"></param>
        public void SetByValue(string value)
        {
            Wait.Initialize()
               .Message($"Value '{value}' not exist in options")
               .Until(() => this.selectElement.Options.FirstOrDefault(x => x.GetAttribute("value") == value));

            this.selectElement.SelectByValue(value);

            var currentValue = this.SelectedValue.Trim();
            if (!currentValue.Equals(value.Trim()))
            {
                throw new Exception($"'{nameof(this.SetByValue)}' failed to set value to '{value}', it was: '{currentValue}'");
            }
        }

        /// <summary>
        /// Return selected text
        /// </summary>
        public string SelectedText => this.selectElement.SelectedOption.Text;

        /// <summary>
        /// Return selected value
        /// </summary>
        public string SelectedValue => this.selectElement.SelectedOption.GetAttribute(Attribute);

        /// <summary>
        /// Return all options displayed text
        /// </summary>
        /// <returns>List of strings</returns>
        public IDictionary<string, string> GetAllOptions()
        {
            var dict = new Dictionary<string, string>();
            using var all = this.selectElement.Options.GetEnumerator();
            while (all.MoveNext())
            {
                dict.Add(all.Current.GetAttribute(Attribute), all.Current.Text);
            }

            return dict;
        }

        /// <summary>
        /// Return list of selected texts
        /// </summary>
        /// <returns>List of strings</returns>
        public IList<string> GetSelectedOptionsTexts()
        {
            var list = new List<string>();
            using var all = this.selectElement.AllSelectedOptions.GetEnumerator();
            while (all.MoveNext())
            {
                list.Add(all.Current.Text);
            }

            return list;
        }

        /// <summary>
        /// Return list of option values
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns>List of strings</returns>
        public IList<string> GetSelectedOptionsValues()
        {
            var list = new List<string>();
            using var all = this.selectElement.AllSelectedOptions.GetEnumerator();
            while (all.MoveNext())
            {
                list.Add(all.Current.GetAttribute(Attribute));
            }

            return list;
        }

        /// <summary>
        /// Return current index of selected options
        /// </summary>
        /// <returns>int</returns>
        public int GetCurrentIndex()
        {
            var index = 0;
            using var all = this.selectElement.Options.GetEnumerator();

            while (all.MoveNext())
            {
                if (all.Current.Selected)
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        /// <summary>
        /// Return number of number of Options
        /// </summary>
        public int Count => this.selectElement.Options.Count;
    }
}
