using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoosenMediator : MonoBehaviour
{
    [SerializeField] private List<TextSlot> textSlots;
    [SerializeField] private List<TextView> textViews;
    [SerializeField] private List<NewsScriptableObject> newsScriptableObjects;
    [SerializeField] private TextWriter titleWriter, titleBody;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextScriptableObject titleTextScriptableObject;

    private Dictionary<TextSlot, TextView> _dictionaryTexPosition;
    private Dictionary<TextView, NewsScriptableObject> _dictionaryNewsText;
    private int _textsInPlace;

    private void Awake()
    {
        SetStatus(false);
    }

    public void SetStatus(bool status)
    {
        canvas.enabled = status;
    }

    void Start()
    {
        _dictionaryTexPosition = new Dictionary<TextSlot, TextView>();
        _dictionaryNewsText = new Dictionary<TextView, NewsScriptableObject>();
        foreach (var textSlot in textSlots)
        {
            textSlot.OnSlotIsFilled += SlotIsFilled;
        }

        foreach (var textView in textViews)
        {
            textView.Init(canvas);
        }

        for (var index = 0; index < newsScriptableObjects.Count; index++)
        {
            var newsScriptableObject = newsScriptableObjects[index];
            var textView = textViews[index];
            textView.SetText(newsScriptableObject.Text_Title);
            _dictionaryNewsText.Add(textViews[index], newsScriptableObject);
        }

        title.SetText(titleTextScriptableObject.Text);
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
        var texts = _dictionaryNewsText[textView];
        titleWriter.SetText(texts.Text_Title.Text);
        titleWriter.StartWrite();
        titleBody.SetText(texts.Text_Description.Text);
        titleBody.StartWrite();
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