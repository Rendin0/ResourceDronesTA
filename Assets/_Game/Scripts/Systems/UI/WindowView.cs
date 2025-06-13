using UnityEngine;

namespace _Game.Utils.UI
{
    public abstract class WindowView<T> : MonoBehaviour, IWindowView where T : WindowViewModel
    {
        protected T ViewModel;
        public Transform Transform => transform;

        public void Bind(WindowViewModel viewModel)
        {
            ViewModel = (T)viewModel;

            OnBind(ViewModel);
        }

        public void Close()
        {
            BeforeClose();

            Destroy(gameObject);
        }

        protected virtual void BeforeClose() { }
        protected virtual void OnBind(T viewModel) { }

    }
}