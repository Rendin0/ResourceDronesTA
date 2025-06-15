using _Game.Gameplay.UI;
using _Game.Utils.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using R3;

public class VScreenGameplayView : WindowView<VScreenGameplayViewModel>
{
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private TMP_Text _crystalsText;

    private void OnEnable()
    {
        _shopButton.onClick.AddListener(OnShopButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
    }
    private void OnDisable()
    {
        _shopButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
    }

    protected override void OnBind(VScreenGameplayViewModel viewModel)
    {
        viewModel.CrystalsAmount.Subscribe(amount => _crystalsText.text = amount);
    }

    private void OnSettingsButtonClicked()
    {
        ViewModel.OpenSettings();
    }

    private void OnShopButtonClicked()
    {
        ViewModel.OpenShop();
    }
}