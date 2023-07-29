using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextView : MonoBehaviour
    , IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IInitializePotentialDragHandler
{
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Canvas _canvas;
    private Vector2 _initialPosition;
    private bool _inSlot;
    [SerializeField] private TextMeshProUGUI title;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.4f;
        _inSlot = false;

        // Debug.Log("BeginDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1;

        // Debug.Log("OnEndDrag");
        if (_inSlot) return;
        // Debug.Log($"Was outside");
        SetToInitialPosition();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var textViewBehind = eventData.pointerDrag.GetComponent<TextView>();
        if (!textViewBehind) return;
        textViewBehind.SetToInitialPosition();
        // Debug.Log("OnDrop");
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }

    public void SetToInitialPosition()
    {
        // Debug.Log("Set To Initial Position");

        _rectTransform.anchoredPosition = _initialPosition;
    }

    public void IsInGoodPosition(bool status)
    {
        // Debug.Log($"IsInGoodPosition {status}");

        _inSlot = status;
    }

    public void Init(Canvas canvas1)
    {
        _canvas = canvas1;
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        _initialPosition = _rectTransform.anchoredPosition;
    }

    public void SetText(TextScriptableObject variableTextTitle)
    {
        title.SetText(variableTextTitle.Text);
    }
}