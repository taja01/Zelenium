namespace Zelenium.Core.Interfaces
{
    public interface IRouteBuilder<TEnum>
    {
        public string GetUrl(TEnum page);
    }
}
