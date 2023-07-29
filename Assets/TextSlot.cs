using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextSlot : MonoBehaviour, IDropHandler
{
    private RectTransform _rectTransform;
    public event Action<TextSlot, TextView> OnSlotIsFilled;

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
        OnSlotIsFilled?.Invoke(this, textView);
    }

}