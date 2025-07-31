using System;

namespace MythicalBattles.Services.ItemSelector
{
    public interface IItemSelector
    {
        public event Action SelectedItemChanged;
    }
}
