using UnityEngine;
using Kingdox.UniFlux;
public sealed class CameraFlux : MonoFlux
{
    public const float MIN = 0.36f;
    public const float MAX = 2f;
    public float speed = 2f;
    public bool changePos;
    public Camera camerar;
    private void Awake() => camerar.orthographicSize=MIN;
    [Flux("Camera.Change")]private void Change(bool condition) => changePos = condition;
    [Flux(Kingdox.UniFlux.Updates.UpdatesService.Key.OnUpdate)] private void OnUpdate() => Move();
    private void Move() => camerar.orthographicSize = Mathf.MoveTowards(camerar.orthographicSize,changePos?MAX:MIN,speed*Time.deltaTime);
}