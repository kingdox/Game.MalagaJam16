using System.Collections;
using System.Collections.Generic;
using Kingdox.UniFlux;
using UnityEngine;

public class EndFlux : MonoFlux
{
    [SerializeField] private TextWriter textWriter;
    [SerializeField] private Canvas canvas;
    private ConclusionScriptableObject _conclusion;
    private int _quoteIndex;
    private Coroutine waitcourtine;

    private void Awake()
    {
        Display(false);
    }

    [Flux("End.Display")]
    private void Display(bool condition) => canvas.enabled = condition;

    [Flux("End.Start")]
    private void StartWrite()
    {
        Write();
    }

    private void Write()
    {
        _conclusion = NewsAtributteProcessor._.GetConclusion();
        _quoteIndex = 0;
        textWriter.SetText(_conclusion.quotes[_quoteIndex].Text);
        textWriter.OnTextEnd += DoWaitCoroutine;
        textWriter.StartWrite();
    }

    private void DoWaitCoroutine()
    {
        waitcourtine = StartCoroutine(WaitSeconds());
    }

    private IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(2);
        UpdateIndexQuote();
    }

    private void UpdateIndexQuote()
    {
        StopCoroutine(waitcourtine);
        _quoteIndex++;
        if (_quoteIndex < _conclusion.quotes.Length)
        {
            textWriter.ResetText();

            textWriter.SetText(_conclusion.quotes[_quoteIndex].Text);
        }
        else
        {
            Application.Quit();
        }
    }
}