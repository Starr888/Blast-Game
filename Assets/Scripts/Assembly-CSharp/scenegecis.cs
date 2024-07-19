using System.Collections;
using UnityEngine;

public class scenegecis : MonoBehaviour
{
	public static MapScene instance;

	public PopupOpener levelPopup;

	private void Start()
	{
		if (Configuration.instance.autoPopup > 0 && Configuration.instance.autoPopup <= Configuration.instance.maxLevel)
		{
			StartCoroutine(OpenLevelPopup());
		}
	}

	private void Update()
	{
	}

	private IEnumerator OpenLevelPopup()
	{
		yield return new WaitForSeconds(0.5f);
		StageLoader.instance.Stage = Configuration.instance.autoPopup;
		StageLoader.instance.LoadLevel();
		Configuration.instance.autoPopup = 0;
		levelPopup.OpenPopup();
	}
}
