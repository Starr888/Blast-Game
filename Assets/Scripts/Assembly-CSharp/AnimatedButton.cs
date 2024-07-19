using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class AnimatedButton : UIBehaviour, IPointerDownHandler, IEventSystemHandler
{
	[Serializable]
	public class ButtonClickedEvent : UnityEvent
	{
	}

	public bool interactable = true;

	[SerializeField]
	private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

	private Animator m_animator;

	public ButtonClickedEvent onClick
	{
		get
		{
			return m_OnClick;
		}
		set
		{
			m_OnClick = value;
		}
	}

	protected override void Start()
	{
		base.Start();
		m_animator = GetComponent<Animator>();
	}

	public virtual void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left && interactable)
		{
			Press();
		}
	}

	private void Press()
	{
		if (IsActive())
		{
			Invoke("InvokeOnClickAction", 0.1f);
		}
	}

	private void InvokeOnClickAction()
	{
		m_OnClick.Invoke();
	}
}
