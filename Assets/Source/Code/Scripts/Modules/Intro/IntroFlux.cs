using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kingdox.UniFlux;
using XavHelpTo;

public sealed class IntroFlux : MonoFlux
{
    public Canvas canvas;
    public TextWriter textWritter_titleGame;
    public TextWriter textWritter_intro;
    public TextScriptableObject text_titleGame;
    public TextScriptableObject text_intro;
    private void Awake()
    {
        Display(false);
    }
    protected override void OnFlux(in bool condition)
    {
        condition.Subscribe(ref textWritter_titleGame.OnTextEnd, OnTextEnd_TitleGame);
        condition.Subscribe(ref textWritter_intro.OnTextEnd, OnTextEnd_Intro);
    }
    [Flux("Intro.Display")] private void Display(bool condition)  => canvas.enabled = condition;
    [Flux("Intro.Start")] private void StartWrite()  
    {
        textWritter_titleGame.SetText(text_titleGame.Text);
        textWritter_titleGame.StartWrite();
    }
    private async void OnTextEnd_TitleGame()
    {
        await Task.Delay(2000);
        textWritter_titleGame.ResetText();
        await Task.Delay(2000);
        textWritter_intro.SetText(text_intro.Text);
        textWritter_intro.StartWrite();
    }
    private async void OnTextEnd_Intro()
    {
        await Task.Delay(2000);
        GoToNextActivity();
    }
    private async void GoToNextActivity()
    {
        Service.Fade(true);
        Service.StopMusic();
        await Task.Delay(1000);
        textWritter_intro.ResetText();
        Display(false);
        "DayN.Display".Dispatch(true);
        await Task.Delay(500);
        Service.Fade(false);
    }
}