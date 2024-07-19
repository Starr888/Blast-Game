using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace EasyMobile.Demo
{
	[AddComponentMenu("")]
	[DisallowMultipleComponent]
	public class ColorHandler : MonoBehaviour
	{
		public float lerpTime = 1f;

		public Color[] colors;

		private Image imgComp;

		private Material material;

		private Color currentColor;

		private void Start()
		{
			imgComp = GetComponent<Image>();
			MeshRenderer component = GetComponent<MeshRenderer>();
			if (component != null)
			{
				material = component.material;
			}
			StartCoroutine(CRChangeColor(lerpTime));
		}

		private IEnumerator CRChangeColor(float time)
		{
			while (true)
			{
				if (material != null)
				{
					currentColor = material.color;
				}
				else if (imgComp != null)
				{
					currentColor = imgComp.color;
				}
				Color newColor;
				do
				{
					newColor = colors[Random.Range(0, colors.Length)];
				}
				while (newColor == currentColor);
				float elapsed = 0f;
				while (elapsed < time)
				{
					elapsed += Time.deltaTime;
					Color c = Color.Lerp(currentColor, newColor, elapsed / time);
					if (material != null)
					{
						material.color = c;
					}
					else if (imgComp != null)
					{
						imgComp.color = c;
					}
					yield return null;
				}
			}
		}
	}
}
