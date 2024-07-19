using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Pagination
{
	public class Page : MonoBehaviour
	{
		[Serializable]
		public class PageEvent : UnityEvent
		{
		}

		[SerializeField]
		[Tooltip("Sets the text shown on the button if 'ShowPageTitlesOnButtons' is set")]
		public string PageTitle = string.Empty;

		[SerializeField]
		[Tooltip("Should this page be accessible?")]
		public bool PageEnabled = true;

		[SerializeField]
		[Tooltip("Should this button be shown on pagination?")]
		public bool ShowOnPagination = true;

		[SerializeField]
		public PageEvent OnShowEvent = new PageEvent();

		[SerializeField]
		public PageEvent OnHideEvent = new PageEvent();

		protected CanvasGroup _CanvasGroup;

		public bool UsePageAnimationType;

		public PagedRect.eAnimationType AnimationType;

		public bool FlipAnimationDirection;

		private LayoutElement m_layoutElement;

		private PageOverlay m_pageOverlay;

		private RectTransform m_rectTransform;

		[HideInInspector]
		public bool IsDuplicate;

		[HideInInspector]
		public Page OriginalPage;

		public Vector3 DesiredScale = Vector3.one;

		private Coroutine scaleCoroutine;

		public int PageNumber { get; set; }

		public bool Initialised { get; protected set; }

		public Animator Animator { get; protected set; }

		public bool Visible { get; protected set; }

		protected PagedRect _pagedRect { get; set; }

		protected Vector3 initialPosition { get; set; }

		public CanvasGroup CanvasGroup
		{
			get
			{
				if (_CanvasGroup == null)
				{
					_CanvasGroup = GetComponent<CanvasGroup>();
					if (_CanvasGroup == null)
					{
						_CanvasGroup = base.gameObject.AddComponent<CanvasGroup>();
					}
				}
				return _CanvasGroup;
			}
		}

		public LayoutElement layoutElement
		{
			get
			{
				if (m_layoutElement == null)
				{
					m_layoutElement = GetComponent<LayoutElement>();
				}
				if (m_layoutElement == null)
				{
					m_layoutElement = base.gameObject.AddComponent<LayoutElement>();
				}
				return m_layoutElement;
			}
		}

		public PageOverlay pageOverlay
		{
			get
			{
				if (m_pageOverlay == null)
				{
					GameObject gameObject = PaginationUtilities.InstantiatePrefab("Page Overlay");
					gameObject.transform.SetParent(base.transform);
					m_pageOverlay = gameObject.GetComponent<PageOverlay>();
					m_pageOverlay.Initialise(this, _pagedRect);
				}
				return m_pageOverlay;
			}
		}

		public RectTransform rectTransform
		{
			get
			{
				if (m_rectTransform == null)
				{
					m_rectTransform = GetComponent<RectTransform>();
				}
				return m_rectTransform;
			}
		}

		public void Initialise(PagedRect pagedRect)
		{
			if (Initialised)
			{
				return;
			}
			initialPosition = base.transform.localPosition;
			Initialised = true;
			_pagedRect = pagedRect;
			UpdateDimensions();
			if (!Application.isPlaying)
			{
				return;
			}
			Animator = GetComponent<Animator>();
			if (Animator == null)
			{
				Animator = base.gameObject.AddComponent<Animator>();
			}
			Animator.runtimeAnimatorController = UnityEngine.Object.Instantiate(pagedRect.AnimationControllerTemplate);
			if (!pagedRect.ShowPagePreviews)
			{
				return;
			}
			PagedRectTimer.DelayedCall(0f, delegate
			{
				if (pagedRect.CurrentPage != PageNumber)
				{
					ShowOverlay();
				}
			}, this);
		}

		public void UpdateDimensions()
		{
			if (!(_pagedRect == null))
			{
				RectTransform rectTransform = _pagedRect.sizingTransform;
				if (rectTransform == null)
				{
					rectTransform = (RectTransform)_pagedRect.transform;
				}
				Rect rect = rectTransform.rect;
				if (rect.height > 0f)
				{
					layoutElement.preferredHeight = rect.height;
				}
				if (rect.width > 0f)
				{
					layoutElement.preferredWidth = rect.width;
				}
			}
		}

		public void OnShow()
		{
			Visible = true;
			if (OnShowEvent != null)
			{
				OnShowEvent.Invoke();
			}
			HideOverlay();
		}

		public void OnHide()
		{
			Visible = false;
			if (OnHideEvent != null)
			{
				OnHideEvent.Invoke();
			}
			if (_pagedRect.ShowPagePreviews)
			{
				ShowOverlay();
			}
		}

		public void FadeIn()
		{
			base.gameObject.SetActive(true);
			PlayNewAnimation("FadeIn");
		}

		public void FadeOut()
		{
			if (base.gameObject.activeInHierarchy)
			{
				PlayNewAnimation("FadeOut");
				StartCoroutine(DisableWhenAnimationIsComplete());
			}
		}

		public void SlideIn(PagedRect.eDirection directionFrom, bool vertical = false)
		{
			base.gameObject.SetActive(true);
			string text = directionFrom.ToString();
			if (vertical)
			{
				text = ((directionFrom != 0) ? "Bottom" : "Top");
			}
			PlayNewAnimation("SlideIn_" + text);
		}

		public void SlideOut(PagedRect.eDirection directionTo, bool vertical = false)
		{
			if (base.gameObject.activeInHierarchy)
			{
				string text = directionTo.ToString();
				if (vertical)
				{
					text = ((directionTo != 0) ? "Bottom" : "Top");
				}
				PlayNewAnimation("SlideOut_" + text);
				StartCoroutine(DisableWhenAnimationIsComplete());
			}
		}

		protected IEnumerator DisableWhenAnimationIsComplete()
		{
			yield return new WaitForSeconds(1f / _pagedRect.AnimationSpeed);
			if (_pagedRect.GetCurrentPage() != this)
			{
				base.gameObject.SetActive(false);
				ResetPositionAndAlpha();
			}
		}

		protected void PlayNewAnimation(string animationName)
		{
			Animator.updateMode = AnimatorUpdateMode.UnscaledTime;
			Animator.speed = _pagedRect.AnimationSpeed;
			Animator.enabled = true;
			Animator.StopPlayback();
			Animator.Play(animationName);
		}

		public void LegacyReset()
		{
			Animator.StopPlayback();
			Animator.enabled = false;
			if (_pagedRect.GetCurrentPage() != this)
			{
				base.gameObject.SetActive(false);
			}
			ResetPositionAndAlpha();
		}

		public void ResetPositionAndAlpha()
		{
			base.transform.localPosition = initialPosition;
			CanvasGroup.alpha = 1f;
		}

		public void EnablePage()
		{
			PageEnabled = true;
			_pagedRect.UpdatePages();
		}

		public void DisablePage()
		{
			PageEnabled = false;
			_pagedRect.UpdatePages();
		}

		public void OverlayClicked()
		{
			if (_pagedRect.ShowPagePreviews)
			{
				_pagedRect.SetCurrentPage(this);
			}
		}

		public void ScaleToScale(Vector3 scale, float animationSpeed = 0.5f)
		{
			if (scaleCoroutine != null)
			{
				StopCoroutine(scaleCoroutine);
			}
			if (!(scale == rectTransform.localScale))
			{
				scaleCoroutine = StartCoroutine(ScaleToScaleInternal(scale, animationSpeed));
			}
		}

		protected IEnumerator ScaleToScaleInternal(Vector3 scale, float animationSpeed)
		{
			float percentageComplete = 0f;
			float timeStartedMoving = Time.time;
			float timeSinceStarted2 = 0f;
			Vector3 initialScale = rectTransform.localScale;
			while (percentageComplete < 1f)
			{
				timeSinceStarted2 = Time.time - timeStartedMoving;
				Vector3 temp = rectTransform.localScale;
				rectTransform.localScale = new Vector3(Mathf.Lerp(initialScale.x, scale.x, percentageComplete), Mathf.Lerp(initialScale.y, scale.y, percentageComplete), 1f);
				percentageComplete = timeSinceStarted2 / (0.25f / animationSpeed);
				if (temp != rectTransform.localScale)
				{
					DesiredScale = rectTransform.localScale;
					LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
				}
				yield return null;
			}
			rectTransform.localScale = scale;
			DesiredScale = rectTransform.localScale;
		}

		public void SetPagePosition(int position)
		{
			position = Math.Max(1, position);
			position = Math.Min(_pagedRect.NumberOfPages, position);
			rectTransform.SetSiblingIndex(position);
			_pagedRect.UpdatePages(true, true);
		}

		public void ShowOverlay()
		{
			if (_pagedRect.EnablePagePreviewOverlays && pageOverlay != null)
			{
				pageOverlay.gameObject.SetActive(true);
				ScaleOverlay();
			}
		}

		public void HideOverlay()
		{
			if (pageOverlay != null)
			{
				pageOverlay.gameObject.SetActive(false);
			}
		}

		private void ScaleOverlay()
		{
			if (_pagedRect.UsingScrollRect)
			{
				if (_pagedRect.ScrollRect.horizontal)
				{
					pageOverlay.transform.localScale = new Vector3(1f, _pagedRect.PagePreviewOverlayScaleOverride, 1f);
				}
				else
				{
					pageOverlay.transform.localScale = new Vector3(_pagedRect.PagePreviewOverlayScaleOverride, 1f, 1f);
				}
			}
		}

		public void NotifyPagedRectOfChange()
		{
			if (_pagedRect != null)
			{
				_pagedRect.UpdatePagination();
			}
		}

		public PagedRect GetPagedRect()
		{
			return _pagedRect;
		}
	}
}
