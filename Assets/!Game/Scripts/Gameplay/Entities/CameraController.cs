using _Game.Gameplay.UI;
using R3.Triggers;
using R3;
using UnityEngine;
using UnityEngine.Splines;
using Zenject;

[RequireComponent(typeof(SplineAnimate))]
public class CameraController : MonoBehaviour
{
    private SplineAnimate _spline;

    [Inject] private readonly GameplaySceneUIView _gameplayUI;

    private bool _isMoving = false;
    private bool _isForward = false;

    private void Awake()
    {
        _spline = GetComponent<SplineAnimate>();
    }

    private void Start()
    {
        _spline.NormalizedTime = 0f;

        WindowOptions.Instance.Position.Subscribe(pos => SetCameraOrientation(pos));

        _gameplayUI.LeftMover.OnMouseEnter.Subscribe(_ => { _isMoving = true; _isForward = true; });
        _gameplayUI.RightMover.OnMouseEnter.Subscribe(_ => { _isMoving = true; _isForward = false; });

        _gameplayUI.LeftMover.OnMouseExit.Subscribe(_ => _isMoving = false);
        _gameplayUI.RightMover.OnMouseExit.Subscribe(_ => _isMoving = false);
    }

    private void SetCameraOrientation(WindowPosition pos)
    {
        _spline.ObjectForwardAxis = pos switch
        {
            WindowPosition.Bottom => SplineComponent.AlignAxis.NegativeXAxis,
            WindowPosition.Left => SplineComponent.AlignAxis.NegativeYAxis,
            WindowPosition.Right => SplineComponent.AlignAxis.NegativeYAxis,
            WindowPosition.Top => SplineComponent.AlignAxis.NegativeXAxis,
            _ => SplineComponent.AlignAxis.NegativeXAxis
        };

        Camera.main.fieldOfView = pos switch
        {
            WindowPosition.Bottom => 21f,
            WindowPosition.Left => 123f,
            WindowPosition.Right => 123f,
            WindowPosition.Top => 21f,
            _ => 21f
        };

        MoveAlongSpline();
    }

    private void Update()
    {
        if (_isMoving)
            MoveAlongSpline();
    }

    private void MoveAlongSpline()
    {
        _spline.NormalizedTime = Mathf.Clamp01((0.03f * (_isForward ? 1 : -1) * Time.deltaTime) + _spline.NormalizedTime);
    }

}