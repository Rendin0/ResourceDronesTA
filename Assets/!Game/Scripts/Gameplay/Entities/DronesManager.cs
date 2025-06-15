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

}