
using R3;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Subject<Unit> OnMouseEnter = new();
    public Subject<Unit> OnMouseExit = new();

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnter.OnNext(Unit.Default);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExit.OnNext(Unit.Default);
    }
}