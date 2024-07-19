using System;
using UnityEngine;
using UnityEngine.UI;

namespace EasyMobile.Demo
{
	[RequireComponent(typeof(Image))]
	[RequireComponent(typeof(Button))]
	public class ColorChooser : MonoBehaviour
	{
		private Image imgComp;

		private Button btnComp;

		public static event Action<Color> colorSelected;

		private void Start()
		{
			imgComp = GetComponent<Image>();
			btnComp = GetComponent<Button>();
			btnComp.onClick.AddListener(SelectColor);
		}

		public void SelectColor()
		{
			ColorChooser.colorSelected(imgComp.color);
		}

		static ColorChooser()
		{
			ColorChooser.colorSelected = delegate
			{
			};
		}
	}
}
