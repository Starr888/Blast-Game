using UnityEngine;

public class ShaketweenEffect : MonoBehaviour
{
	private void Start()
	{
		iTween.ShakePosition(base.gameObject, iTween.Hash("x", 0.05, "y", 0.05, "easeType", "easeInOutCirc", "loopType", "loop", "delay", 0.2));
	}
}
