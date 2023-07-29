using System.Collections.Generic;
using UnityEngine;

public class ChoosenMediator : MonoBehaviour
{
    [SerializeField] private List<TextSlot> textSlots;
    [SerializeField] private List<TextView> textViews;
    public float distance = 0.04f;

    private Dictionary<TextSlot, TextView> _dictionaryTexPosition;
    private int _textsInPlace;

    void Start()
    {
        _dictionaryTexPosition = new Dictionary<TextSlot, TextView>();
        foreach (var textSlot in textSlots)
        {
            textSlot.OnSlotIsFilled += SlotIsFilled;
        }

        foreach (var textSlot in textViews)
        {
            textSlot.OnTextBeginDrag += TextStartsDrag;
            textSlot.OnTextViewMoving += TextIsMoving;
        }
    }

    private void TextIsMoving(TextView obj)
    {
        //if texto is moving check if is moving inside a slot
        //set textIsInsideSlot in textview
        //When we do drop, check if slot was filled before and reset
        foreach (var VARIABLE in textSlots)
        {
            var distanceCenter = VARIABLE.GetComponent<RectTransform>().rect.center -
                                 obj.GetComponent<RectTransform>().rect.center;
            Debug.Log($"Distance {distanceCenter} {VARIABLE} CENTER {VARIABLE.GetComponent<RectTransform>().anchoredPosition}" +
                      $"CENTER2 {obj.GetComponent<RectTransform>().anchoredPosition}");
            var cointains =VARIABLE.GetComponent<RectTransform>().anchoredPosition - obj.GetComponent<RectTransform>().anchoredPosition;
            if (cointains.sqrMagnitude < distance)
            {
                Debug.Log($"{obj} is inside Slot {VARIABLE}");
            }
            //
            // // if (!RectContainsAnother(VARIABLE.GetComponent<RectTransform>(), obj.GetComponent<RectTransform>()))
            // //     continue;
            // // Debug.Log($"{obj} is inside Slot");
            // obj.SetInsideSlot(true);
            // return;
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
        if (_dictionaryTexPosition.ContainsKey(arg1))
        {
            arg2.SetToLastPosition();
            return;
        }

        _dictionaryTexPosition.Add(arg1, arg2);
        _textsInPlace++;
    }


    public void Continue()
    {
        Debug.Log(_textsInPlace == textSlots.Count ? "OK" : "ERROR");
    }

    public static bool RectContainsAnother(RectTransform rct, RectTransform another)
    {
        var r = rct.GetWorldRect();
        var a = another.GetWorldRect();
        return r.xMin <= a.xMin && r.yMin <= a.yMin && r.xMax >= a.xMax && r.yMax >= a.yMax;
    }
}