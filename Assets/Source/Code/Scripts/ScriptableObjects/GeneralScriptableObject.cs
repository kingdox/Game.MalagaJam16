using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XavHelpTo;

[Serializable, CreateAssetMenu(menuName = "GeneralObject")]
public class GeneralScriptableObject : ScriptableObject
{
    public List<NewsScriptableObject> News = new List<NewsScriptableObject>();
    public ListEnum<MusicEnum, AudioClip> Musics = new ListEnum<MusicEnum, AudioClip>();
    public ListEnum<SoundEnum, AudioClip> Sounds = new ListEnum<SoundEnum, AudioClip>();
    
    public ListEnum<PeopleEnum, PeopleScriptableObject> Peoples = new ListEnum<PeopleEnum, PeopleScriptableObject>();

    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying) return;
        Musics.ValidateList();
        Sounds.ValidateList();
        Peoples.ValidateList();
    }
    #endif
}