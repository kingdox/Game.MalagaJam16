using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XavHelpTo;

[Serializable, CreateAssetMenu(menuName = "NewsObject")]
public class NewsScriptableObject : ScriptableObject
{
    [SerializeField] [TextArea] private string _debug_info;
    public TextScriptableObject Text_Summary;
    public TextScriptableObject[] Text_Quotes;

}