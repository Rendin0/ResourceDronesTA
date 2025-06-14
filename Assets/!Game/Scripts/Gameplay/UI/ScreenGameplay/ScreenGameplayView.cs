using _Game.Utils.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Gameplay.UI
{
    public class ScreenGameplayView : WindowView<ScreenGameplayViewModel>
    {
        [SerializeField] private Button _closeAppButton;


        private void OnEnable()
        {
            _closeAppButton.onClick.AddListener(OnCloseAppButtonClicked);
        }

        private void OnCloseAppButtonClicked()
        {
            Application.Quit();
        }

        private void OnDisable()
        {
            _closeAppButton.onClick.RemoveAllListeners();
        }


    }
}