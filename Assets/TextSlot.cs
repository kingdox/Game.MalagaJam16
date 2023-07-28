using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextSlot : MonoBehaviour, IDropHandler, IDragHandler
{
    private RectTransform _rectTransform;
    public event Action OnTextEnter;
    public event Action OnTextExit;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Slot IN");
        var textView = eventData.pointerDrag.GetComponent<TextView>();
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
            _rectTransform.anchoredPosition;
        if (!textView) return;
        if (textView.Slot != null && textView.Slot != this)
        {
            textView.Slot.Reset();
        }
        textView.SetSlot(this);
        OnTextEnter?.Invoke();
    }

    public void Reset()
    {
        Debug.Log($"RESET {this}");
        OnTextExit?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Slot OUT");
        var textView = eventData.pointerDrag.GetComponent<TextView>();
        if (textView)
        {
            Reset();
        }
    }
}