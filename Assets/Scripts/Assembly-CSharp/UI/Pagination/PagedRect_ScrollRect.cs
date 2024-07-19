using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Pagination
{
	[ExecuteInEditMode]
	public class PagedRect_ScrollRect : ScrollRect
	{
		public bool DisableDragging;

		public bool DisableScrollWheel;

		protected bool isBeingDragged;

		public bool ResetDragOffset;

		private bool notifyPagedRect = true;

		[SerializeField]
		private PagedRect _PagedRect;

		private RectTransform m_contentRectTransform;

		public PagedRect_Scrollbar ScrollBar;

		private static Vector3 horizontalVector = new Vector2(1f, 0f);

		private static Vector3 verticalVector = new Vector2(0f, -1f);

		public PagedRect PagedRect
		{
			get
			{
				if (_PagedRect == null)
				{
					_PagedRect = GetComponent<PagedRect>();
				}
				return _PagedRect;
			}
		}

		protected RectTransform contentRectTransform
		{
			get
			{
				if (m_contentRectTransform == null)
				{
					m_contentRectTransform = base.content.transform as RectTransform;
				}
				return m_contentRectTransform;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			contentRectTransform.pivot = new Vector2(0f, 1f);
		}

		public override void OnScroll(PointerEventData data)
		{
		}

		public override void OnBeginDrag(PointerEventData eventData)
		{
			if (!DisableDragging)
			{
				ResetDragOffset = false;
				if (notifyPagedRect && PagedRect != null)
				{
					PagedRect.OnBeginDrag(eventData);
				}
				base.OnBeginDrag(eventData);
				isBeingDragged = true;
			}
		}

		public override void OnDrag(PointerEventData eventData)
		{
			if (DisableDragging || !isBeingDragged)
			{
				return;
			}
			DragEventAnalysis dragEventAnalysis = AnalyseDragEvent(eventData);
			if ((!base.horizontal || dragEventAnalysis.DragPlane == DragEventAnalysis.eDragPlane.Horizontal) && (!base.vertical || dragEventAnalysis.DragPlane == DragEventAnalysis.eDragPlane.Vertical))
			{
				if (ResetDragOffset)
				{
					notifyPagedRect = false;
					OnEndDrag(eventData);
					OnBeginDrag(eventData);
					notifyPagedRect = true;
				}
				if (PagedRect != null)
				{
					PagedRect.OnDrag(eventData);
				}
				base.OnDrag(eventData);
			}
		}

		public override void OnEndDrag(PointerEventData eventData)
		{
			if (!DisableDragging && isBeingDragged)
			{
				isBeingDragged = false;
				if (notifyPagedRect && PagedRect != null)
				{
					PagedRect.OnEndDrag(eventData);
				}
				base.OnEndDrag(eventData);
			}
		}

		public DragEventAnalysis AnalyseDragEvent(PointerEventData data)
		{
			return new DragEventAnalysis(data);
		}

		public Vector2 GetDirectionVector()
		{
			return (!base.horizontal) ? verticalVector : horizontalVector;
		}

		public float GetOffset()
		{
			return (!base.horizontal) ? base.content.anchoredPosition.y : (0f - base.content.anchoredPosition.x);
		}

		public float GetTotalSize()
		{
			return (!base.horizontal) ? base.content.rect.height : base.content.rect.width;
		}

		public float GetPageSize()
		{
			return (!base.horizontal) ? PagedRect.sizingTransform.rect.height : PagedRect.sizingTransform.rect.width;
		}
	}
}
