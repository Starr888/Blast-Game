using System;
using UnityEngine;

namespace UI.Pagination
{
	public class ScrollWheelInput : MonoBehaviour
	{
		public Action OnScrollUp;

		public Action OnScrollDown;

		public float UpdateRate = 0.125f;

		private float lastUpdated;

		private void Update()
		{
			lastUpdated += Time.deltaTime;
			float axis = Input.GetAxis("Mouse ScrollWheel");
			if (axis == 0f || !(lastUpdated >= UpdateRate))
			{
				return;
			}
			if (axis < 0f)
			{
				if (OnScrollUp != null)
				{
					OnScrollUp();
				}
			}
			else if (OnScrollDown != null)
			{
				OnScrollDown();
			}
			lastUpdated = 0f;
		}
	}
}
