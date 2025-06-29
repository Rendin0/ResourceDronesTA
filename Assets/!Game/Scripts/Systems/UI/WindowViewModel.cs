using R3;
using System;

namespace _Game.Utils.UI
{
    public abstract class WindowViewModel : IDisposable
    {
        public Observable<WindowViewModel> CloseRequested => _closeRequested;
        private readonly Subject<WindowViewModel> _closeRequested = new();

        public abstract string Id { get; }

        public void RequestClose()
        {
            OnClose();
            _closeRequested.OnNext(this);
        }

        public virtual void Dispose() { }

        protected virtual void OnClose() { }
    }
}