using _Game.Utils.UI;
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
        }
        #endregion

        public void OpenScreenGameplay()
        {
            var viewModel = new ScreenGameplayViewModel();
            _sceneUI.OpenScreen(viewModel);
        }
    }
}