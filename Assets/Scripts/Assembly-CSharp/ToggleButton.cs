using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IEventSystemHandler
{
	public Image btnToggleGraphics;

	[HideInInspector]
	public Vector2 btnToggleGraphicsPosition = Vector2.zero;

	private void Start()
	{
		btnToggleGraphicsPosition = btnToggleGraphics.rectTransform.anchoredPosition;
	}

	public virtual void OnPointerClick(PointerEventData eventData)
	{
		Vector2 anchoredPosition = btnToggleGraphics.rectTransform.anchoredPosition;
		anchoredPosition[0] = Mathf.Clamp(anchoredPosition.x, 0f - Mathf.Abs(btnToggleGraphicsPosition.x), Mathf.Abs(btnToggleGraphicsPosition.x));
		btnToggleGraphics.rectTransform.anchoredPosition = anchoredPosition;
		bool flag = anchoredPosition.x < 0f;
		EGTween.MoveTo(btnToggleGraphics.gameObject, EGTween.Hash("x", (!flag) ? (0f - Mathf.Abs(btnToggleGraphicsPosition.x)) : Mathf.Abs(btnToggleGraphicsPosition.x), "isLocal", true, "time", 0.5f, "easeType", EGTween.EaseType.easeOutExpo));
		OnToggleStatusChanged(flag);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
	}

	public virtual void OnDrag(PointerEventData eventData)
	{
		Vector2 localPoint = Vector2.zero;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform.GetComponent<RectTransform>(), eventData.position, Camera.main, out localPoint);
		localPoint[0] = Mathf.Clamp(localPoint.x, 0f - Mathf.Abs(btnToggleGraphicsPosition.x), Mathf.Abs(btnToggleGraphicsPosition.x));
		localPoint[1] = btnToggleGraphics.rectTransform.anchoredPosition.y;
		btnToggleGraphics.rectTransform.anchoredPosition = localPoint;
	}

	public virtual void OnEndDrag(PointerEventData eventData)
	{
		Vector2 anchoredPosition = btnToggleGraphics.rectTransform.anchoredPosition;
		anchoredPosition[0] = Mathf.Clamp(anchoredPosition.x, 0f - Mathf.Abs(btnToggleGraphicsPosition.x), Mathf.Abs(btnToggleGraphicsPosition.x));
		btnToggleGraphics.rectTransform.anchoredPosition = anchoredPosition;
		bool flag = !(anchoredPosition.x < 0f);
		EGTween.MoveTo(btnToggleGraphics.gameObject, EGTween.Hash("x", (!flag) ? (0f - Mathf.Abs(btnToggleGraphicsPosition.x)) : Mathf.Abs(btnToggleGraphicsPosition.x), "isLocal", true, "time", 0.5f, "easeType", EGTween.EaseType.easeOutExpo));
		OnToggleStatusChanged(flag);
	}

	public virtual void OnToggleStatusChanged(bool status)
	{
	}
}
