using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using TestPage.Pages.Components;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace TestPage.Pages
{
    public class MainPage(ILogger logger, IWebDriver webDriver) : AbstractLoadableContainer(logger, webDriver, By.CssSelector(".container"), "https://taja01.github.io/testpage/index.html")
    {
        public NavBar NavBar = new(logger, webDriver, By.CssSelector(".navbar"));
        public Element Title => this.Find<Element>(By.CssSelector("h1"));
        public ButtonSection ButtonSection => this.Find<ButtonSection>(By.CssSelector("[data-testid='buttons']"));
        public CheckBoxSection CheckBoxSection => this.Find<CheckBoxSection>(locator: By.CssSelector("[data-testid='checkboxes']"));
        public InputFieldsSection InputFieldsSection => this.Find<InputFieldsSection>(locator: By.CssSelector("[data-testid='input-fields']"));
        public RadioButtonContainer RadioButtonsSection => this.Find<RadioButtonContainer>(locator: By.CssSelector("[data-testid='radio-buttons']"));
        public ComboboxSection ComboboxSection => this.Find<ComboboxSection>(By.CssSelector("[data-testid='combobox-/-dropdown']"));
        public ContrastsSection ContrastsSection => this.Find<ContrastsSection>(By.CssSelector("[data-testid='text-with-different-background']"));

        public override ValidationResult IsLoaded()
        {
            (Func<bool> Condition, string Description)[] checks =
                [
                (() => this.Displayed, "Container not loaded"),
                (() => this.Title.Displayed, $"{nameof(this.Title)} not loaded"),
                (() => this.ButtonSection.Displayed , $"{nameof(this.ButtonSection)} not loaded"),
                (() => this.CheckBoxSection.Displayed, $"{nameof(this.CheckBoxSection)} not loaded"),
                (() => this.InputFieldsSection.Displayed, $"{nameof(this.InputFieldsSection)} not loaded"),
                (() => this.ContrastsSection.Displayed, $"{nameof(this.ContrastsSection)} not loaded")
                ];

            return base.CheckAllLoaded(checks);
        }
    }
}
