using UnityEngine;

namespace UI.Pagination
{
	[ExecuteInEditMode]
	public class Viewport : MonoBehaviour
	{
		private PagedRect _pagedRect;

		public void Initialise(PagedRect pagedRect)
		{
			_pagedRect = pagedRect;
		}

		private void OnRectTransformDimensionsChange()
		{
			if (!(_pagedRect == null))
			{
				_pagedRect.ViewportDimensionsChanged();
			}
		}
	}
}
