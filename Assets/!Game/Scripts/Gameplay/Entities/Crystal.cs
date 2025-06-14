using UnityEngine;

public class Crystal : MonoBehaviour
{
    [field: SerializeField] public uint Tier { get; private set; }
    [field: SerializeField, Min(0f)] public float SpeedModifier { get; private set; } = 1f;
    [field: SerializeField, Min(0f)] public float ResourcesPerTick { get; private set; }
    public bool IsFree { get; set; } = true;


}
