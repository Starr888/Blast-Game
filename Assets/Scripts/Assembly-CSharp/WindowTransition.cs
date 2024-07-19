using UnityEngine;
using UnityEngine.UI;

public class WindowTransition : MonoBehaviour
{
	public bool doAnimateOnLoad = true;

	public bool doAnimateOnDestroy = true;

	public bool doFadeInBackLayOnLoad = true;

	public bool doFadeOutBacklayOnDestroy = true;

	public Image BackLay;

	public GameObject WindowContent;

	public float TransitionDuration = 0.35f;

	public void OnWindowAdded()
	{
		if (doAnimateOnLoad && WindowContent != null)
		{
			WindowContent.MoveFrom(EGTween.Hash("x", -600, "easeType", EGTween.EaseType.easeOutBack, "time", TransitionDuration, "islocal", true, "ignoretimescale", true));
		}
		if (doFadeInBackLayOnLoad)
		{
			BackLay.gameObject.ValueTo(EGTween.Hash("From", 0, "To", 0.7f, "Time", TransitionDuration, "onupdate", "OnOpacityUpdate", "onupdatetarget", base.gameObject, "ignoretimescale", true));
		}
	}

	public void OnWindowRemove()
	{
		if (doAnimateOnDestroy && WindowContent != null)
		{
			WindowContent.MoveTo(EGTween.Hash("x", 600f, "easeType", EGTween.EaseType.easeInBack, "time", 0.5f, "islocal", true, "ignoretimescale", true));
			if (doFadeOutBacklayOnDestroy)
			{
				BackLay.gameObject.ValueTo(EGTween.Hash("From", TransitionDuration, "To", 0f, "Time", TransitionDuration, "onupdate", "OnOpacityUpdate", "onupdatetarget", base.gameObject, "ignoretimescale", true));
			}
			Invoke("OnRemoveTransitionComplete", 0.5f);
		}
		else if (doFadeOutBacklayOnDestroy)
		{
			BackLay.gameObject.ValueTo(EGTween.Hash("From", TransitionDuration, "To", 0f, "Time", TransitionDuration, "onupdate", "OnOpacityUpdate", "onupdatetarget", base.gameObject));
			Invoke("OnRemoveTransitionComplete", 0.5f);
		}
		else
		{
			OnRemoveTransitionComplete();
		}
	}

	public void AnimateWindowOnLoad()
	{
		if (doAnimateOnLoad && WindowContent != null)
		{
			WindowContent.MoveFrom(EGTween.Hash("x", 600, "easeType", EGTween.EaseType.easeOutBack, "time", TransitionDuration, "islocal", true));
		}
		FadeInBackLayOnLoad();
	}

	public void AnimateWindowOnDestroy()
	{
		if (doAnimateOnDestroy && WindowContent != null)
		{
			WindowContent.MoveTo(EGTween.Hash("x", -600f, "easeType", EGTween.EaseType.easeInBack, "time", TransitionDuration, "islocal", true));
		}
		FadeOutBacklayOnDestroy();
	}

	public void FadeInBackLayOnLoad()
	{
		if (doFadeInBackLayOnLoad)
		{
			BackLay.gameObject.ValueTo(EGTween.Hash("From", 0f, "To", 0.5f, "Time", TransitionDuration, "onupdate", "OnOpacityUpdate", "onupdatetarget", base.gameObject));
		}
	}

	public void FadeOutBacklayOnDestroy()
	{
		if (doFadeOutBacklayOnDestroy)
		{
			BackLay.gameObject.ValueTo(EGTween.Hash("From", 0.5f, "To", 0f, "Time", TransitionDuration, "onupdate", "OnOpacityUpdate", "onupdatetarget", base.gameObject));
		}
	}

	private void OnOpacityUpdate(float Opacity)
	{
		BackLay.color = new Color(BackLay.color.r, BackLay.color.g, BackLay.color.b, Opacity);
	}

	private void OnRemoveTransitionComplete()
	{
		Object.Destroy(base.gameObject);
	}
}
