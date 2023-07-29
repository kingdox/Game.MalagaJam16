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
    [Flux("Intro.Display")] private void Display(bool condition)  => canvas.enabled = condition;
    [Flux("DayN.Start")] private void StartWrite()
    {
        Write();
    }

    private void Start()
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
    }
}