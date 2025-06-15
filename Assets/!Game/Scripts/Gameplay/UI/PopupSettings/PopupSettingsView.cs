
using _Game.Utils.UI;
using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupSettingsView : PopupView<PopupSettingsViewModel>
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private TMP_Dropdown _windowPositionDropdown;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButtonClicked);
        _windowPositionDropdown.OnValueChangedAsObservable().Skip(1).Subscribe(value =>
            {
                WindowPosition newPosition = (WindowPosition)value;
                WindowOptions.Instance.Position.OnNext(newPosition);
                ViewModel.RequestClose();
            })
            .AddTo(this);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveAllListeners();
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }
}