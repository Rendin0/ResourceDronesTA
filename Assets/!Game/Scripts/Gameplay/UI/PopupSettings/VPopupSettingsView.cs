
using _Game.Utils.UI;
using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VPopupSettingsView : PopupView<VPopupSettingsViewModel>
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private TMP_Dropdown _windowPositionDropdown;

    [SerializeField] private Button _bottomButton;
    [SerializeField] private Button _topButton;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    [SerializeField] private TMP_Dropdown _languageDropdown;

    [SerializeField] private Slider _musicVolumeSlider;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = FindAnyObjectByType<AudioSource>();

        _exitButton.onClick.AddListener(OnExitButtonClicked);
        //_windowPositionDropdown.OnValueChangedAsObservable().Skip(1).Subscribe(value =>
        //    {
        //        WindowPosition newPosition = (WindowPosition)value;
        //        WindowOptions.Instance.Position.OnNext(newPosition);
        //        ViewModel.RequestClose();
        //    })
        //    .AddTo(this);

        _bottomButton.onClick.AddListener(() => ChangeWindowPos(WindowPosition.Bottom));
        _topButton.onClick.AddListener(() => ChangeWindowPos(WindowPosition.Top));
        _leftButton.onClick.AddListener(() => ChangeWindowPos(WindowPosition.Left));
        _rightButton.onClick.AddListener(() => ChangeWindowPos(WindowPosition.Right));

        _languageDropdown.value = (int)LocalisationManager.Instance.CurrentLocalisation.Value;
        _languageDropdown.onValueChanged.AddListener(OnLanguageChanged);

        _musicVolumeSlider.value = SaveManager.Instance.Data.Volume;
        _musicVolumeSlider.onValueChanged.AddListener(value =>
        {
            _audioSource.volume = value;
            SaveManager.Instance.Data.Volume = value;
        });
    }

    private void OnLanguageChanged(int languageId)
    {
        Localisation newLanguage = (Localisation)languageId;
        LocalisationManager.Instance.CurrentLocalisation.OnNext(newLanguage);
    }

    private void ChangeWindowPos(WindowPosition position)
    {
        WindowOptions.Instance.Position.OnNext(position);
        ViewModel.RequestClose();
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
