
using _Game.Utils.UI;
using R3;
using System;
using UnityEngine;

namespace _Game.Gameplay.UI
{
    public class ScreenGameplayViewModel : WindowViewModel
    {
        public override string Id => "ScreenGameplay";

        private readonly ReactiveProperty<string> _crystalsAmount = new("0");
        public Observable<string> CrystalsAmount => _crystalsAmount;

        private GameplayUIManager _uiManager;

        public ScreenGameplayViewModel(GameplayUIManager uiManager)
        {
            _uiManager = uiManager;
            var crystalsManager = GameObject.FindAnyObjectByType<CrystalsManager>();
            crystalsManager.Storage.Subscribe(amount => _crystalsAmount.OnNext(amount.ToString()));
        }

        internal void OpenSettings()
        {
            _uiManager.OpenPopupSettings();
        }

        internal void OpenShop()
        {
            _uiManager.OpenPopupShop();
        }
    }
}