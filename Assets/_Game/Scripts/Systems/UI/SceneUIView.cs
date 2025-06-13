using ObservableCollections;
using R3;
using UnityEngine;
using Zenject;

namespace _Game.Utils.UI
{
    public class SceneUIView<T> : MonoBehaviour where T : SceneUIViewModel
    {
        [SerializeField] private WindowsContainer _windowsContainer;

        private readonly CompositeDisposable _subscriptions = new();

        [Inject] private readonly UIRootView _uiRoot;
        [Inject] private readonly T _viewModel;

        private void Start()
        {
            _uiRoot.AttachSceneUI(gameObject);
            Bind(_viewModel);
        }

        public void Bind(SceneUIViewModel viewModel)
        {
            _subscriptions.Add(viewModel.OpenedScreen.Subscribe(newScreenViewModel =>
            {
                _windowsContainer.OpenScreen(newScreenViewModel);
            }));

            foreach (var openedPopup in viewModel.OpenedPopups)
            {
                _windowsContainer.OpenPopup(openedPopup);
            }


            _subscriptions.Add(viewModel.OpenedPopups.ObserveAdd().Subscribe(e =>
            {
                _windowsContainer.OpenPopup(e.Value);
            }));

            _subscriptions.Add(viewModel.OpenedPopups.ObserveRemove().Subscribe(e =>
            {
                _windowsContainer.ClosePopup(e.Value);
            }));

            OnBind(viewModel);
        }

        protected virtual void OnBind(SceneUIViewModel rootViewModel) { }

        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }

    }
}