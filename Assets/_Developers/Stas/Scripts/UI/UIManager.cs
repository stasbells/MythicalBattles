using Reflex.Core;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI
{
    public abstract class UIManager
    {
        protected readonly ContainerBuilder _builder;

        protected UIManager(ContainerBuilder builder)
        {
            _builder = builder;
        }
    }
}
