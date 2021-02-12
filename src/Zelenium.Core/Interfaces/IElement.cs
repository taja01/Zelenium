using System.Drawing;

namespace Zelenium.Core.Interfaces
{
    public interface IElement : IElementContainer
    {
        public string Text { get; }
        public Color Color { get; }
    }
}
