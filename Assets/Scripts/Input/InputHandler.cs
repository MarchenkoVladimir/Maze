using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    public static event Action<bool> OnDive;
    public static event Action<PointerEventData> OnBeginDragEvent;
    public static event Action<PointerEventData> OnDragEvent;
    public static event Action<PointerEventData> OnEndDragEvent;
    public static event Action<PointerEventData> OnClickEvent;
    public static event Action<PointerEventData> OnStartHold;
    public static event Action<PointerEventData> OnStopHold;

    public static PointerEventData data;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDive?.Invoke(true);
        data = eventData;
        OnStartHold?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnDive?.Invoke(false);
        data = null;
        OnStopHold?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent?.Invoke(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(eventData);
    }
}
