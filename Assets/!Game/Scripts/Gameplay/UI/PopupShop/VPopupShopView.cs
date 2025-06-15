using _Game.Utils.UI;
using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VPopupShopView : PopupView<VPopupShopViewModel>
{
    [SerializeField] private Button _drone0Button;
    [SerializeField] private Button _drone1Button;
    [SerializeField] private Button _drone2Button;
    [SerializeField] private Button _drone3Button;


    [SerializeField] private TMP_Text _drone0Amount;
    [SerializeField] private TMP_Text _drone1Amount;
    [SerializeField] private TMP_Text _drone2Amount;
    [SerializeField] private TMP_Text _drone3Amount;


    [SerializeField] private float _drone0Price;
    [SerializeField] private float _drone1Price;
    [SerializeField] private float _drone2Price;
    [SerializeField] private float _drone3Price;

    [SerializeField] private TMP_Text _drone0PriceText;
    [SerializeField] private TMP_Text _drone1PriceText;
    [SerializeField] private TMP_Text _drone2PriceText;
    [SerializeField] private TMP_Text _drone3PriceText;

    private DronesManager _dronesManager;
    private CrystalsManager _crystalsManager;

    private void OnEnable()
    {
        _dronesManager = FindAnyObjectByType<DronesManager>();
        _crystalsManager = FindAnyObjectByType<CrystalsManager>();

        _drone0Button.onClick.AddListener(() => BuyDrone(0));
        _drone1Button.onClick.AddListener(() => BuyDrone(1));
        _drone2Button.onClick.AddListener(() => BuyDrone(2));
        _drone3Button.onClick.AddListener(() => BuyDrone(3));


        _drone0PriceText.text = _drone0Price.ToString();
        _drone1PriceText.text = _drone1Price.ToString();
        _drone2PriceText.text = _drone2Price.ToString();
        _drone3PriceText.text = _drone3Price.ToString();
    }

    private void OnDisable()
    {
        _drone0Button.onClick.RemoveAllListeners();
        _drone1Button.onClick.RemoveAllListeners();
        _drone2Button.onClick.RemoveAllListeners();
        _drone3Button.onClick.RemoveAllListeners();
    }

    private void BuyDrone(uint tier)
    {
        float dronePrice = tier switch
        {
            0 => _drone0Price,
            1 => _drone1Price,
            2 => _drone2Price,
            3 => _drone3Price,
            _ => 0f
        };

        if (_crystalsManager.Storage.Value < dronePrice)
            return;

        _crystalsManager.Storage.Value -= dronePrice;
        _dronesManager.CreateDrone(tier);
        UpdateDronesAmount();
    }

    private void UpdateDronesAmount()
    {
        _drone0Amount.text = _dronesManager.Drones0.Count.ToString();
        _drone1Amount.text = _dronesManager.Drones1.Count.ToString();
        _drone2Amount.text = _dronesManager.Drones2.Count.ToString();
        _drone3Amount.text = _dronesManager.Drones3.Count.ToString();
    }

}
