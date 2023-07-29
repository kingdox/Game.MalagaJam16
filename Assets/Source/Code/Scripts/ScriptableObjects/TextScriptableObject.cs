using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XavHelpTo;

[Serializable, CreateAssetMenu(menuName = "TextObject")]
public class TextScriptableObject : ScriptableObject
{
    [SerializeField] [TextArea] private string _debug_info;
    public ListEnum<LanguageEnum, string> Texts = new ListEnum<LanguageEnum, string>();
    public string Text => Texts.Get(Application.systemLanguage.GET_LOCALE());

    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying) return;
        Texts.ValidateList();
    }
    #endif
}