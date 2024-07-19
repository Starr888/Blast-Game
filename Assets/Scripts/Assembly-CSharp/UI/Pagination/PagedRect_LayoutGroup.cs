using UnityEngine;
using UnityEngine.UI;

namespace UI.Pagination
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class PagedRect_LayoutGroup : LayoutGroup, ILayoutElement
	{
		public PagedRect pagedRect;

		[SerializeField]
		protected RectTransform.Axis m_Axis;

		public RectTransform.Axis Axis
		{
			get
			{
				return m_Axis;
			}
			set
			{
				SetProperty(ref m_Axis, value);
			}
		}

		public bool IsVertical
		{
			get
			{
				return m_Axis == RectTransform.Axis.Vertical;
			}
		}

		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			CalcAlongAxis(0, IsVertical);
		}

		public override void CalculateLayoutInputVertical()
		{
			CalcAlongAxis(1, IsVertical);
		}

		public override void SetLayoutHorizontal()
		{
			SetChildrenAlongAxis(0, IsVertical);
		}

		public override void SetLayoutVertical()
		{
			SetChildrenAlongAxis(1, IsVertical);
		}

		protected void CalcAlongAxis(int axis, bool isVertical)
		{
			float num = 0f;
			for (int i = 0; i < base.rectChildren.Count; i++)
			{
				Page component = base.rectChildren[i].GetComponent<Page>();
				if (!(component == null))
				{
					num = ((axis != 0) ? (num + component.layoutElement.preferredHeight * component.DesiredScale.y) : (num + component.layoutElement.preferredWidth * component.DesiredScale.x));
				}
			}
			SetLayoutInputForAxis(num, num, 1f, axis);
		}

		protected void SetChildrenAlongAxis(int axis, bool isVertical)
		{
			if (!isVertical)
			{
				SetChildrenHorizontal(axis);
			}
			else
			{
				SetChildrenVertical(axis);
			}
		}

		protected void SetChildrenHorizontal(int axis)
		{
			float num = 0f;
			for (int i = 0; i < base.rectChildren.Count; i++)
			{
				Page component = base.rectChildren[i].GetComponent<Page>();
				if (!(component == null))
				{
					if (axis == 1)
					{
						SetChildAlongAxis(base.rectChildren[i], axis, 0f, base.rectTransform.rect.height);
					}
					else
					{
						component.rectTransform.pivot = new Vector2(0f, 0.5f);
						component.rectTransform.localPosition = new Vector2(num, component.rectTransform.localPosition.y);
						component.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, component.layoutElement.preferredWidth);
						float num2 = component.layoutElement.preferredWidth * GetPageDesiredScale(component, 0);
						num += num2;
					}
					num += pagedRect.SpaceBetweenPages;
				}
			}
		}

		protected void SetChildrenVertical(int axis)
		{
			float num = 0f;
			for (int i = 0; i < base.rectChildren.Count; i++)
			{
				Page component = base.rectChildren[i].GetComponent<Page>();
				if (!(component == null))
				{
					if (axis == 0)
					{
						SetChildAlongAxis(base.rectChildren[i], axis, 0f, base.rectTransform.rect.width);
					}
					else
					{
						component.rectTransform.pivot = new Vector2(0.5f, 1f);
						component.rectTransform.localPosition = new Vector2(component.rectTransform.localPosition.x, num);
						component.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, component.layoutElement.preferredHeight);
						float num2 = component.layoutElement.preferredHeight * GetPageDesiredScale(component, 1);
						num -= num2;
					}
					num -= pagedRect.SpaceBetweenPages;
				}
			}
		}

		protected float GetPageDesiredScale(Page page, int axis)
		{
			if (pagedRect.ShowPagePreviews)
			{
				if (axis == 0)
				{
					return page.DesiredScale.x;
				}
				return page.DesiredScale.y;
			}
			return 1f;
		}
	}
}
