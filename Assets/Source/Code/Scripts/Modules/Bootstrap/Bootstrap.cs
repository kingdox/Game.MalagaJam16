using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Kingdox.UniFlux;
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

        yield return SceneManager.LoadSceneAsync(SceneData.Scene, LoadSceneMode.Additive);
        yield return SceneManager.LoadSceneAsync(SceneData.Updates, LoadSceneMode.Additive);
        yield return SceneManager.LoadSceneAsync(SceneData.Click, LoadSceneMode.Additive);
        yield return SceneManager.LoadSceneAsync(SceneData.Binary, LoadSceneMode.Additive);
        yield return SceneManager.LoadSceneAsync(SceneData.EventSystem, LoadSceneMode.Additive);

        // yield return Kingdox.UniFlux.Scenes.Key.Add.Dispatch<IEnumerator>();
        yield return 0;

    }
}