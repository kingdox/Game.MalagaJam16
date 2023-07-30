using System;
using System.Collections;
using TMPro;
using UnityEngine;
using XavHelpTo.Get;
using XavHelpTo.Know;

public class TextWriter : MonoBehaviour
{
    public TextMeshProUGUI textTextMeshProUGUI;
    public float letterPause = 0.1f;
    public bool canPlaySound = default;
    [HideInInspector] public string _text;
    public Action OnTextStart;
    public Action OnTextEnd;
    public Action OnTextWrite;
    public Action OnTextReset;
    private Coroutine typecoroutine;

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
        typecoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        OnTextStart?.Invoke();
        var currentText = _text;
        foreach (char letter in currentText)
        {
            if (currentText == _text)
            {
                textTextMeshProUGUI.SetText(textTextMeshProUGUI.text + letter);

                if (canPlaySound)
                {
                    SoundEnum s = Get.Range(SoundEnum.Key_1, SoundEnum.Key_2, SoundEnum.Key_3, SoundEnum.Key_4,
                        SoundEnum.Key_5);
                    Service.PlaySound(s);
                }

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
        if (typecoroutine != null)
        {
            StopCoroutine(typecoroutine);
        }
    }
}