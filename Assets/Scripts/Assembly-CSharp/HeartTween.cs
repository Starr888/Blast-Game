using UnityEngine;

public class HeartTween : MonoBehaviour
{
	private void Start()
	{
		iTween.ScaleTo(base.gameObject, iTween.Hash("x", 1.7f, "y", 1.7f, "easeType", "easeInOutQuart", "loopType", "pingPong"));
	}
}
