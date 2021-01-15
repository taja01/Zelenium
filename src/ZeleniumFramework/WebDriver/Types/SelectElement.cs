using System.Collections.Generic;
using OpenQA.Selenium;

namespace ZeleniumFramework.WebDriver.Types
{
    public class SelectElement : Element
    {
        public SelectElement(IWebDriver webDriver, By by = null) : base(webDriver, by)
        {

        }
        OpenQA.Selenium.Support.UI.SelectElement selectElement => new OpenQA.Selenium.Support.UI.SelectElement(this.WebElement);

        public void DeselectAll()
        {
            this.selectElement.DeselectAll();
        }

        public void SelectByIndex(int index)
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

        public void SelectByText(string text)
        {
            this.selectElement.SelectByText(text);
        }

        public void SelectByValue(string text)
        {
            this.selectElement.SelectByValue(text);
        }

        public string SelectedText()
        {
            return this.selectElement.SelectedOption.Text;
        }

        public IList<string> GetAllOptions()
        {
            var list = new List<string>();
            using var all = this.selectElement.Options.GetEnumerator();
            while (all.MoveNext())
            {
                list.Add(all.Current.Text);
            }

            return list;
        }

        public IList<string> GetSelectedOptions()
        {
            var list = new List<string>();
            using var all = this.selectElement.AllSelectedOptions.GetEnumerator();
            while (all.MoveNext())
            {
                list.Add(all.Current.Text);
            }

            return list;
        }

        public int CurrentIndex()
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

        public int Count => this.selectElement.Options.Count;
    }
}
