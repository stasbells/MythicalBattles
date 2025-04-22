using Reflex.Core;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI
{
    public abstract class UIManager
    {
        protected readonly ContainerBuilder Container;

        protected UIManager(ContainerBuilder container)
        {
            Container = container;
        }
    }
}
