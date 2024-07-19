using UnityEngine;

public class SceneTransition2 : MonoBehaviour
{
	public string scene = "<Insert scene name>";

	public float duration = 0.1f;

	public Color color = Color.black;

	public void PerformTransition()
	{
		Transition.LoadLevel(scene, 0.1f, color);
	}
}
