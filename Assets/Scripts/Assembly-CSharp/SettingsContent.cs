using UnityEngine;

public class SettingsContent : MonoBehaviour
{
	public Transform settings_main;

	public Transform settings_gameplay;

	public static SettingsContent instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}
}
