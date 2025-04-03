using System;
using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View
{
    class UIGameplayRootBinder : MonoBehaviour
    {
        public event Action GoToMainMenuButtonClicked;

        public void HandleGoToMainMenuButtonClicked()
        {
            GoToMainMenuButtonClicked?.Invoke();
        }
    }
}
