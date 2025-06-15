
using _Game.Gameplay.UI;
using _Game.Utils.UI;
using R3;
using UnityEngine;

public class VScreenGameplayViewModel : WindowViewModel
{
    public override string Id => "VScreenGameplay";

    private readonly ReactiveProperty<string> _crystalsAmount = new("0");
    public Observable<string> CrystalsAmount => _crystalsAmount;

    private GameplayUIManager _uiManager;

    public VScreenGameplayViewModel(GameplayUIManager uiManager)
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