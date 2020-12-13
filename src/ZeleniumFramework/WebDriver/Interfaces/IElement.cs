using System.Drawing;

namespace ZeleniumFramework.WebDriver.Interfaces
{
    public interface IElement : IElementContainer
    {
        public string Text { get; }
        public Color Color { get; }
    }
}
