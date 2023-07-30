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
        Init();
    }

    private void Awake()
    {
        // Display(false);
    }

    private void Init()
    {
        "CurrentNew".GetState(out currentNew);
        _isShowingQuote = true;
        dialogSystem.Init();
        Service.PlayMusic(MusicEnum.Casa);
        indexText = 0;
        dialogSystem.SetText(currentNew.Text_Quotes[indexText]);
    }

    [Flux(Kingdox.UniFlux.Click.Click.Key.OnClickEnterNew)]
    private void OnClickEnter()
    {
        if (!_enableEnter) return;
        _enableEnter = false;
        UpdateTextIndex();
        Debug.Log("AAA");
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

            indexText = 0;
            WritePeopleText();

            _isShowingQuote = false;
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
            //END
            return;
        }

        "DayN".DispatchState(daysLeft);

        GoToChoiceScene();
    }

    private async void GoToChoiceScene()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Debug.Log("Empezaos Corrotina MapFlux")
        StartCoroutine(GoToNextActivityCoroutine());
        return;
#endif
        dialogSystem.ResetTexts();
        Service.Fade(true);
        await Task.Delay(2000);
        Display(false);
        await Task.Delay(2000);
        "DayN.Display".Dispatch(true);
        Service.Fade(false);
        "DayN.Start".Dispatch();
    }
    
    private IEnumerator GoToNextActivityCoroutine()
    {
        dialogSystem.ResetTexts();
        Service.Fade(true);
        yield return new WaitForSeconds(2);
        Display(false);
        yield return new WaitForSeconds(2);
        "DayN.Display".Dispatch(true);
        Service.Fade(false);
        "DayN.Start".Dispatch();
    }
}