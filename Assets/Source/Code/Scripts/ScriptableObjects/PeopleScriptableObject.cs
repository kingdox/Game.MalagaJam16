using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XavHelpTo;

[Serializable, CreateAssetMenu(menuName = "PeopleObject")]
public class PeopleScriptableObject : ScriptableObject
{
    [SerializeField] [TextArea] private string _debug_info;
    public ListEnum<LanguageEnum, string> Name = new ListEnum<LanguageEnum, string>();
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying) return;
        Name.ValidateList();
    }
    #endif
}