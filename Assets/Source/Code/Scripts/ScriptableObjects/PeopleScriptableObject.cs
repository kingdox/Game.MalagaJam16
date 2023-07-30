using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XavHelpTo;

[Serializable, CreateAssetMenu(menuName = "PeopleObject")]
public class PeopleScriptableObject : ScriptableObject
{
    [SerializeField] [TextArea] private string _debug_info;
    public Color peopleColor;
    public ListEnum<LanguageEnum, string> Names = new ListEnum<LanguageEnum, string>();
    public string Name => Names.Get(Application.systemLanguage.GET_LOCALE());

    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying) return;
        Names.ValidateList();
    }
    #endif
}