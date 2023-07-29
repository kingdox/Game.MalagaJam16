using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    public TextMeshProUGUI textTextMeshProUGUI;
    public float letterPause  = 0.1f;
    [HideInInspector] public string _text;
    public Action OnTextStart;
    public Action OnTextEnd;
    public Action OnTextWrite;
    public Action OnTextReset;

    public void Awake()
    {
        ResetText();
    }

    public void SetText(string text)
    {
        _text = text;
    }
    
    public void StartWrite()
    {
       StartCoroutine(TypeText());
    }
    
    private IEnumerator TypeText()
    {
        OnTextStart?.Invoke();
        var currentText = _text;
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

    public void ResetText()
    {
        SetText("");
        textTextMeshProUGUI.SetText("");
    }
}