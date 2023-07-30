using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Kingdox.UniFlux;
//using Service;

public sealed class MapFlux : MonoFlux
{
    public Animator animator_tv;

    [Flux("Map.Begin")] private void Begin() 
    {
        animator_tv.SetTrigger("Play");
    }
    private void OnEndIntroSound() 
    {
        Service.PlayMusic(MusicEnum.Report);
        
    }

}

/*

- Empieza a sonar la cadena de televisión
- Empieza el Animator


*/