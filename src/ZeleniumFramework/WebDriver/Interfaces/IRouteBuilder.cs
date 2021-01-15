namespace ZeleniumFramework.WebDriver.Interfaces
{
    public interface IRouteBuilder<TEnum>
    {
        public string GetUrl(TEnum page);
    }
}
