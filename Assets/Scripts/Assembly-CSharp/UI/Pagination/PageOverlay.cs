using UnityEngine;
using UnityEngine.UI;

namespace UI.Pagination
{
	public class PageOverlay : MonoBehaviour
	{
		protected Page m_page;

		protected PagedRect m_pagedRect;

		private Image m_Image;

		public Image Image
		{
			get
			{
				if (m_Image == null)
				{
					m_Image = GetComponent<Image>();
				}
				return m_Image;
			}
		}

		public void Initialise(Page page, PagedRect pagedRect)
		{
			m_page = page;
			m_pagedRect = pagedRect;
			RectTransform rectTransform = base.transform as RectTransform;
			rectTransform.offsetMin = Vector2.zero;
			rectTransform.offsetMax = Vector2.one;
			rectTransform.localScale = Vector3.one;
			Image.color = m_pagedRect.PagePreviewOverlayNormalColor;
			Image.sprite = m_pagedRect.PagePreviewOverlayImage;
		}

		public void Clicked()
		{
			m_page.OverlayClicked();
		}

		public void MouseEnter()
		{
			Image.color = m_pagedRect.PagePreviewOverlayHoverColor;
		}

		public void MouseExit()
		{
			Image.color = m_pagedRect.PagePreviewOverlayNormalColor;
		}

		private void OnEnable()
		{
			if (m_pagedRect != null)
			{
				Image.color = m_pagedRect.PagePreviewOverlayNormalColor;
			}
		}

		private void OnDisable()
		{
			if (m_pagedRect != null)
			{
				Image.color = m_pagedRect.PagePreviewOverlayNormalColor;
			}
		}
	}
}
