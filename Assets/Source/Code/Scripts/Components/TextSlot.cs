using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextSlot : MonoBehaviour, IDropHandler
{
    public event Action<TextSlot, TextView> OnSlotIsFilled;

    public void OnDrop(PointerEventData eventData)
    {
        var textView = eventData.pointerDrag.GetComponent<TextView>();
        if (!textView) return;
        // Debug.Log("Slot IN");
        OnSlotIsFilled?.Invoke(this, textView);
    }
}