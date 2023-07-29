using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Kingdox.UniFlux;
using Kingdox.UniFlux.Scenes;
//using Service;
using UnityEngine.SceneManagement;
using XavHelpTo;

public sealed class Bootstrap : MonoFlux
{
    //protected override void OnFlux(bool condition) {}
    //[Flux()] private void Method() => ;
    private static Bootstrap _;
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
        
        // END
        yield return Service.RemoveScene(SceneData.Bootstrap); 
    }
}