using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Kingdox.UniFlux;
using Updates =  Kingdox.UniFlux.Updates;
//using Service;

public sealed class FaderFlux : MonoFlux
{
    public CanvasGroup canvasGroup;
    public float target;
    public float speed;
    public float Speed
    {
        get => speed;
        [Flux("SpeedFade")] set => speed = value;
    }
    [Flux(Updates.UpdatesService.Key.OnUpdate)] private void OnUpdate()
    {
        canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Speed * Time.deltaTime);
    }
    [Flux("Fade")] private void Fade(bool condition) => target = condition ? 1 : 0;
}