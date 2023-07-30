using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu(menuName = "ConclusionObject")]
public class ConclusionScriptableObject : ScriptableObject
{
    public AttributeScriptableObject[] requireds;
    public TextScriptableObject[] quotes;
}
