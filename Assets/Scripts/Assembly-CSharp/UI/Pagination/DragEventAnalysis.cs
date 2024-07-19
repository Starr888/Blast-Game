using System;
using UnityEngine.EventSystems;

namespace UI.Pagination
{
	public class DragEventAnalysis
	{
		public enum eDragPlane
		{
			Horizontal = 0,
			Vertical = 1,
			None = 2
		}

		private PointerEventData data;

		public eDragPlane DragPlane
		{
			get
			{
				if (Math.Abs(data.delta.x) > Math.Abs(data.delta.y))
				{
					return eDragPlane.Horizontal;
				}
				if (Math.Abs(data.delta.y) > Math.Abs(data.delta.x))
				{
					return eDragPlane.Vertical;
				}
				return eDragPlane.None;
			}
		}

		public DragEventAnalysis(PointerEventData data)
		{
			this.data = data;
		}
	}
}
