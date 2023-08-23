using System.Collections;
using UnityEngine;
using Kingdox.UniFlux;

public sealed class DiaN_Flux : MonoFlux
{
    [SerializeField] private TextWriter textWriter;
    [SerializeField] private Canvas canvas;

    [SerializeField] private TextScriptableObject textScriptableObject;

    private void Awake()
    {
        Display(false);
    }

    [Flux("DayN.Display")]
    private void Display(bool condition) => canvas.enabled = condition;

    [Flux("DayN.Start")] private void StartWrite()
    {
        Write();
    }
    
    private void Write()
    {
        "DayN".GetState(out int daysLeft);
        var originalText = textScriptableObject.Text;
        var formattedText = string.Format(originalText, daysLeft);
        textWriter.SetText(formattedText);
        textWriter.StartWrite();
        textWriter.OnTextEnd += GoToNextScene;
    }

    private void GoToNextScene()
    {
        StartCoroutine(GoToNextActivityCoroutine());
    }
    
    
    private IEnumerator GoToNextActivityCoroutine()
    {
        yield return new WaitForSeconds(2);
        Service.Fade(true);
        yield return new WaitForSeconds(1);
        textWriter.OnTextEnd -= GoToNextScene;        
        textWriter.ResetText();
        Display(false);
        yield return new WaitForSeconds(1);
        "Choice.Display".Dispatch(true);
        Service.Fade(false);
        "Choice.Start".Dispatch();
    }
}