using Reflex.Core;

namespace MythicalBattles.Assets.Scripts.UI
{
    public abstract class UIManager
    {
        protected UIManager(ContainerBuilder container)
        {
            Container = container;
        }
        
        protected ContainerBuilder Container { get; }
    }
}
