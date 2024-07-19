using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
	private void Awake()
	{
		if (PlayerPrefs.GetInt("isMusicEnabled", 0) == 0)
		{
			GetComponent<AudioSource>().Play();
		}
	}

	private void OnEnable()
	{
		AudioManager.OnMusicStatusChangedEvent += AudioManager_OnMusicStatusChangedEvent;
	}

	private void OnDisable()
	{
		AudioManager.OnMusicStatusChangedEvent -= AudioManager_OnMusicStatusChangedEvent;
	}

	private void AudioManager_OnMusicStatusChangedEvent(bool status)
	{
		if (status)
		{
			GetComponent<AudioSource>().Play();
		}
		else
		{
			GetComponent<AudioSource>().Stop();
		}
	}
}
