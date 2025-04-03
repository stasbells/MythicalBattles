using System;
using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View
{
    class UIMainMenuRootBinder : MonoBehaviour
    {
        public event Action GoToGameplayButtonClicked;

        public void HandleGoToGameplayButtonClicked()
        {
            GoToGameplayButtonClicked?.Invoke();
        }
    }
}
