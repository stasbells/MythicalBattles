using System;

namespace MythicalBattles
{
    public interface IItemSelector
    {
        public event Action SelectedItemChanged;
    }
}
