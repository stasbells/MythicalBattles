using R3;
using System;

namespace MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils
{
    public class Signal
    {
        private Subject<Unit> _exitSceneRequest = new ();
        private Subject<Unit> _restartSceneRequest = new();

        public Subject<Unit> ExitSceneRequest => _exitSceneRequest;
        public Subject<Unit> RestartSceneRequest => _restartSceneRequest;
    }
}