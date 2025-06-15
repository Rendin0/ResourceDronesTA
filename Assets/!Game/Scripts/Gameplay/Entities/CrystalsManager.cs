using R3;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrystalsManager : MonoBehaviour
{
    private readonly List<Crystal> _crystals0 = new();
    private readonly List<Crystal> _crystals1 = new();
    private readonly List<Crystal> _crystals2 = new();
    private readonly List<Crystal> _crystals3 = new();
    public ReactiveProperty<float> Storage { get; set; } = new();

    private void Awake()
    {
        Storage.Value = SaveManager.Instance.Data.Crystals;

        var crystals = GetComponentsInChildren<Crystal>();
        foreach (var crystal in crystals)
        {
            switch (crystal.Tier)
            {
                case 0:
                    _crystals0.Add(crystal);
                    break;
                case 1:
                    _crystals1.Add(crystal);
                    break;
                case 2:
                    _crystals2.Add(crystal);
                    break;
                case 3:
                    _crystals3.Add(crystal);
                    break;
                default:
                    Debug.LogWarning($"Crystal with unsupported tier {crystal.Tier} found in {gameObject.name}.", crystal);
                    break;
            }
        }
        SortCrtystalsByDistance();
    }

    private void SortCrtystalsByDistance()
    {
        SortByDistance(_crystals0);
        SortByDistance(_crystals1);
        SortByDistance(_crystals2);
        SortByDistance(_crystals3);
    }

    private void SortByDistance(List<Crystal> crystals)
    {
        crystals.Sort((a, b) => Vector3.Distance(a.transform.position, transform.position)
            .CompareTo(Vector3.Distance(b.transform.position, transform.position)));
    }

    public Crystal FindNearestFreeCrystal(Drone drone)
    {
        var crystals = drone.Tier switch
        {
            0 => _crystals0,
            1 => _crystals1.Concat(_crystals0),
            2 => _crystals2.Concat(_crystals1).Concat(_crystals0),
            3 => _crystals3.Concat(_crystals2).Concat(_crystals1).Concat(_crystals0),
            _ => null
        };

        var crystal = crystals?.FirstOrDefault(crystal => crystal.IsFree);
        if (crystal != null)
            crystal.IsFree = false;

        return crystal;
    }

    private void OnDestroy()
    {
        SaveManager.Instance.Data.Crystals = Storage.Value;
        SaveManager.Instance.Save();
    }
}