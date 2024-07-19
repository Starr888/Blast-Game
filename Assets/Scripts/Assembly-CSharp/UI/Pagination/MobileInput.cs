using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Pagination
{
	[RequireComponent(typeof(PagedRect))]
	public class MobileInput : MonoBehaviour, IEndDragHandler, IEventSystemHandler
	{
		private readonly Vector2 mXAxis = new Vector2(1f, 0f);

		private readonly Vector2 mYAxis = new Vector2(0f, 1f);

		private const float mAngleRange = 30f;

		public float mMinSwipeDist = 15f;

		private const float mMinVelocity = 600f;

		private Vector2 mStartPosition;

		private float mSwipeStartTime;

		public Action OnSwipeRight;

		public Action OnSwipeLeft;

		public Action OnSwipeUp;

		public Action OnSwipeDown;

		private RectTransform _rectTransform;

		private Canvas _canvas;

		private PagedRect _pagedRect;

		private bool swipeInProgress;

		private RectTransform rectTransform
		{
			get
			{
				if (_rectTransform == null)
				{
					_rectTransform = GetComponent<RectTransform>();
				}
				return _rectTransform;
			}
		}

		private Canvas canvas
		{
			get
			{
				if (_canvas == null)
				{
					_canvas = GetComponentInParent<Canvas>();
				}
				return _canvas;
			}
		}

		private PagedRect pagedRect
		{
			get
			{
				if (_pagedRect == null)
				{
					_pagedRect = GetComponent<PagedRect>();
				}
				return _pagedRect;
			}
		}

		private void Update()
		{
			if (canvas.renderMode == RenderMode.WorldSpace)
			{
				return;
			}
			if (Input.GetMouseButtonDown(0))
			{
				mStartPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				if ((canvas.renderMode == RenderMode.ScreenSpaceOverlay && RectTransformUtility.RectangleContainsScreenPoint(rectTransform, mStartPosition)) || (canvas.renderMode == RenderMode.ScreenSpaceCamera && RectTransformUtility.RectangleContainsScreenPoint(rectTransform, mStartPosition, canvas.worldCamera)))
				{
					mSwipeStartTime = Time.time;
					swipeInProgress = true;
				}
				else
				{
					mStartPosition = Vector2.zero;
					swipeInProgress = false;
				}
			}
			if (swipeInProgress && Input.GetMouseButtonUp(0))
			{
				float num = Time.time - mSwipeStartTime;
				Vector2 vector = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				Vector2 lhs = vector - mStartPosition;
				float num2 = lhs.magnitude / num;
				if (num2 > 600f && lhs.magnitude > mMinSwipeDist)
				{
					lhs.Normalize();
					float f = Vector2.Dot(lhs, mXAxis);
					f = Mathf.Acos(f) * 57.29578f;
					if (f < 30f)
					{
						if (OnSwipeRight != null)
						{
							OnSwipeRight();
						}
					}
					else if (180f - f < 30f)
					{
						if (OnSwipeLeft != null)
						{
							OnSwipeLeft();
						}
					}
					else
					{
						f = Vector2.Dot(lhs, mYAxis);
						f = Mathf.Acos(f) * 57.29578f;
						if (f < 30f)
						{
							if (OnSwipeUp != null)
							{
								OnSwipeUp();
							}
						}
						else if (180f - f < 30f && OnSwipeDown != null)
						{
							OnSwipeDown();
						}
					}
				}
			}
			if (swipeInProgress && !Input.GetMouseButton(0))
			{
				swipeInProgress = false;
			}
		}

		public void OnEndDrag(PointerEventData data)
		{
			if (canvas.renderMode == RenderMode.WorldSpace)
			{
				pagedRect.HandleDragDelta(data);
			}
		}
	}
}
