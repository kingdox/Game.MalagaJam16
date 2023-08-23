using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kingdox.UniFlux;
using UnityEngine;

public class MapFlux : MonoFlux
{
    [SerializeField] private Canvas canvas;

    [SerializeField] private DialogSystem dialogSystem;
    private bool _enableEnter, _isShowingQuote;
    private int indexText;
    private NewsScriptableObject currentNew;

    protected override void OnFlux(in bool condition)
    {
        condition.Subscribe(ref dialogSystem.OnEnableEnter, EnableEnter);
    }

    [Flux("Map.Display")]
    private void Display(bool condition) => canvas.enabled = condition;

    [Flux("Map.Start")]
    private void StartWrite()
    {
        StartCoroutine(ReportMusic());
        Init();
    }

    private IEnumerator ReportMusic()
    {
        Service.PlaySound(SoundEnum.ReportIntro);
        yield return new WaitForSeconds(6f);
        Service.PlayMusic(MusicEnum.Report);
    }

    private void Init()
    {

        "CurrentNew".GetState(out currentNew);
        _isShowingQuote = true;
        dialogSystem.Init();
        indexText = 0;
        dialogSystem.SetText(currentNew.Text_Quotes[indexText]);
        NewsAtributteProcessor._.AddAttributes(currentNew.Attributes);
    }

    public void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (!_enableEnter) return;
        _enableEnter = false;
        UpdateTextIndex();
    }

    private void UpdateTextIndex()
    {

        indexText++;
        if (_isShowingQuote)
        {
            if (indexText < currentNew.Text_Quotes.Length)
            {
                dialogSystem.ResetTexts();

                dialogSystem.SetText(currentNew.Text_Quotes[indexText]);
                return;
            }
            else
            {
                // TErmina proceso de quotes
                "Camera.Change".Dispatch(true);
                Service.PlayMusic(MusicEnum.Casa);
                indexText = 0;
                WritePeopleText();
                _isShowingQuote = false;
            }
        }
        else
        {
            if (indexText < currentNew.Text_PeopleChat.Length)
            {
                WritePeopleText();
            }
            else
            {
                LastTextShown();
            }
        }
    }

    private void WritePeopleText()
    {
        dialogSystem.ResetTexts();
        dialogSystem.SetText(currentNew.Text_PeopleChat[indexText]);
    }

    private void EnableEnter()
    {
        _enableEnter = true;
    }

    private void LastTextShown()
    {
        "DayN".GetState(out int daysLeft);
        daysLeft--;
        if (daysLeft == 0)
        {
            GoToEndScene();
            return;
        }

        "DayN".DispatchState(daysLeft);

        GoToChoiceScene();
    }

    private void GoToEndScene()
    {
        StartCoroutine(GoToEndSceneCoroutine());
    }

    private IEnumerator GoToEndSceneCoroutine()
    {

        dialogSystem.ResetTexts();
        Service.Fade(true);
        yield return new WaitForSeconds(2);
        Display(false);
        "Camera.Change".Dispatch(false);
        yield return new WaitForSeconds(2);
        "End.Display".Dispatch(true);
        Service.Fade(false);
        "End.Start".Dispatch();
    }

    private void GoToChoiceScene()
    {
        StartCoroutine(GoToNextActivityCoroutine());
    }

    private IEnumerator GoToNextActivityCoroutine()
    {
        dialogSystem.ResetTexts();
        Service.Fade(true);
        yield return new WaitForSeconds(2);
        Display(false);
        "Camera.Change".Dispatch(false);
        yield return new WaitForSeconds(2);
        "DayN.Display".Dispatch(true);
        Service.Fade(false);
        "DayN.Start".Dispatch();
    }
}