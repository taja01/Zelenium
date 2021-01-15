namespace ZeleniumFramework.WebDriver.Interfaces
{
    public interface IRouteBuilder<T>
    {
        string GetUrl(T page);
    }
}
