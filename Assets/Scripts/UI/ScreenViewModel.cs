using R3;
using System;

namespace MythicalBattles.Assets.Scripts.UI
{
    public abstract class ScreenViewModel : IDisposable
    {
        private readonly Subject<ScreenViewModel> _closeReqested = new();

        public Observable<ScreenViewModel> CloseReqested => _closeReqested;
        public abstract string Name { get; }

        public void Close()
        {
            _closeReqested.OnNext(this);
        }

        public virtual void Dispose() { }
    }
}