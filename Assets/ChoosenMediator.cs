using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosenMediator : MonoBehaviour
{
    [SerializeField] private List<TextSlot> textSlots;

    private int _textsInPlace;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var textSlot in textSlots)
        {
            textSlot.OnTextEnter += TextEnter;
            textSlot.OnTextExit += TextExit;
        }
    }

    private void OnDestroy()
    {
        foreach (var textSlot in textSlots)
        {
            textSlot.OnTextEnter -= TextEnter;
            textSlot.OnTextExit -= TextExit;
        }
    }

    private void TextExit()
    {
        _textsInPlace--;
    }

    private void TextEnter()
    {
        _textsInPlace++;
    }

    public void Continue()
    {
        if (_textsInPlace == textSlots.Count)
        {
            Debug.Log("OK");
        }
        else
        {
            Debug.Log("ERROR");
        }
    }
}