using System.Collections;
using UnityEngine;
using Kingdox.UniFlux;

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

    [Flux("Intro.Display")]
    private void Display(bool condition) => canvas.enabled = condition;

    [Flux("Intro.Start")]
    private void StartWrite()
    {
        textWritter_titleGame.SetText(text_titleGame.Text);
        textWritter_titleGame.StartWrite();
    }

    private void OnTextEnd_TitleGame()
    {
        Debug.Log("Empezaos OnTextEnd_TitleGame");

        StartCoroutine(OnTextEndCoroutine());
    }

    private IEnumerator OnTextEndCoroutine()
    {
        yield return new WaitForSeconds(2);

        textWritter_titleGame.ResetText();
        yield return new WaitForSeconds(2);
        textWritter_intro.SetText(text_intro.Text);
        textWritter_intro.StartWrite();
    }

    private void OnTextEnd_Intro()
    {
        GoToNextActivity();
    }

    private void GoToNextActivity()
    {
        Debug.Log("Empezamos Corroutina intro");

        StartCoroutine(GoToNextActivityCoroutine());
    }

    private IEnumerator GoToNextActivityCoroutine()
    {
        yield return new WaitForSeconds(2);
        Service.Fade(true);
        Service.StopMusic();
        yield return new WaitForSeconds(1);
        textWritter_intro.ResetText();
        Display(false);
        "DayN.Display".Dispatch(true);
        yield return new WaitForSeconds(0.5f);
        Service.Fade(false);
        "DayN.Start".Dispatch();
    }
}