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
        var textView = eventData.pointerDrag.GetComponent<TextView>();
        // var textViewRectTransform = textView.GetComponent<RectTransform>();
        
        // textViewRectTransform.anchoredPosition = _rectTransform.anchoredPosition;
        if (!textView) return;
        // Debug.Log("Slot IN");
        OnSlotIsFilled?.Invoke(this, textView);
    }
}