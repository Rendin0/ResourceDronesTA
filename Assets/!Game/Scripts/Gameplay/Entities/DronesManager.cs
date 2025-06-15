using System.Collections.Generic;
using UnityEngine;

public class DronesManager : MonoBehaviour
{
    [SerializeField] private CrystalsManager _crystalsManager;

    public List<Drone> Drones0 { get; } = new();
    public List<Drone> Drones1 { get; } = new();
    public List<Drone> Drones2 { get; } = new();
    public List<Drone> Drones3 { get; } = new();

    [SerializeField] private Drone _drone0Prefab;
    [SerializeField] private Drone _drone1Prefab;
    [SerializeField] private Drone _drone2Prefab;
    [SerializeField] private Drone _drone3Prefab;

    private void Awake()
    {
        for (int i = 0; i < SaveManager.Instance.Data.Drones[0]; i++)
        {
            CreateDrone(0);
        }
        for (int i = 0; i < SaveManager.Instance.Data.Drones[1]; i++)
        {
            CreateDrone(1);
        }
        for (int i = 0; i < SaveManager.Instance.Data.Drones[2]; i++)
        {
            CreateDrone(2);
        }
        for (int i = 0; i < SaveManager.Instance.Data.Drones[3]; i++)
        {
            CreateDrone(3);
        }
    }

    public void CreateDrone(uint tier)
    {
        var drone = tier switch
        {
            0 => Instantiate(_drone0Prefab, transform),
            1 => Instantiate(_drone1Prefab, transform),
            2 => Instantiate(_drone2Prefab, transform),
            3 => Instantiate(_drone3Prefab, transform),
            _ => throw new System.ArgumentOutOfRangeException(nameof(tier), "Invalid drone tier")
        };
        drone.Init(_crystalsManager);

        switch (tier)
        {
            case 0:
                Drones0.Add(drone);
                break;
            case 1:
                Drones1.Add(drone);
                break;
            case 2:
                Drones2.Add(drone);
                break;
            case 3:
                Drones3.Add(drone);
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        SaveManager.Instance.Data.Drones[0] = Drones0.Count;
        SaveManager.Instance.Data.Drones[1] = Drones1.Count;
        SaveManager.Instance.Data.Drones[2] = Drones2.Count;
        SaveManager.Instance.Data.Drones[3] = Drones3.Count;

        SaveManager.Instance.Data.Localisation = LocalisationManager.Instance.CurrentLocalisation.Value;

        SaveManager.Instance.Save();
    }

}