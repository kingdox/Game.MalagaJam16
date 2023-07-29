using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Kingdox.UniFlux;
using Kingdox.UniFlux.Scenes;
using UnityEngine.SceneManagement;
using XavHelpTo;
using XavHelpTo.Get;
using XavHelpTo.Know;

public sealed class Bootstrap : MonoFlux
{
    
    private static Bootstrap _;
    private void Awake()
    {
        if (SceneManager.sceneCount!=1)
        {
            Reset();
        }
    }

    private IEnumerator Start()
    {
        //FIRST
        yield return SceneManager.LoadSceneAsync(SceneData.Scene, LoadSceneMode.Additive);

        // ESENTIALS
        yield return Service.AddScene(SceneData.Updates);
        yield return Service.AddScene(SceneData.Click);
        yield return Service.AddScene(SceneData.Binary);
        yield return Service.AddScene(SceneData.EventSystem);
        yield return Service.AddScene(SceneData.System_Audio); 
        yield return Service.AddScene(SceneData.Fader); 

        // EXPERIMENTAL
        yield return Service.AddScene(SceneData.Dia_n); 
        yield return Service.AddScene(SceneData.ChooseScene); 
        yield return Service.AddScene(SceneData.Intro); 
        yield return Service.AddScene(SceneData.Map); 


        // INIT GAME
        Service.SetBinary("_debug_", Service.GetBinary("_debug_", 0) + 1);
        "DayN".DispatchState(3);

        Service.PlayMusic(MusicEnum.Intro);
        "Intro.Display".Dispatch(true);
        "Intro.Start".Dispatch();

        // END
        yield return Service.RemoveScene(SceneData.Bootstrap); 
    }

    [Flux("Reset")] private void Reset()
    {
        SceneManager.LoadScene(SceneData.Bootstrap);       
    }
}