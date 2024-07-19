using EasyMobile;
using UnityEngine;

public class MenuScene : MonoBehaviour
{
	public GoogleAnalyticsV4 googleAnalytics;

	private void Start()
	{
	//	googleAnalytics.LogScreen("Menu");
		GameServiceManager.Init();
		GetComponent<SceneTransition>().PerformTransition();
	}

	public void PlayButtonClick()
	{

        AudioManager.instance.ButtonClickAudio();
		GetComponent<SceneTransition>().PerformTransition();
	}

	public void LevelPlayClose()
	{
		AudioManager.instance.ButtonClickAudio();
		GetComponent<SceneTransition2>().PerformTransition();
	}

	private void OnEnable()
	{
		GameServiceManager.UserLoginSucceeded += OnUserLoginSucceeded;
		GameServiceManager.UserLoginFailed += OnUserLoginFailed;
	}

	private void OnDisable()
	{
		GameServiceManager.UserLoginSucceeded -= OnUserLoginSucceeded;
		GameServiceManager.UserLoginFailed -= OnUserLoginFailed;
	}

	private void OnUserLoginSucceeded()
	{
		Debug.Log("User logged in successfully.");
	}

	private void OnUserLoginFailed()
	{
		Debug.Log("User login failed.");
	}
}
