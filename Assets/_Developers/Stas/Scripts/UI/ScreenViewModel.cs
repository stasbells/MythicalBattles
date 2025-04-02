using R3;
using System;

namespace MythicalBattles
{
    public abstract class ScreenViewModel : IDisposable
    {
        public Observable<ScreenViewModel> CloseReqested => _closeReqested;
        public abstract string Name { get; }

        private readonly Subject<ScreenViewModel> _closeReqested = new();

        public void Close()
        {
            _closeReqested.OnNext(this);
        }

        public virtual void Dispose()
        {
            
        }
    }
}
