namespace Zelenium.Core.WebDriver.Interfaces
{
    public interface IRouteBuilder<TEnum>
    {
        public string GetUrl(TEnum page);
    }
}
