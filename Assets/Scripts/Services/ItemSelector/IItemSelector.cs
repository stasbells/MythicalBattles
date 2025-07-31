using System;

namespace MythicalBattles.Assets.Scripts.Services.ItemSelector
{
    public interface IItemSelector
    {
        public event Action SelectedItemChanged;
    }
}
