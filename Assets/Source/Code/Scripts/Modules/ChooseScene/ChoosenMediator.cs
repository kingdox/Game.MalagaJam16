using System.Collections.Generic;
using System.Threading.Tasks;
using Kingdox.UniFlux;
using TMPro;
using UnityEngine;
using XavHelpTo.Get;

public class ChoosenMediator : MonoFlux
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

    [Flux("Choice.Display")]
    private void Display(bool condition) => canvas.enabled = condition;

    [Flux("Choice.Start")]
    private void StartWrite()
    {
        Init();
    }

    private void Awake()
    {
        Display(false);
    }

    private void Init()
    {
        Service.PlayMusic(MusicEnum.Elecciones);
        ResetTexts();
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
            textView.SetNew(newsScriptableObject);
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
        _textsInPlace = 0;
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
        var soundEnum = GetRandomSoundForMMO();

        Service.PlaySound(soundEnum);
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
        var currentNew = textView.GetNew();
        _textsInPlace++;
        "CurrentNew".DispatchState(currentNew);
    }

    public void Continue()
    {
        Debug.Log(_textsInPlace == textSlots.Count ? "OK" : "ERROR");
        if (_textsInPlace == textSlots.Count)
        {
            GoToNextScene();
        }
    }

    private async void GoToNextScene()
    {
        titleBody.ResetText();
        Service.Fade(true);
        await Task.Delay(2000);
        Display(false);
        await Task.Delay(2000);
        "Map.Display".Dispatch(true);
        Service.Fade(false);
        "Map.Start".Dispatch();
    }
    
    private SoundEnum GetRandomSoundForMMO()
    {
        var soundEnum = Get.Range(SoundEnum.GrabPaper_1, SoundEnum.GrabPaper_2, SoundEnum.GrabPaper_3);

        return soundEnum;
    }
}