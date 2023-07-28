using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{

    [SerializeField] private Transform eee;
    public TextMeshProUGUI textTextMeshProUGUI;
    public float letterPause  = 0.1f;
    
    private IEnumerator TypeText()
    {
        string text = "OEOE";
        foreach (char letter in text.ToCharArray())
        {
            textTextMeshProUGUI.text += letter;
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
        //yield return new WaitForSeconds(1.0f);
        //StartGame();
    }

    // Update is called once per frame
    void Update()
    {
    }
}