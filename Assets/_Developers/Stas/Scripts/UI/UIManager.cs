using Reflex.Core;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI
{
    public abstract class UIManager
    {
        private readonly ContainerBuilder _container;

        public ContainerBuilder Container => _container;

        protected UIManager(ContainerBuilder container)
        {
            _container = container;
        }
    }
}
