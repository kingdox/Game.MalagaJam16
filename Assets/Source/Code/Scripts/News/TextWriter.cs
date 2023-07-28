using System.Collections;
using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    public TextMeshProUGUI textTextMeshProUGUI;
    public float letterPause  = 0.1f;
    public string _text;

    private void Start()
    {
        StartCoroutine(TypeText());
        string text = "OEOE";
    }

    private IEnumerator TypeText()
    {
        foreach (char letter in _text)
        {
            textTextMeshProUGUI.SetText(textTextMeshProUGUI.text+letter);
            // textTextMeshProUGUI.text += letter;
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
        //yield return new WaitForSeconds(1.0f);
    }
}