
using CrashKonijn.Agent.Core;
using FullOpaqueVFX;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(VFX_SpellManager))]
public class Drone : MonoBehaviour
{
    private CrystalsManager _crystalsManager;
    private NavMeshAgent _agent;
    private VFX_SpellManager _spellManager;

    [SerializeField] private Animator _animator;
    [field: SerializeField] public uint Tier { get; private set; }
    [field: SerializeField, Min(0f)] public float MoveSpeed { get; private set; }
    [field: SerializeField, Min(0f)] public float MiningSpeed { get; private set; }
    [field: SerializeField, Min(0f)] public float StorageSize { get; private set; }

    [SerializeField] private float _storage = 0f;
    private Crystal _crystalTarger;
    private bool _isMoving;

    private readonly int _animatorWalkHash = Animator.StringToHash("isWalk");
    private readonly int _animatorIdleHash = Animator.StringToHash("isIdle");

    private void Awake()
    {
        _spellManager = GetComponent<VFX_SpellManager>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = MoveSpeed;
    }

    public void Init(CrystalsManager crystalsManager)
    {
        _crystalsManager = crystalsManager;
        MoveToTarget();
    }

    void Update()
    {
        if (_isMoving)
        {
            // Проверяем, достиг ли агент цели (с учётом небольшого запаса)
            if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    OnDestinationReached();
                }
            }
        }
    }

    // Метод для задания цели и начала движения
    private void MoveToTarget()
    {
        if (_crystalTarger == null)
        {
            StartCoroutine(FindCrystalCoroutine());
            return;
        }
        
        MoveTo(_crystalTarger.gameObject);
    }

    private void SetMoving(bool isMoving)
    {
        _isMoving = isMoving;
        _animator.SetBool(_animatorWalkHash, _isMoving);
        _animator.SetBool(_animatorIdleHash, !_isMoving);
    }

    private void MoveTo(GameObject obj)
    {
        Debug.Log($"{name}: Moving to target {obj.name}");
        _agent.SetDestination(obj.transform.position);
        SetMoving(true);
    }

    private void MoveToStorage()
    {
        MoveTo(_crystalsManager.gameObject);
    }

    private IEnumerator FindCrystalCoroutine()
    {
        while (_crystalTarger == null)
        {
            _crystalTarger = _crystalsManager.FindNearestFreeCrystal(this);
            if (_crystalTarger != null)
            {
                _spellManager.target = _crystalTarger.transform;
                MoveToTarget();
                yield break;
            }
            yield return new WaitForSeconds(1f); // Пауза перед следующей попыткой
        }
    }

    // Метод, который вызывается после достижения цели
    private void OnDestinationReached()
    {
        SetMoving(false);

        if (_crystalTarger != null)
            StartCoroutine(MineCrystal());
        else
            SendCrystals();
    }

    private void SendCrystals()
    {
        _crystalsManager.Storage.Value += _storage;
        _storage = 0;
        MoveToTarget();
    }

    private IEnumerator MineCrystal()
    {
        while (_storage < StorageSize)
        {
            StartCoroutine(_spellManager.CastSpell());
            _storage = Mathf.Clamp(_crystalTarger.ResourcesPerTick + _storage, 0, StorageSize);
            yield return new WaitForSeconds(MiningSpeed * _crystalTarger.SpeedModifier);
        }

        _crystalTarger.IsFree = true;
        _crystalTarger = null;
        _spellManager.target = null;
        MoveToStorage();
    }
}