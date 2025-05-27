using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameComplete
{
    public class ScreenGameCompleteViewModel : ScreenViewModel
    {
        private readonly Subject<Unit> _exitSceneRequest;
        
        public ScreenGameCompleteViewModel(Subject<Unit> exitSceneRequest)
        {
            _exitSceneRequest = exitSceneRequest;
        }
        
        public override string Name => "ScreenGameComplete";
        
        public void RequestGoToMainMenu()
        {
            _exitSceneRequest.OnNext(Unit.Default);
        }
    }
}
