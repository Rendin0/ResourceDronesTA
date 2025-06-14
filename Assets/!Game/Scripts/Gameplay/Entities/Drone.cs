
using CrashKonijn.Agent.Core;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Drone : MonoBehaviour
{
    private CrystalsManager _crystalsManager;
    private NavMeshAgent _agent;

    [SerializeField] private Animator _animator;
    [field: SerializeField] public uint Tier { get; private set; }
    [field: SerializeField, Min(0f)] public float MoveSpeed { get; private set; }
    [field: SerializeField, Min(0f)] public float MiningSpeed { get; private set; }
    [field: SerializeField, Min(0f)] public float StorageSize { get; private set; }

    private Crystal _crystalTarger;
    private bool _isMoving;

    private readonly int _animatorWalkHash = Animator.StringToHash("isWalk");
    private readonly int _animatorIdleHash = Animator.StringToHash("isIdle");

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = MoveSpeed;
    }

    public void Init(CrystalsManager crystalsManager)
    {
        _crystalsManager = crystalsManager;
        _crystalTarger = _crystalsManager.FindNearestFreeCrystal(this);
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
                    _isMoving = false;
                    OnDestinationReached();
                }
            }
        }
    }

    // Метод для задания цели и начала движения
    public void MoveToTarget()
    {
        if (_crystalTarger == null)
        {
            StartCoroutine(FindCrystalCoroutine());
            return;
        }
        Debug.Log($"Moving to target {_crystalTarger.name}");
        _agent.SetDestination(_crystalTarger.transform.position);
        _isMoving = true;
        _animator.SetBool(_animatorWalkHash, _isMoving);
        _animator.SetBool(_animatorIdleHash, !_isMoving);
    }

    private IEnumerator FindCrystalCoroutine()
    {
        while (_crystalTarger == null)
        {
            _crystalTarger = _crystalsManager.FindNearestFreeCrystal(this);
            if (_crystalTarger != null)
            {
                MoveToTarget();
                yield break;
            }
            yield return new WaitForSeconds(1f); // Пауза перед следующей попыткой
        }
    }

    // Метод, который вызывается после достижения цели
    private void OnDestinationReached()
    {
        _animator.SetBool(_animatorWalkHash, _isMoving);
        _animator.SetBool(_animatorIdleHash, !_isMoving);


    }
}