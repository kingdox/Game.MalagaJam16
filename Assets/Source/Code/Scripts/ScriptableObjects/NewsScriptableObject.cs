using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XavHelpTo;

[Serializable, CreateAssetMenu(menuName = "NewsObject")]
public class NewsScriptableObject : ScriptableObject
{
    [SerializeField] [TextArea] private string _debug_info;

    [Header("Cosas que se usan en ChooseScene")]
    [Space]
    public TextScriptableObject Text_Title;
    public TextScriptableObject Text_Description;
    
    [Header("Cosas que se usan en Map (la parte de las noticias)")]
    [Space]
    public TextScriptableObject[] Text_Quotes;


    [Header("Cosas que se usan en Map (la parte de las charlas)")]
    [Space]
    public PeopleTextScriptableObject[] Text_PeopleChat;
    
    
    [Header("Los puntajes que posse de valor esta noticia, influye en el juego en segundo plano")]
    [Space]
    public AttributeScriptableObject[] Attributes;
}