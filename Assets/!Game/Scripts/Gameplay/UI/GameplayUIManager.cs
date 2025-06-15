using _Game.Utils.UI;
using R3;
using System;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.UI
{
    public class GameplayUIManager : IInitializable
    {
        [Inject] private readonly GameplaySceneUIViewModel _sceneUI;
    
        #region Zenject
        public void Initialize()
        {
            OpenScreenGameplay();

            WindowOptions.Instance.Position.Skip(1).Subscribe(_ => OpenScreenGameplay());
        }
        #endregion

        public void OpenScreenGameplay()
        {
            WindowViewModel viewModel = WindowOptions.Instance.Position.Value switch
            {
                WindowPosition.Left => new VScreenGameplayViewModel(this),
                WindowPosition.Right => new VScreenGameplayViewModel(this),
                WindowPosition.Top => new ScreenGameplayViewModel(this),
                WindowPosition.Bottom => new ScreenGameplayViewModel(this),

                _ => throw new ArgumentOutOfRangeException()
            };
            _sceneUI.OpenScreen(viewModel);
        }

        internal void OpenPopupSettings()
        {
            WindowViewModel viewModel = WindowOptions.Instance.Position.Value switch
            {
                WindowPosition.Left => new VPopupSettingsViewModel(),
                WindowPosition.Right => new VPopupSettingsViewModel(),
                WindowPosition.Top => new PopupSettingsViewModel(),
                WindowPosition.Bottom => new PopupSettingsViewModel(),

                _ => throw new ArgumentOutOfRangeException()
            };
            _sceneUI.OpenPopup(viewModel);
        }

        internal void OpenPopupShop()
        {
            WindowViewModel viewModel = WindowOptions.Instance.Position.Value switch
            {
                WindowPosition.Left => new VPopupShopViewModel(),
                WindowPosition.Right => new VPopupShopViewModel(),
                WindowPosition.Top => new PopupShopViewModel(),
                WindowPosition.Bottom => new PopupShopViewModel(),

                _ => throw new ArgumentOutOfRangeException()
            };
            _sceneUI.OpenPopup(viewModel);
        }
    }
}