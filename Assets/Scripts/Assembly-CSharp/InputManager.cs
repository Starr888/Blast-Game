using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
	public static InputManager instance;

	private static bool isTouchAvailable = true;

	public EventSystem eventSystem;

	public AudioClip ClickSound;

	public static event Action<Vector2> OnTouchDownEvent;

	public static event Action<Vector2> OnTouchUpEvent;

	public static event Action<Vector2> OnMouseDownEvent;

	public static event Action<Vector2> OnMouseUpEvent;

	public static event Action OnBackButtonPressedEvent;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public bool canInput(float delay = 0.5f, bool disableOnAvailable = true)
	{
		bool flag = isTouchAvailable;
		if (flag && disableOnAvailable)
		{
			isTouchAvailable = false;
			eventSystem.enabled = false;
			StopCoroutine("EnbaleTouchAfterDelay");
			StartCoroutine("EnbaleTouchAfterDelay", delay);
		}
		return flag;
	}

	public void DisableTouchForDelay(float delay = 0.5f)
	{
		isTouchAvailable = false;
		eventSystem.enabled = false;
		StopCoroutine("EnbaleTouchAfterDelay");
		StartCoroutine("EnbaleTouchAfterDelay", delay);
	}

	public void EnableTouch()
	{
		isTouchAvailable = true;
		eventSystem.enabled = true;
	}

	public IEnumerator EnbaleTouchAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		EnableTouch();
	}

	public void AddButtonTouchEffect()
	{
		if (AudioManager.instance.isSoundEnabled)
		{
			GetComponent<AudioSource>().PlayOneShot(ClickSound);
		}
	}

	public void AddButtonTouchEffect(GameObject btn)
	{
		if (AudioManager.instance.isSoundEnabled)
		{
			GetComponent<AudioSource>().PlayOneShot(ClickSound);
		}
	}

	private void Update()
	{
		Touch[] touches = Input.touches;
		for (int i = 0; i < touches.Length; i++)
		{
			Touch touch = touches[i];
			switch (touch.phase)
			{
			case TouchPhase.Began:
				if (InputManager.OnTouchDownEvent != null)
				{
					InputManager.OnTouchDownEvent(touch.position);
				}
				break;
			case TouchPhase.Ended:
			case TouchPhase.Canceled:
				if (InputManager.OnTouchUpEvent != null)
				{
					InputManager.OnTouchUpEvent(touch.position);
				}
				break;
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape) && InputManager.OnBackButtonPressedEvent != null)
		{
			InputManager.OnBackButtonPressedEvent();
		}
	}

	private void OnGUI()
	{
	}
}
