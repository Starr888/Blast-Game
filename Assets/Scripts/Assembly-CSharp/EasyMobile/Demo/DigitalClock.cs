using System;
using UnityEngine;
using UnityEngine.UI;

namespace EasyMobile.Demo
{
	[AddComponentMenu("")]
	[RequireComponent(typeof(Text))]
	public class DigitalClock : MonoBehaviour
	{
		private Text clockText;

		private void Start()
		{
			clockText = GetComponent<Text>();
		}

		private void Update()
		{
			clockText.text = DateTime.Now.ToString("hh:mm:ss");
		}
	}
}
