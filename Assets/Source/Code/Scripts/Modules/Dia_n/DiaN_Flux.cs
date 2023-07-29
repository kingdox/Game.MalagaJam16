using UnityEngine;
using Kingdox.UniFlux;

public sealed class DiaN_Flux : MonoFlux
{
    [SerializeField] private TextWriter textWriter;
    [SerializeField] private Canvas canvas;

    [SerializeField] private TextScriptableObject textScriptableObject;

    private void Awake()
    {
        // canvas.enabled = false;
    }

    private void Start()
    {
        Method();
    }

    private void Method()
    {
        "DayN".GetState(out int daysLeft);
        var originalText = textScriptableObject.Text;
        var formattedText = string.Format(originalText, daysLeft);
        textWriter.SetText(formattedText);
        textWriter.StartWrite();
    }
}