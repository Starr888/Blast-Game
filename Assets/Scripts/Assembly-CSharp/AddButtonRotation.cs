using UnityEngine;

public class AddButtonRotation : MonoBehaviour
{
	private void Start()
	{
		iTween.RotateBy(base.gameObject, iTween.Hash("z", -0.25, "easeType", "easeInOutCirc", "loopType", "loop", "delay", 0.4));
	}
}
