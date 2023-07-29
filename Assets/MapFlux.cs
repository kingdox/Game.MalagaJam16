using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kingdox.UniFlux;
using UnityEngine;

public class MapFlux : MonoFlux
{
    [SerializeField] private Canvas canvas;

    [SerializeField] private DialogSystem dialogSystem;

    [Flux("Map.Display")]
    private void Display(bool condition) => canvas.enabled = condition;

    [Flux("Map.Start")]
    private void StartWrite()
    {
        Init();
    }

    private void Awake()
    {
        // Display(false);
    }

    private void Init()
    {
        dialogSystem.Init();
        Service.PlayMusic(MusicEnum.Casa);
        dialogSystem.OnLastTextIsShown += LastTextShown;
        LastTextShown();
    }

    private void LastTextShown()
    {
        "DayN".GetState(out int daysLeft);
        daysLeft--;
        if (daysLeft == 0)
        {
            //END
            return;
        }

        "DayN".DispatchState(daysLeft);

        GoToChoiceScene();
    }

    private async void GoToChoiceScene()
    {
        Service.Fade(true);
        await Task.Delay(2000);

        Display(false);
        await Task.Delay(2000);
        "Choice.Display".Dispatch(true);
        Service.Fade(false);
        "Choice.Start".Dispatch();
    }
}