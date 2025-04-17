using R3;
using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Game.Gameplay.Root.View
{
    class UIMainMenuRootBinder : MonoBehaviour
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
