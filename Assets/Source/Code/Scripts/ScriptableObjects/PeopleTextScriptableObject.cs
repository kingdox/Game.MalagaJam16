using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XavHelpTo;

[Serializable, CreateAssetMenu(menuName = "PeopleTextObject")]
public class PeopleTextScriptableObject : TextScriptableObject
{
    public PeopleEnum people;
}