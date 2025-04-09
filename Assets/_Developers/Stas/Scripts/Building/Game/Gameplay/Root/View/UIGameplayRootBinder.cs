using System;
using UnityEngine;
using R3;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View
{
    public class UIGameplayRootBinder : MonoBehaviour
    {
        private Subject<Unit> _exitSceneSignal;

        public void HandleGoToMainMenuButtonClicked()
        {
            _exitSceneSignal?.OnNext(Unit.Default);
        }

        public void Bind(Subject<Unit> exitSceneSignal)
        {
            _exitSceneSignal = exitSceneSignal;
        }
    }
}
