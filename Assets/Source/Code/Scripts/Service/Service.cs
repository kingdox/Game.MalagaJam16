using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kingdox.UniFlux;
using Scenes = Kingdox.UniFlux.Scenes;

public static class Service 
{
    public static IEnumerator AddScene(in string scene) => Scenes.Key.Add.Dispatch<string, IEnumerator>(scene);
    public static IEnumerator RemoveScene(in string scene) => Scenes.Key.Remove.Dispatch<string, IEnumerator>(scene);
    public static void PlayMusic(in MusicEnum music) =>  "PlayMusic".Dispatch(music);
    public static void PlaySound(in SoundEnum sound) => "PlaySound".Dispatch(sound);
    public static void Fade(in bool condition) => "Fade".Dispatch(condition);
    public static float SpeedFade
    {
        set => "SpeedFade".Dispatch(value);
    }
}
