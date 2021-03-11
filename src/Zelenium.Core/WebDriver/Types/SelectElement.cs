using System.Collections.Generic;
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
                throw new System.IndexOutOfRangeException($"Index was out of range. Must be non-negative and less than the size of the collection. Index: {index}, Max index: {this.Count} Path: {this.Path}");
            }
        }

        /// <summary>
        /// Set by translated value
        /// </summary>
        /// <param name="text"></param>
        public void SetByText(string text)
        {
            this.selectElement.SelectByText(text);
        }

        /// <summary>
        /// Set by value attribute
        /// </summary>
        /// <param name="text"></param>
        public void SetByValue(string text)
        {
            this.selectElement.SelectByValue(text);
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
