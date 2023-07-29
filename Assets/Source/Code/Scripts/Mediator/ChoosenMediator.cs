using System.Collections.Generic;
using UnityEngine;

public class ChoosenMediator : MonoBehaviour
{
    [SerializeField] private List<TextSlot> textSlots;
    [SerializeField] private List<TextView> textViews;
    [SerializeField] private TextWriter titleWriter, titleBody;

    private Dictionary<TextSlot, TextView> _dictionaryTexPosition;
    private int _textsInPlace;
    private TextView _currentTextClicked;

    void Start()
    {
        _dictionaryTexPosition = new Dictionary<TextSlot, TextView>();
        foreach (var textSlot in textSlots)
        {
            textSlot.OnSlotIsFilled += SlotIsFilled;
        }

        foreach (var textView in textViews)
        {
            textView.OnTextBeginDrag += TextStartsDrag;
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

        foreach (var textView in textViews)
        {
            textView.OnTextBeginDrag -= TextStartsDrag;
        }
    }

    private void ResetTexts()
    {
        Debug.Log("Reset Texts");
        titleWriter.ResetText();
        titleBody.ResetText();
    }

    private void ResetSlot(TextSlot obj)
    {
        Debug.Log($"Reset slot {obj}");
        _dictionaryTexPosition.Remove(obj);
        ResetTexts();
        _textsInPlace--;
    }

    private void SlotIsFilled(TextSlot arg1, TextView textView)
    {
        Debug.Log($"SlotIsFilled {arg1} is filled with {textView}");
        if (_dictionaryTexPosition.ContainsKey(arg1))
        {
            Debug.Log($"SlotIsFilled 1");
            ResetSlot(arg1);
        }

        textView.SetToInitialPosition();
        //Get SO of textView
        Debug.Log($"SlotIsFilled 2");
        textView.IsInGoodPosition(true);
        textView.SetCompleteText();
        titleWriter.SetText("Title 1 ");
        titleWriter.StartCoroutine();
        titleBody.SetText("Body 1 ");
        titleBody.StartCoroutine();
        _dictionaryTexPosition.Add(arg1, textView);
        _textsInPlace++;
    }
    
    public async void Continue()
    {
        if (_textsInPlace == textSlots.Count)
        {
            //OK
        }
        // await 
        Debug.Log(_textsInPlace == textSlots.Count ? "OK" : "ERROR");
    }
}