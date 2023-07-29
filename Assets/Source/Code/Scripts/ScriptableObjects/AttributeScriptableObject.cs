using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XavHelpTo;

[Serializable, CreateAssetMenu(menuName = "AttributeObject")]
public class AttributeScriptableObject : ScriptableObject
{
    [SerializeField] [TextArea] private string _debug_info;
}