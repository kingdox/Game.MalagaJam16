using System.Collections.Generic;
using UnityEngine;

public class ChoosenMediator : MonoBehaviour
{
    [SerializeField] private List<TextSlot> textSlots;
    [SerializeField] private List<TextView> textViews;

    private Dictionary<TextSlot, TextView> _dictionaryTexPosition;
    private int _textsInPlace;

    void Start()
    {
        _dictionaryTexPosition = new Dictionary<TextSlot, TextView>();
        foreach (var textSlot in textSlots)
        {
            textSlot.OnSlotIsFilled += SlotIsFilled;
            textSlot.OnSlotIsReseted += ResetSlot;
        }

        foreach (var textSlot in textViews)
        {
            textSlot.OnTextBeginDrag += TextStartsDrag;
        }
    }

    private void TextStartsDrag(TextView textView)
    {
        TextSlot elementToDelete = null;
        foreach (var (key, value) in _dictionaryTexPosition)
        {
            if (value == textView)
            {
                elementToDelete = key;
            }
        }

        if (elementToDelete == null) return;
        ResetSlot(elementToDelete);
    }

    private void OnDestroy()
    {
        foreach (var textSlot in textSlots)
        {
            textSlot.OnSlotIsFilled -= SlotIsFilled;
            textSlot.OnSlotIsReseted -= ResetSlot;
        }

        foreach (var textSlot in textViews)
        {
            textSlot.OnTextBeginDrag -= TextStartsDrag;
        }
    }

    private void ResetSlot(TextSlot obj)
    {
        _dictionaryTexPosition.Remove(obj);

        _textsInPlace--;
    }

    private void SlotIsFilled(TextSlot arg1, TextView arg2)
    {
        _dictionaryTexPosition.Add(arg1, arg2);
        _textsInPlace++;
    }


    public void Continue()
    {
        Debug.Log(_textsInPlace == textSlots.Count ? "OK" : "ERROR");
    }
}