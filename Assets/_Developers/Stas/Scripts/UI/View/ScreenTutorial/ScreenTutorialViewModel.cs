using System.Collections;
using System.Collections.Generic;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenMainMenu;
using R3;
using UnityEngine;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenTutorial
{
    public class ScreenTutorialViewModel : ScreenViewModel
    {
        private readonly Subject<Unit> _exitSceneRequest;
        
        
        public ScreenTutorialViewModel(Subject<Unit> exitSceneRequest)
        {
            _exitSceneRequest = exitSceneRequest;
        }
        
        public override string Name => "ScreenTutorial";

        public void RequestGoToSceneGameplay()
        {
            _exitSceneRequest.OnNext(Unit.Default);
        }
    }
}
