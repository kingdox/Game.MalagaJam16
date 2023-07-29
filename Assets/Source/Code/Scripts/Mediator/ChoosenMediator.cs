using System.Collections.Generic;
using UnityEngine;

public class ChoosenMediator : MonoBehaviour
{
    [SerializeField] private List<TextSlot> textSlots;
    [SerializeField] private List<TextView> textViews;
    [SerializeField] private TextWriter titleWriter, titleBody;
    [SerializeField] private Canvas canvas;

    private Dictionary<TextSlot, TextView> _dictionaryTexPosition;
    private int _textsInPlace;

    void Start()
    {
        _dictionaryTexPosition = new Dictionary<TextSlot, TextView>();
        foreach (var textSlot in textSlots)
        {
            textSlot.OnSlotIsFilled += SlotIsFilled;
        }

        foreach (var textView in textViews)
        {
            textView.Init(canvas);
        }
    }

    private void OnDestroy()
    {
        foreach (var textSlot in textSlots)
        {
            textSlot.OnSlotIsFilled -= SlotIsFilled;
        }
    }

    private void ResetTexts()
    {
        // Debug.Log("Reset Texts");
        titleWriter.ResetText();
        titleBody.ResetText();
        _textsInPlace--;
    }

    private void RemoveSlot(TextSlot obj)
    {
        Debug.Log($"Reset slot {obj}");
        _dictionaryTexPosition.Remove(obj);
    }

    private void SlotIsFilled(TextSlot arg1, TextView textView)
    {
        Debug.Log($"SlotIsFilled {arg1} is filled with {textView}");
        if (_dictionaryTexPosition.ContainsKey(arg1))
        {
            // Debug.Log($"SlotIsFilled 1");
            RemoveSlot(arg1);
            ResetTexts();
        }

        textView.SetToInitialPosition();
        //Get SO of textView
        Debug.Log($"SlotIsFilled 2 {textView.name}");
        textView.IsInGoodPosition(true);
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