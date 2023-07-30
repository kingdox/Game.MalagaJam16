using System.Collections;
using System.Collections.Generic;
using Kingdox.UniFlux;
using UnityEngine;

public class EndFlux : MonoFlux
{
    [SerializeField] private TextWriter textWriter;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextScriptableObject textScriptableObject;
    
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
        var originalText = textScriptableObject.Text;
        textWriter.SetText("F");
        textWriter.StartWrite();
    }
}