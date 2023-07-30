using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Kingdox.UniFlux;
using XavHelpTo;

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

    private void Start()
    {
        // Write();
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

    private async void GoToNextScene()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Debug.Log("Empezaos Corrotina DayN")
        StartCoroutine(GoToNextActivityCoroutine());
        return;
#endif
        await Task.Delay(2000);
        Service.Fade(true);
        await Task.Delay(1000);
        textWriter.OnTextEnd -= GoToNextScene;        
        textWriter.ResetText();
        Display(false);
        await Task.Delay(1000);
        "Choice.Display".Dispatch(true);
        Service.Fade(false);
        "Choice.Start".Dispatch();

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