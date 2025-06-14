using System.Collections.Generic;
using UnityEngine;

public class DronesManager : MonoBehaviour
{
    [SerializeField] private CrystalsManager _crystalsManager;

    private readonly List<Drone> _drones = new();

    [SerializeField] private Drone _drone0Prefab;
    [SerializeField] private Drone _drone1Prefab;
    [SerializeField] private Drone _drone2Prefab;
    [SerializeField] private Drone _drone3Prefab;

    private void Start()
    {
        CreateDrone(0);
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
    }

}