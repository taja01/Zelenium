using Zelenium.Core.WebDriver.Types;

namespace Zelenium.Core.Interfaces
{
    interface IDualContainer<out T1, out T2>
        where T1 : AbstractContainer
        where T2 : AbstractContainer
    {
        T1 Desktop();
        T2 Mobile();
    }
}
