using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Game.Utils.UI
{
    public class WindowsContainer : MonoBehaviour
    {
        [SerializeField] private Transform _screensContainer;
        [SerializeField] private Transform _popupsContainer;

        [Inject] private readonly DiContainer _sceneDIContainer;

        private readonly Dictionary<WindowViewModel, IWindowView> _openedPopupBinders = new();
        private IWindowView _openedScreenBinder;
        private DiContainer _screenSubDIContainer;

        public void OpenPopup(WindowViewModel viewModel)
        {
            var prefabPath = GetPrefabPath(viewModel);
            var prefab = Resources.Load<GameObject>(prefabPath);
            var createdPopup = Instantiate(prefab, _popupsContainer);
            var binder = createdPopup.GetComponent<IWindowView>();

            binder.Bind(viewModel);
            _openedPopupBinders.Add(viewModel, binder);
        }

        public void ClosePopup(WindowViewModel popupViewModel)
        {
            var binder = _openedPopupBinders[popupViewModel];

            binder?.Close();
            _openedPopupBinders.Remove(popupViewModel);
        }

        public void OpenScreen(WindowViewModel viewModel)
        {
            if (viewModel == null)
            {
                return;
            }

            _openedScreenBinder?.Close();
            _screenSubDIContainer?.UnbindAll();

            var prefabPath = GetPrefabPath(viewModel);
            var prefab = Resources.Load<GameObject>(prefabPath);

            _screenSubDIContainer = _sceneDIContainer.CreateSubContainer();
            _screenSubDIContainer.Bind<IWindowView>().FromComponentInNewPrefab(prefab).AsSingle();

            var createdScreen = _screenSubDIContainer.Resolve<IWindowView>();
            createdScreen.Transform.SetParent(_screensContainer, false);

            var binder = createdScreen.Transform.GetComponent<IWindowView>();


            binder.Bind(viewModel);
            _openedScreenBinder = binder;
        }

        private static string GetPrefabPath(WindowViewModel viewModel)
        {
            return $"UI/{viewModel.Id}";
        }
    }
}