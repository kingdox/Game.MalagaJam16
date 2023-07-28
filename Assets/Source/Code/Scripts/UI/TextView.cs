using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TextView : MonoBehaviour, IBeginDragHandler, IEndDragHandler,
    IDragHandler, IDropHandler, IInitializePotentialDragHandler
{
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    public Canvas canvas;
    public LayerMask layerMask;
    private RaycastHit2D[] raycastResults;
    private TextSlot _slot;

    public TextSlot Slot => _slot;

    private void Awake()
    {
        raycastResults = new RaycastHit2D[1];
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.4f;
        Debug.Log("hhhhhhhh");
        if (_slot)
        {
            _slot.Reset();
            _slot = null;
        }
        // Physics2D.RaycastNonAlloc(_rectTransform.anchoredPosition, Vector2.up, raycastResults, int.MaxValue, layerMask);
        // if (raycastResults.Length <= 0) return;
        // if (raycastResults[0].collider == null) return;
        //
        // var slot = raycastResults[0].transform.GetComponent<TextSlot>();
        // if (!slot) return;
        // slot.IsNotUsed();
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
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnDrop(PointerEventData eventData)
    {
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }

    public void SetSlot(TextSlot textSlot)
    {
        _slot = textSlot;
    }
}