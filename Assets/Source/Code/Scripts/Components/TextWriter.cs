using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Kingdox.UniFlux;

public class TextWriter : MonoBehaviour
{
    public TextMeshProUGUI textTextMeshProUGUI;
    public float letterPause  = 0.1f;
    public string _text;
    public Action OnTextStart;
    public Action OnTextEnd;
    public Action OnTextWrite;
    public Action OnTextReset;

    private void Start()
    {
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        OnTextStart?.Invoke();
        string currentText = _text;
        foreach (char letter in currentText)
        {
            if(currentText == _text)
            {
                textTextMeshProUGUI.SetText(textTextMeshProUGUI.text+letter);
                yield return new WaitForSeconds(letterPause);
                OnTextWrite?.Invoke();
            }
            else
            {
                OnTextReset?.Invoke();
                yield break;
            }
        }
        OnTextEnd?.Invoke();
        yield return default;
    }
}