using System.Collections;
using Updates =  Kingdox.UniFlux.Updates;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using XavHelpTo.Get;
using XavHelpTo.Change;
using XavHelpTo.Set;
using XavHelpTo;
using Kingdox.UniFlux;

#pragma warning disable 0649
public sealed class AudioSystem : MonoFlux
{
    public const float DEFAULT_SOUND_PERCENT = 1f;
    public const float DEFAULT_MUSIC_PERCENT = 1f;
    private const string MUSIC_KEY = "MusicVolume";
    private const string SOUND_KEY = "SoundVolume";

    private const float TIMER_FADE = 5;
    private const float MIN_dB = 80f;
    private const float MAX_dB = 55f;//dato curioso: Según la OMS, el nivel de ruido que el oído humano puede tolerar sin alterar su salud es de 55 decibeles. Y dependiendo del tiempo de exposición, ruidos mayores a los 60 decibeles pueden provocarnos malestares físicos.
    private Vector2 dBValues;

    [Header("AudioSystem")]
    public GeneralScriptableObject general;
    public AudioMixer mixer;
    public AudioSource src_sound;
    public AudioSource src_music_A;
    public AudioSource src_music_B;
    public float crossSpeed = 2.0f;
    public float volume_music_A_target = 0.0f;
    public float volume_music_B_target = 0.0f;

    [Flux(Updates.UpdatesService.Key.OnUpdate)] void OnUpdate()
    {
        src_music_A.volume = Mathf.MoveTowards(src_music_A.volume, volume_music_A_target, Time.deltaTime * crossSpeed);
        src_music_B.volume = Mathf.MoveTowards(src_music_B.volume, volume_music_B_target, Time.deltaTime * crossSpeed);
    }
    public void StartCrossfade(AudioClip clip)
    {
        if (volume_music_A_target>volume_music_B_target)
        {
            $"B".Print();
            volume_music_A_target=0;
            volume_music_B_target=1;
            src_music_B.clip=clip;
            src_music_B.Play();
        }
        else if(volume_music_B_target>volume_music_A_target)
        {
            $"A1".Print();
            volume_music_A_target=1;
            volume_music_B_target=0;
            src_music_A.clip=clip;
            src_music_A.Play();
        }
        else
        {
            $"A2".Print();
            volume_music_A_target=1;
            volume_music_B_target=0;
            src_music_A.clip=clip;
            src_music_A.Play();
        }
    }

    [Flux("PlayMusic")] public void PlayMusic(MusicEnum e) 
    {
        StartCrossfade(general.Musics.Get(e));
    }
    [Flux("PlaySound")] public void PlaySound(SoundEnum g) 
    {
        // src_sound.clip = general.Sounds.Get(g);
        // src_sound.Play();
        src_sound.PlayOneShot(general.Sounds.Get(g));
    }
    [Flux("StopMusic")] public void StopMusic() 
    {
        volume_music_A_target=0;
        volume_music_B_target=0;   
    }
}