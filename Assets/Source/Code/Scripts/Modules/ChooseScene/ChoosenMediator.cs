using System.Collections;
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
    [SerializeField] private TextWriter titleWriter, titleBody;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextScriptableObject titleTextScriptableObject;
    public TextScriptableObject slotText;

    private Dictionary<TextSlot, TextView> _dictionaryTexPosition;
    private Dictionary<TextView, NewsScriptableObject> _dictionaryNewsText;
    private int _textsInPlace;
    public TextMeshProUGUI desecription;

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
        desecription.SetText(slotText.Text);

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

        var randomNews = NewsRandomSelector._.GetTwoRandomNews();

        for (var index = 0; index < randomNews.Count; index++)
        {
            var newsScriptableObject = randomNews[index];
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
        titleWriter.ResetText();
        titleBody.ResetText();
        _textsInPlace = 0;
    }

    private void RemoveSlot(TextSlot obj)
    {
        _dictionaryTexPosition.Remove(obj);
    }

    private void SlotIsFilled(TextSlot arg1, TextView textView)
    {
        ResetTexts();

        if (_dictionaryTexPosition.ContainsKey(arg1))
        {
            RemoveSlot(arg1);
            ResetTexts();
        }

        var soundEnum = GetRandomSoundForMMO();
        Service.PlaySound(soundEnum);
        textView.SetToInitialPosition();
        //Get SO of textView
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
        if (_textsInPlace == textSlots.Count)
        {
            GoToNextScene();
        }
    }

    private void GoToNextScene()
    {
        StartCoroutine(GoToNextActivityCoroutine());
    }

    private IEnumerator GoToNextActivityCoroutine()
    {
        titleBody.ResetText();
        Service.Fade(true);
        yield return new WaitForSeconds(2);
        Display(false);
        yield return new WaitForSeconds(2);
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