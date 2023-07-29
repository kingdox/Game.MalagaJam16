using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextView : MonoBehaviour, IBeginDragHandler, IEndDragHandler,
    IDragHandler, IDropHandler, IInitializePotentialDragHandler
{
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    public Canvas canvas;
    private Vector2 _lastPosition;
    private bool _wasOutsideScreen;
    private Camera _cameraMain;
    private bool _wasInsideSlot;

    public event Action<TextView> OnTextBeginDrag;
    public event Action<TextView> OnTextViewMoving;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _cameraMain = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.4f;
        OnTextBeginDrag?.Invoke(this);
        Debug.Log("BeginDrag");

                  
                  
                  // Physics2D.RaycastNonAlloc(_rectTransform.anchoredPosition, Vector2.up, raycastResults, int.MaxValue, layerMask);
                  // Physics2D.RaycastNonAlloc(_rectTransform.anchoredPosition, Vector2.up, raycastResults, int.MaxValue, layerMask);
        // if (raycastResults.Length <= 0) return;
        // if (raycastResults[0].collider == null) return;
        // Debug.Log("TEXT VIEW SLOT IN");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1;
        // Physics2D.RaycastNonAlloc(_rectTransform.anchoredPosition, eventData., raycastResults, MathF.i));
        // if (raycastResults.Length > 0)
        // {
        //     if (raycastResults[0].collider == null) return;
        //     var slot = raycastResults[0].transform.GetComponent<TextSlot>();
        //     if (slot)
        //     {
        //         slot.IsUsed();
        //         Debug.Log("SLOT IN");
        //     }
        // }
        Debug.Log("DFFFF");
        if (!_wasOutsideScreen) return;
        Debug.Log($"Was outside");
        _wasOutsideScreen = false;
        _rectTransform.anchoredPosition = _lastPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        bool isVisibleFrom = _rectTransform.IsVisibleFrom(_cameraMain);
        bool isFullyVisibleFrom = _rectTransform.IsFullyVisibleFrom(_cameraMain);
        OnTextViewMoving?.Invoke(this);
        _wasOutsideScreen = !isFullyVisibleFrom;
        if (!_wasOutsideScreen)
        {
            _lastPosition = _rectTransform.anchoredPosition;
            Debug.Log($"Saving las position");

        }

        // Debug.Log($"WAS OUTSIDE {!isVisibleFrom || !isFullyVisibleFrom}");
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!_wasOutsideScreen) return;
        Debug.Log($"Was outside");
        _wasOutsideScreen = false;
        SetToLastPosition();
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }

    public void SetToLastPosition()
    {
        _rectTransform.anchoredPosition = _lastPosition;
    }

    public void SetInsideSlot(bool b)
    {
        _wasInsideSlot = true;
    }
}