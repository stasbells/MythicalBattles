using System;
using R3;

namespace MythicalBattles.Assets.Scripts.UI
{
    public abstract class ScreenViewModel : IDisposable
    {
        private readonly Subject<ScreenViewModel> _closeRequested = new ();

        public Observable<ScreenViewModel> CloseRequested => _closeRequested;
        public abstract string Name { get; }

        public void Close()
        {
            _closeRequested.OnNext(this);
        }

        public virtual void Dispose() { }
    }
}