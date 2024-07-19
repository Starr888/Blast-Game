using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Pagination
{
	[ExecuteInEditMode]
	public class PagedRect : MonoBehaviour
	{
		[Serializable]
		public class PageChangedEventType : UnityEvent<Page, Page>
		{
		}

		public enum eAnimationType
		{
			None = 0,
			SlideHorizontal = 1,
			SlideVertical = 2,
			Fade = 3
		}

		public enum eDirection
		{
			Left = 0,
			Right = 1
		}

		public enum DeltaDirection
		{
			None = 0,
			Next = 1,
			Previous = 2
		}

		public int DefaultPage;

		[Header("Pagination")]
		[Tooltip("If this is set to false, the page buttons will not be shown.")]
		public bool ShowPagination = true;

		public bool ShowFirstAndLastButtons = true;

		public bool ShowPreviousAndNextButtons = true;

		[Tooltip("If there are too many page buttons to show at once, use this field to limit the number of visible buttons. 0 == No Limit")]
		public int MaximumNumberOfButtonsToShow = 15;

		[Tooltip("Set this to false to hide the button templates in edit mode")]
		public bool ShowButtonTemplatesInEditor = true;

		public bool ShowPageButtons = true;

		public bool ShowNumbersOnButtons = true;

		public bool ShowPageTitlesOnButtons;

		[Header("Legacy (Non-ScrollRect) Animation")]
		public eAnimationType AnimationType = eAnimationType.SlideHorizontal;

		protected eAnimationType previousAnimationTypeValue;

		[Range(0.1f, 5f)]
		public float AnimationSpeed = 1f;

		[Header("Automation")]
		public bool AutomaticallyMoveToNextPage;

		public float DelayBetweenPages = 5f;

		public bool LoopEndlessly = true;

		protected float _timeSinceLastPage;

		[Header("New Page Template")]
		[Tooltip("Optional Template for adding new pages dynamically at runtime.")]
		public Page NewPageTemplate;

		[Header("Keyboard Input")]
		public bool UseKeyboardInput;

		public KeyCode PreviousPageKey = KeyCode.LeftArrow;

		public KeyCode NextPageKey = KeyCode.RightArrow;

		public KeyCode FirstPageKey = KeyCode.Home;

		public KeyCode LastPageKey = KeyCode.End;

		[Header("Legacy (Non-ScrollRect) Input")]
		public bool UseSwipeInput = true;

		[Header("ScrollRect")]
		public bool UseSwipeInputForScrollRect = true;

		public float SwipeDeltaThreshold = 0.1f;

		public float SpaceBetweenPages;

		public bool LoopSeamlessly;

		public bool ShowScrollBar;

		[Header("Scroll Wheel Input")]
		public bool UseScrollWheelInput;

		public bool OnlyUseScrollWheelInputWhenMouseIsOver = true;

		[Header("Highlight")]
		public bool HighlightWhenMouseIsOver;

		public Color NormalColor = new Color(1f, 1f, 1f);

		public Color HighlightColor = new Color(0.9f, 0.9f, 0.9f);

		protected bool mouseIsOverPagedRect;

		[Header("Events")]
		public PageChangedEventType PageChangedEvent = new PageChangedEventType();

		[Header("Page Previews")]
		public bool ShowPagePreviews;

		public float PagePreviewScale = 0.25f;

		public bool LockOneToOneScaleRatio = true;

		public bool EnablePagePreviewOverlays = true;

		public Sprite PagePreviewOverlayImage;

		public Color PagePreviewOverlayNormalColor;

		public Color PagePreviewOverlayHoverColor;

		public float PagePreviewOverlayScaleOverride = 1f;

		private Vector3 m_currentPageSize = Vector3.zero;

		private Vector3 m_otherPageSize = Vector3.zero;

		private Vector3 m_currentPageScale = Vector3.zero;

		private Vector3 m_otherPageScale = Vector3.zero;

		[Header("References")]
		public PagedRect_ScrollRect ScrollRect;

		public GameObject ScrollRectViewport;

		public GameObject Viewport;

		public GameObject Pagination;

		public PaginationButton ButtonTemplate_CurrentPage;

		public PaginationButton ButtonTemplate_OtherPages;

		public PaginationButton ButtonTemplate_DisabledPage;

		public PaginationButton Button_PreviousPage;

		public PaginationButton Button_NextPage;

		public PaginationButton Button_FirstPage;

		public PaginationButton Button_LastPage;

		public RuntimeAnimatorController AnimationControllerTemplate;

		public List<Page> Pages = new List<Page>();

		public int editorSelectedPage = 1;

		public RectTransform sizingTransform;

		protected List<Page> _pages = new List<Page>();

		private PointerEventData lastEndDragData;

		private float dragStartTime;

		private int currentPageBeforeDragStarted = 1;

		private List<Page> pageCollection = new List<Page>();

		private Dictionary<PaginationButton.eButtonType, List<PaginationButton>> buttonPool = new Dictionary<PaginationButton.eButtonType, List<PaginationButton>>();

		private MobileInput _MobileInput;

		private ScrollWheelInput _ScrollWheelInput;

		private Image _imageComponent;

		private HorizontalOrVerticalLayoutGroup _layoutGroup;

		private bool? _UsingScrollRect;

		protected Vector2 _ScrollRectPosition = default(Vector2);

		[NonSerialized]
		protected bool firstPageSet;

		protected List<KeyValuePair<double, Action>> delayedEditorActions = new List<KeyValuePair<double, Action>>();

		private Vector2 scrollRectAnimation_InitialPosition = Vector2.zero;

		private Vector2 scrollRectAnimation_DesiredPosition = Vector2.zero;

		private Coroutine scrollCoroutine;

		private Canvas m_canvas;

		[NonSerialized]
		private int _numberOfPages;

		public bool isDirty { get; set; }

		public int CurrentPage { get; protected set; }

		public int NumberOfPages
		{
			get
			{
				return Pages.Count;
			}
		}

		public MobileInput MobileInput
		{
			get
			{
				if (_MobileInput == null)
				{
					_MobileInput = GetComponent<MobileInput>();
					if (_MobileInput == null && Application.isPlaying)
					{
						_MobileInput = base.gameObject.AddComponent<MobileInput>();
						if (AnimationType == eAnimationType.SlideHorizontal)
						{
							_MobileInput.OnSwipeLeft = NextPage;
							_MobileInput.OnSwipeRight = PreviousPage;
						}
						else if (AnimationType == eAnimationType.SlideVertical)
						{
							_MobileInput.OnSwipeUp = NextPage;
							_MobileInput.OnSwipeDown = PreviousPage;
						}
						else
						{
							_MobileInput.OnSwipeLeft = (_MobileInput.OnSwipeUp = delegate
							{
								NextPage();
							});
							_MobileInput.OnSwipeRight = (_MobileInput.OnSwipeDown = delegate
							{
								PreviousPage();
							});
						}
					}
				}
				return _MobileInput;
			}
		}

		public ScrollWheelInput ScrollWheelInput
		{
			get
			{
				if (_ScrollWheelInput == null)
				{
					_ScrollWheelInput = GetComponent<ScrollWheelInput>();
					if (_ScrollWheelInput == null && Application.isPlaying)
					{
						_ScrollWheelInput = base.gameObject.AddComponent<ScrollWheelInput>();
						_ScrollWheelInput.OnScrollUp = delegate
						{
							ScrollWheelUp();
						};
						_ScrollWheelInput.OnScrollDown = delegate
						{
							ScrollWheelDown();
						};
					}
				}
				return _ScrollWheelInput;
			}
		}

		protected Image imageComponent
		{
			get
			{
				if (_imageComponent == null)
				{
					_imageComponent = GetComponent<Image>();
				}
				return _imageComponent;
			}
		}

		protected HorizontalOrVerticalLayoutGroup layoutGroup
		{
			get
			{
				if (_layoutGroup == null && Viewport != null)
				{
					_layoutGroup = Viewport.GetComponent<HorizontalOrVerticalLayoutGroup>();
				}
				return _layoutGroup;
			}
		}

		public bool UsingScrollRect
		{
			get
			{
				if (!_UsingScrollRect.HasValue)
				{
					_UsingScrollRect = ScrollRect != null;
				}
				return _UsingScrollRect.Value;
			}
		}

		private Canvas canvas
		{
			get
			{
				if (m_canvas == null)
				{
					m_canvas = GetComponentInParent<Canvas>();
				}
				return m_canvas;
			}
		}

		protected void PageEnterAnimation(Page page, eDirection direction, bool initial = false)
		{
            eAnimationType eAnimationType = ((!page.UsePageAnimationType) ? AnimationType : page.AnimationType);
            if (!Application.isPlaying || AnimationType == eAnimationType.None || initial)
			{
				page.gameObject.SetActive(true);
				return;
			}
			
			switch (eAnimationType)
			{
			case eAnimationType.Fade:
				page.FadeIn();
				break;
			case eAnimationType.SlideHorizontal:
			case eAnimationType.SlideVertical:
				if (page.FlipAnimationDirection)
				{
					direction = ((direction == eDirection.Left) ? eDirection.Right : eDirection.Left);
				}
				page.SlideIn(direction, eAnimationType == eAnimationType.SlideVertical);
				break;
			}
		}

		protected void PageExitAnimation(Page page, eDirection direction)
		{
            eAnimationType eAnimationType = ((!page.UsePageAnimationType) ? AnimationType : page.AnimationType);
            if (!Application.isPlaying || AnimationType == eAnimationType.None)
			{
				page.gameObject.SetActive(false);
				return;
			}
			
			switch (eAnimationType)
			{
			case eAnimationType.Fade:
				page.FadeOut();
				break;
			case eAnimationType.SlideHorizontal:
			case eAnimationType.SlideVertical:
				if (page.FlipAnimationDirection)
				{
					direction = ((direction == eDirection.Left) ? eDirection.Right : eDirection.Left);
				}
				page.SlideOut(direction, eAnimationType == eAnimationType.SlideVertical);
				break;
			}
		}

		public void OnBeginDrag(PointerEventData data)
		{
			if (UsingScrollRect)
			{
				dragStartTime = Time.time;
				currentPageBeforeDragStarted = CurrentPage;
			}
		}

		public void OnEndDrag(PointerEventData data)
		{
			if (!UsingScrollRect || lastEndDragData == data)
			{
				return;
			}
			lastEndDragData = data;
			if (LoopSeamlessly && !ShowPagePreviews)
			{
				int pagePosition = GetPagePosition(CurrentPage);
				if (pagePosition == 1 || pagePosition == NumberOfPages)
				{
					DeltaDirection dragDeltaDirection = GetDragDeltaDirection(data);
					if (dragDeltaDirection == DeltaDirection.Next && pagePosition == NumberOfPages)
					{
						MoveFirstPageToEnd();
						PreviousPage();
					}
					else if (dragDeltaDirection == DeltaDirection.Previous && pagePosition == 1)
					{
						MoveLastPageToStart();
						NextPage();
					}
				}
				else
				{
					UpdateSeamlessPagePositions();
				}
			}
			if (!UseSwipeInputForScrollRect || !(Mathf.Abs(Time.time - dragStartTime) <= 0.25f) || !HandleDragDelta(data))
			{
				CurrentPage = currentPageBeforeDragStarted;
				ScrollToClosestPage();
			}
		}

		protected void ScrollToClosestPage()
		{
			Dictionary<int, float> pageDistancesFromScrollRectCenter = GetPageDistancesFromScrollRectCenter();
			if (pageDistancesFromScrollRectCenter.Any())
			{
				int key = pageDistancesFromScrollRectCenter.OrderBy((KeyValuePair<int, float> p) => p.Value).First().Key;
				SetCurrentPage(key);
			}
		}

		protected DeltaDirection GetDragDeltaDirection(PointerEventData data)
		{
			bool flag = false;
			bool flag2 = false;
			if (ScrollRect.horizontal)
			{
				if (Mathf.Abs(data.delta.y) > Mathf.Abs(data.delta.x))
				{
					return DeltaDirection.None;
				}
				if (data.delta.x * 2f > SwipeDeltaThreshold)
				{
					flag2 = true;
				}
				else if (data.delta.x / 2f < 0f - SwipeDeltaThreshold)
				{
					flag = true;
				}
				else
				{
					flag2 = _ScrollRectPosition.x < 0f;
					flag = _ScrollRectPosition.x > 1f;
				}
			}
			else if (ScrollRect.vertical)
			{
				if (Mathf.Abs(data.delta.x) > Mathf.Abs(data.delta.y))
				{
					return DeltaDirection.None;
				}
				if (data.delta.y > SwipeDeltaThreshold)
				{
					flag2 = true;
				}
				else if (data.delta.y < 0f - SwipeDeltaThreshold)
				{
					flag = true;
				}
				else
				{
					flag2 = _ScrollRectPosition.y < 0f;
					flag = _ScrollRectPosition.y > 1f;
				}
			}
			if (flag)
			{
				return DeltaDirection.Next;
			}
			if (flag2)
			{
				return DeltaDirection.Previous;
			}
			return DeltaDirection.None;
		}

		internal bool HandleDragDelta(PointerEventData data)
		{
			DeltaDirection dragDeltaDirection = GetDragDeltaDirection(data);
			return HandleDragDelta(dragDeltaDirection);
		}

		protected bool HandleDragDelta(DeltaDirection deltaDirection)
		{
			switch (deltaDirection)
			{
			case DeltaDirection.Next:
				NextPage();
				return true;
			case DeltaDirection.Previous:
				PreviousPage();
				return true;
			default:
				return false;
			}
		}

		public void OnDrag(PointerEventData data)
		{
			if (!UsingScrollRect)
			{
				return;
			}
			if (ShowPagePreviews)
			{
				HandleDrag_PagePreviews();
				return;
			}
			int closestPageNumberToScrollRectCenter = GetClosestPageNumberToScrollRectCenter();
			if (CurrentPage != closestPageNumberToScrollRectCenter)
			{
				CurrentPage = closestPageNumberToScrollRectCenter;
				UpdatePagination();
				UpdateScrollBarPosition();
				UpdateSeamlessPagePositions();
			}
		}

		protected void ShowHighlight()
		{
			imageComponent.color = HighlightColor;
		}

		protected void ClearHighlight()
		{
			imageComponent.color = NormalColor;
		}

		private void HandleKeyboardInput()
		{
			if (Input.GetKeyDown(NextPageKey))
			{
				NextPage();
			}
			else if (Input.GetKeyDown(PreviousPageKey))
			{
				PreviousPage();
			}
			else if (Input.GetKeyDown(FirstPageKey))
			{
				ShowFirstPage();
			}
			else if (Input.GetKeyDown(LastPageKey))
			{
				ShowLastPage();
			}
		}

		public void UpgradeLayoutGroupIfNecessary()
		{
			if (UsingScrollRect)
			{
				LayoutGroup component = ScrollRect.content.GetComponent<LayoutGroup>();
				if (!(component is PagedRect_LayoutGroup))
				{
					RemoveLayoutGroup(component);
					AddPagedRectLayoutGroup();
				}
				else
				{
					((PagedRect_LayoutGroup)component).pagedRect = this;
				}
			}
		}

		private void AddPagedRectLayoutGroup()
		{
			PagedRect_LayoutGroup pagedRect_LayoutGroup = ScrollRect.content.gameObject.AddComponent<PagedRect_LayoutGroup>();
			pagedRect_LayoutGroup.pagedRect = this;
			pagedRect_LayoutGroup.Axis = ((!ScrollRect.horizontal) ? RectTransform.Axis.Vertical : RectTransform.Axis.Horizontal);
		}

		private void RemoveLayoutGroup(LayoutGroup layoutGroup)
		{
			UnityEngine.Object.DestroyImmediate(layoutGroup);
		}

		protected void OnMouseOver()
		{
			if (HighlightWhenMouseIsOver)
			{
				ShowHighlight();
			}
		}

		protected void OnMouseExit()
		{
			if (HighlightWhenMouseIsOver)
			{
				ClearHighlight();
			}
		}

		private void SetupMouseEvents()
		{
			EventTrigger eventTrigger = base.gameObject.AddComponent<EventTrigger>();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerEnter;
			entry.callback = new EventTrigger.TriggerEvent();
			EventTrigger.Entry entry2 = entry;
			entry2.callback.AddListener(delegate
			{
				mouseIsOverPagedRect = true;
				OnMouseOver();
			});
			entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerExit;
			entry.callback = new EventTrigger.TriggerEvent();
			EventTrigger.Entry entry3 = entry;
			entry3.callback.AddListener(delegate
			{
				mouseIsOverPagedRect = false;
				OnMouseExit();
			});
			eventTrigger.triggers.Add(entry2);
			eventTrigger.triggers.Add(entry3);
		}

		public void AddPage(Page page)
		{
			if (UsingScrollRect)
			{
				page.gameObject.SetActive(true);
			}
			page.transform.SetParent(Viewport.transform);
			page.transform.localPosition = Vector3.zero;
			page.transform.localScale = Vector3.one;
			RectTransform rectTransform = (RectTransform)page.transform;
			rectTransform.offsetMax = Vector2.zero;
			rectTransform.offsetMin = Vector2.zero;
			rectTransform.sizeDelta = Vector2.zero;
			page.ShowOnPagination = true;
			isDirty = true;
			UpdateDisplay();
			if (UsingScrollRect)
			{
				StartCoroutine(DelayedCall(0.1f, delegate
				{
					CenterScrollRectOnCurrentPage(true);
				}));
			}
		}

		public Page AddPageUsingTemplate()
		{
			if (NewPageTemplate == null)
			{
				throw new UnityException("Attempted to use PagedRect.AddPageUsingTemplate(), but this PagedRect instance has no NewPageTemplate set!");
			}
			Page page = UnityEngine.Object.Instantiate(NewPageTemplate);
			AddPage(page);
			page.name = "Page " + NumberOfPages;
			return page;
		}

		public void RemovePage(Page page, bool destroyPageObject = false)
		{
			if (!Pages.Contains(page))
			{
				return;
			}
			page.ShowOnPagination = false;
			Pages.Remove(page);
			page.gameObject.SetActive(false);
			if (destroyPageObject)
			{
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(page.gameObject);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(page.gameObject);
				}
			}
			else if (UsingScrollRect)
			{
				page.gameObject.SetActive(false);
			}
			isDirty = true;
			UpdatePages();
		}

		public void RemovePage(int pageNumber, bool destroyPageObject = false)
		{
			RemovePage(GetPageByNumber(pageNumber), destroyPageObject);
		}

		public void RemoveAllPages(bool destroyPageObjects = false)
		{
			SetCurrentPage(1);
			List<Page> list = Pages.ToList();
			foreach (Page item in list)
			{
				RemovePage(item, destroyPageObjects);
			}
		}

		public void SetEditorSelectedPage(int page)
		{
			editorSelectedPage = page;
		}

		public Page GetPageByNumber(int pageNumber, bool secondAttempt = false, bool allowNulls = false)
		{
			Page page = Pages.FirstOrDefault((Page p) => p.PageNumber == pageNumber);
			if (page == null && !secondAttempt && !allowNulls)
			{
				UpdatePages();
				return GetPageByNumber(pageNumber, true);
			}
			return page;
		}

		public Page GetCurrentPage()
		{
			return GetPageByNumber(CurrentPage);
		}

		public int GetPageNumber(Page page)
		{
			if (page.PageNumber == -1)
			{
				UpdatePages();
			}
			return page.PageNumber;
		}

		protected int GetPagePosition(int PageNumber)
		{
			Page pageByNumber = GetPageByNumber(CurrentPage);
			return GetPagePosition(pageByNumber);
		}

		protected int GetPagePosition(Page page)
		{
			int num = Pages.IndexOf(page);
			return num + 1;
		}

		private void MonitorPageCollection()
		{
			if (!UsingScrollRect)
			{
				return;
			}
			List<Page> list = new List<Page>();
			foreach (RectTransform item in ScrollRect.content)
			{
				if (item.gameObject.activeInHierarchy)
				{
					Page component = item.GetComponent<Page>();
					if (component != null)
					{
						list.Add(component);
					}
				}
			}
			list = list.OrderBy((Page p) => p.PageNumber).ToList();
			if (!pageCollection.SequenceEqual(list))
			{
				UpdatePages(true, !Application.isPlaying);
				UpdateDisplay();
				pageCollection = list;
				if (ShowPagePreviews)
				{
					HandlePagePreviewScaling();
				}
			}
		}

		public void UpdatePagination()
		{
			List<PaginationButton> list = (from pb in GetComponentsInChildren<PaginationButton>(true)
				where !pb.DontUpdate
				where pb != ButtonTemplate_CurrentPage && pb != ButtonTemplate_OtherPages
				where pb.transform.parent == Pagination.transform
				select pb).ToList();
			list.ForEach(delegate(PaginationButton pb)
			{
				FreeButton(pb);
			});
			if (!ShowPagination || !ShowPageButtons)
			{
				return;
			}
			List<int> list2 = (from p in Pages
				where p.ShowOnPagination
				select p.PageNumber into p
				orderby p
				select p).ToList();
			List<int> list3 = list2;
			if (MaximumNumberOfButtonsToShow != 0 && list2.Count > MaximumNumberOfButtonsToShow)
			{
				int num = list2.IndexOf(CurrentPage);
				int num2 = (int)Math.Floor((float)MaximumNumberOfButtonsToShow / 2f);
				List<int> list4 = new List<int>();
				list4.Add(CurrentPage);
				List<int> list5 = list4;
				int num3 = 0;
				if (num > 0)
				{
					int num4 = num2;
					int num5 = num - 1;
					while (num5 <= num && num5 >= 0)
					{
						list5.Insert(0, list2[num5]);
						num3 = num5;
						num4--;
						if (num4 <= 0)
						{
							break;
						}
						num5--;
					}
				}
				int num6 = MaximumNumberOfButtonsToShow - list5.Count;
				for (int i = 1; i <= num6; i++)
				{
					int num7 = num + i;
					if (num7 >= list2.Count)
					{
						break;
					}
					list5.Add(list2[num + i]);
				}
				int num8 = num3 - 1;
				while (num8 > 0 && list5.Count < MaximumNumberOfButtonsToShow)
				{
					list5.Insert(0, list2[num8]);
					num8--;
				}
				list3 = list5;
			}
			int num9 = 0;
			foreach (int item in list3)
			{
				int _pageNumber = item;
				Page pageByNumber = GetPageByNumber(item, false, true);
				if (pageByNumber == null)
				{
					PageWasDeleted(item);
					return;
				}
				if (!pageByNumber.ShowOnPagination)
				{
					continue;
				}
				PaginationButton.eButtonType buttonType = ((item != CurrentPage) ? PaginationButton.eButtonType.OtherPages : PaginationButton.eButtonType.CurrentPage);
				if (!pageByNumber.PageEnabled)
				{
					buttonType = PaginationButton.eButtonType.DisabledPage;
				}
				PaginationButton buttonFromPool = GetButtonFromPool(buttonType);
				buttonFromPool.Button.onClick.RemoveAllListeners();
				if (pageByNumber.PageEnabled)
				{
					buttonFromPool.Button.onClick.AddListener(delegate
					{
						SetCurrentPage(_pageNumber);
					});
				}
				buttonFromPool.gameObject.transform.SetParent(Pagination.transform, false);
				buttonFromPool.transform.SetSiblingIndex(num9);
				string text = string.Empty;
				if (ShowNumbersOnButtons)
				{
					text = item.ToString();
					if (ShowPageTitlesOnButtons)
					{
						text += ". ";
					}
				}
				if (ShowPageTitlesOnButtons)
				{
					text += pageByNumber.PageTitle;
				}
				buttonFromPool.SetText(text);
				buttonFromPool.gameObject.name = string.Format("Button - Page {0} {1}", item, (item != CurrentPage) ? string.Empty : "(Current Page)");
				buttonFromPool.DontUpdate = false;
				buttonFromPool.gameObject.SetActive(true);
				num9++;
			}
			Button_PreviousPage.gameObject.transform.SetAsFirstSibling();
			Button_FirstPage.gameObject.transform.SetAsFirstSibling();
			Button_NextPage.gameObject.transform.SetAsLastSibling();
			Button_LastPage.gameObject.transform.SetAsLastSibling();
		}

		private void ToggleTemplateButtons(bool show)
		{
			if (ButtonTemplate_CurrentPage != null)
			{
				ButtonTemplate_CurrentPage.gameObject.SetActive(show);
			}
			if (ButtonTemplate_OtherPages != null)
			{
				ButtonTemplate_OtherPages.gameObject.SetActive(show);
			}
			if (ButtonTemplate_DisabledPage != null)
			{
				ButtonTemplate_DisabledPage.gameObject.SetActive(show);
			}
		}

		private void ToggleFirstAndLastButtons(bool show)
		{
			if (Button_FirstPage != null)
			{
				Button_FirstPage.gameObject.SetActive(show);
			}
			if (Button_LastPage != null)
			{
				Button_LastPage.gameObject.SetActive(show);
			}
		}

		private void TogglePreviousAndNextButtons(bool show)
		{
			if (Button_NextPage != null)
			{
				Button_NextPage.gameObject.SetActive(show);
			}
			if (Button_PreviousPage != null)
			{
				Button_PreviousPage.gameObject.SetActive(show);
			}
		}

		private void OptimizePagination()
		{
			if (!(Pagination == null))
			{
				Canvas component = Pagination.GetComponent<Canvas>();
				if (component == null)
				{
					Pagination.AddComponent<Canvas>();
					Pagination.AddComponent<GraphicRaycaster>();
				}
			}
		}

		public void InvalidateButtonPool()
		{
			buttonPool.Clear();
			List<PaginationButton> list = (from pb in GetComponentsInChildren<PaginationButton>(true)
				where !pb.DontUpdate
				where pb != ButtonTemplate_CurrentPage && pb != ButtonTemplate_OtherPages
				where pb.transform.parent == Pagination.transform
				select pb).ToList();
			list.ForEach(delegate(PaginationButton pb)
			{
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(pb.gameObject);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(pb.gameObject);
				}
			});
		}

		private PaginationButton GetButtonFromPool(PaginationButton.eButtonType buttonType)
		{
			PaginationButton paginationButton = null;
			if (Application.isPlaying)
			{
				if (!buttonPool.ContainsKey(buttonType))
				{
					buttonPool.Add(buttonType, new List<PaginationButton>());
				}
				paginationButton = buttonPool[buttonType].FirstOrDefault((PaginationButton b) => !b.gameObject.activeSelf);
			}
			if (paginationButton == null)
			{
				PaginationButton original = null;
				switch (buttonType)
				{
				case PaginationButton.eButtonType.CurrentPage:
					original = ButtonTemplate_CurrentPage;
					break;
				case PaginationButton.eButtonType.OtherPages:
					original = ButtonTemplate_OtherPages;
					break;
				case PaginationButton.eButtonType.DisabledPage:
					original = ButtonTemplate_DisabledPage ?? ButtonTemplate_OtherPages;
					break;
				}
				paginationButton = UnityEngine.Object.Instantiate(original);
				if (Application.isPlaying)
				{
					buttonPool[buttonType].Add(paginationButton);
				}
			}
			return paginationButton;
		}

		private void FreeButton(PaginationButton button)
		{
			button.gameObject.SetActive(false);
			button.Button.onClick.RemoveAllListeners();
			if (!Application.isPlaying)
			{
				UnityEngine.Object.DestroyImmediate(button.gameObject);
			}
		}

		public void ScrollBarValueChanged()
		{
			if (UsingScrollRect && !(ScrollRect.ScrollBar == null) && NumberOfPages > 0)
			{
				float num = 1f / (float)(NumberOfPages - 1);
				int num2 = Mathf.RoundToInt(ScrollRect.ScrollBar.value / num);
				Page pageByNumber = GetPageByNumber(num2 + 1);
				if (pageByNumber.PageNumber != CurrentPage)
				{
					SetCurrentPage(pageByNumber);
				}
			}
		}

		private void UpdateScrollBar()
		{
			if (!UsingScrollRect || ScrollRect.ScrollBar == null)
			{
				return;
			}
			if (ShowScrollBar)
			{
				if (!ScrollRect.ScrollBar.gameObject.activeInHierarchy)
				{
					ScrollRect.ScrollBar.gameObject.SetActive(true);
				}
			}
			else if (ScrollRect.ScrollBar.gameObject.activeInHierarchy)
			{
				ScrollRect.ScrollBar.gameObject.SetActive(false);
			}
			ScrollRect.ScrollBar.numberOfSteps = NumberOfPages;
			ScrollRect.ScrollBar.size = 1f / (float)NumberOfPages;
		}

		private void UpdateScrollBarPosition()
		{
			if (UsingScrollRect && !(ScrollRect.ScrollBar == null) && ScrollRect.ScrollBar.gameObject.activeInHierarchy && NumberOfPages > 1)
			{
				float num = 1f / (float)(NumberOfPages - 1);
				float value = (float)(CurrentPage - 1) * num;
				ScrollRect.ScrollBar.value = value;
			}
		}

		protected void ScrollRectValueChanged(Vector2 newPosition)
		{
			_ScrollRectPosition = newPosition;
			bool flag = !ScrollRect.DisableDragging;
			if (!ShowPagePreviews || flag)
			{
				UpdateSeamlessPagePositions();
			}
		}

		private void ScrollWheelUp()
		{
			HandleScrollWheel(DeltaDirection.Next);
		}

		private void ScrollWheelDown()
		{
			HandleScrollWheel(DeltaDirection.Previous);
		}

		private void HandleScrollWheel(DeltaDirection direction)
		{
			bool flag = false;
			if (LoopSeamlessly && !ShowPagePreviews)
			{
				int pagePosition = GetPagePosition(CurrentPage);
				if (pagePosition == 1 || pagePosition == NumberOfPages)
				{
					switch (direction)
					{
					case DeltaDirection.Next:
						MoveFirstPageToEnd();
						NextPage();
						flag = true;
						break;
					case DeltaDirection.Previous:
						MoveLastPageToStart();
						PreviousPage();
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				switch (direction)
				{
				case DeltaDirection.Next:
					NextPage();
					break;
				case DeltaDirection.Previous:
					PreviousPage();
					break;
				}
			}
		}

		protected void RecalculateDesiredPageSizes()
		{
			if (!ShowPagePreviews)
			{
				return;
			}
			float num = 1f - PagePreviewScale * 2f;
			m_currentPageSize = new Vector3(sizingTransform.rect.width * num, sizingTransform.rect.height * num, 1f);
			if (ScrollRect.horizontal)
			{
				m_currentPageSize.x -= SpaceBetweenPages * 2f;
			}
			else
			{
				m_currentPageSize.y -= SpaceBetweenPages * 2f;
			}
			m_otherPageSize = new Vector3(sizingTransform.rect.width * PagePreviewScale, sizingTransform.rect.height * PagePreviewScale, 1f);
			m_currentPageScale = new Vector3(num, num, 1f);
			float num2 = SpaceBetweenPages / ((!ScrollRect.horizontal) ? sizingTransform.rect.height : sizingTransform.rect.width);
			if (ScrollRect.horizontal)
			{
				m_currentPageScale.x -= num2 * 2f;
				if (LockOneToOneScaleRatio)
				{
					m_currentPageScale.y = m_currentPageScale.x;
				}
			}
			else
			{
				m_currentPageScale.y -= num2 * 2f;
				if (LockOneToOneScaleRatio)
				{
					m_currentPageScale.x = m_currentPageScale.y;
				}
			}
			m_otherPageScale = new Vector3(PagePreviewScale, PagePreviewScale, 1f);
		}

		protected float GetDesiredScrollRectOffset_PagePreviews()
		{
			return GetPageOffset_PagePreviews(GetCurrentPage());
		}

		public float GetPageOffset_PagePreviews(Page page)
		{
			float num = 0f;
			int pagePosition = GetPagePosition(page);
			int pagePosition2 = GetPagePosition(CurrentPage);
			int num2 = pagePosition - 1;
			if (pagePosition2 < num2)
			{
				num2--;
				if (ScrollRect.horizontal)
				{
					num -= m_currentPageSize.x + SpaceBetweenPages;
				}
			}
			if (ScrollRect.horizontal)
			{
				num -= m_otherPageSize.x * (float)num2 + SpaceBetweenPages * (float)num2;
				return num + (m_otherPageSize.x + SpaceBetweenPages);
			}
			num += m_otherPageSize.y * (float)num2 + SpaceBetweenPages * (float)num2;
			return num - (m_otherPageSize.y + SpaceBetweenPages);
		}

		protected void HandlePagePreviewPreferredSizes()
		{
			RecalculateDesiredPageSizes();
			HorizontalOrVerticalLayoutGroup component = Viewport.GetComponent<HorizontalOrVerticalLayoutGroup>();
			if (component != null)
			{
				component.childAlignment = TextAnchor.MiddleCenter;
			}
			Page currentPage = GetCurrentPage();
			bool changed = false;
			Pages.ForEach(delegate(Page page)
			{
				LayoutElement component2 = page.GetComponent<LayoutElement>();
				if (page == currentPage)
				{
					page.rectTransform.localScale = m_currentPageScale;
				}
				else
				{
					page.rectTransform.localScale = m_otherPageScale;
				}
				component2.preferredWidth = sizingTransform.rect.width;
				component2.preferredHeight = sizingTransform.rect.height;
				changed = changed || page.DesiredScale != page.rectTransform.localScale;
				page.DesiredScale = page.rectTransform.localScale;
				if (changed)
				{
					LayoutRebuilder.MarkLayoutForRebuild(page.rectTransform);
				}
			});
		}

		protected void HandlePagePreviewScaling()
		{
			RecalculateDesiredPageSizes();
			Page currentPage = GetCurrentPage();
			foreach (Page page in Pages)
			{
				if (!(page == currentPage))
				{
					page.ScaleToScale(m_otherPageScale, AnimationSpeed);
				}
			}
			if (currentPage != null)
			{
				currentPage.ScaleToScale(m_currentPageScale, AnimationSpeed);
			}
		}

		protected void HandleDrag_PagePreviews()
		{
			int closestPageNumberToScrollRectCenter = GetClosestPageNumberToScrollRectCenter();
			if (closestPageNumberToScrollRectCenter != CurrentPage)
			{
				CurrentPage = closestPageNumberToScrollRectCenter;
				UpdatePagination();
				UpdateScrollBarPosition();
				HandlePagePreviewScaling();
			}
		}

		public void CenterScrollRectOnCurrentPage(bool initial = false)
		{
			if (NumberOfPages == 0)
			{
				return;
			}
			ScrollRect.ResetDragOffset = true;
			if (Application.isPlaying && !initial)
			{
				if (scrollCoroutine != null)
				{
					StopCoroutine(scrollCoroutine);
				}
				scrollCoroutine = StartCoroutine(ScrollToDesiredPosition());
			}
			else
			{
				SetScrollRectPosition();
			}
		}

		protected void SetScrollRectPosition()
		{
			if (ShowPagePreviews)
			{
				HandlePagePreviewPreferredSizes();
			}
			float num = ((NumberOfPages <= 0) ? 0f : GetDesiredScrollRectOffset());
			if (ScrollRect.horizontal)
			{
				ScrollRect.content.anchoredPosition = new Vector2(num, 0f);
			}
			else
			{
				ScrollRect.content.anchoredPosition = new Vector2(0f, num);
			}
		}

		protected IEnumerator ScrollToDesiredPosition()
		{
			float percentageComplete = 0f;
			if (ShowPagePreviews)
			{
				HandlePagePreviewScaling();
			}
			float offset = GetDesiredScrollRectOffset();
			scrollRectAnimation_DesiredPosition = Vector2.zero;
			scrollRectAnimation_InitialPosition = ScrollRect.content.anchoredPosition;
			if (ScrollRect.horizontal)
			{
				scrollRectAnimation_DesiredPosition.x = offset;
				scrollRectAnimation_InitialPosition.y = 0f;
			}
			else
			{
				scrollRectAnimation_DesiredPosition.y = offset;
				scrollRectAnimation_InitialPosition.x = 0f;
			}
			float timeStartedMoving = Time.time;
			while (percentageComplete < 1f)
			{
				float timeSinceStarted = Time.time - timeStartedMoving;
				percentageComplete = timeSinceStarted / (0.25f / AnimationSpeed);
				ScrollRect.content.anchoredPosition = Vector2.Lerp(scrollRectAnimation_InitialPosition, scrollRectAnimation_DesiredPosition, percentageComplete);
				yield return null;
			}
			ScrollRect.content.anchoredPosition = scrollRectAnimation_DesiredPosition;
		}

		protected int GetClosestPageNumberToScrollRectCenter()
		{
			return (from d in GetPageDistancesFromScrollRectCenter()
				orderby d.Value
				select d).FirstOrDefault().Key;
		}

		protected Dictionary<int, float> GetPageDistancesFromScrollRectCenter()
		{
			Dictionary<int, float> dictionary = new Dictionary<int, float>();
			RectTransform rectTransform = Viewport.transform as RectTransform;
			int childCount = rectTransform.childCount;
			int num = 0;
			float num2 = ((!ScrollRect.horizontal) ? sizingTransform.rect.height : sizingTransform.rect.width);
			float num3 = num2 / 2f;
			float num4 = 0f;
			float num5 = Math.Abs((!ScrollRect.horizontal) ? ScrollRect.content.transform.localPosition.y : ScrollRect.content.transform.localPosition.x);
			for (int i = 0; i < childCount; i++)
			{
				Transform child = rectTransform.GetChild(i);
				if (child.gameObject.activeInHierarchy)
				{
					Page component = child.GetComponent<Page>();
					if (!(component == null))
					{
						float num6 = ((!ScrollRect.horizontal) ? component.transform.localScale.y : component.transform.localScale.x);
						dictionary.Add(component.PageNumber, Mathf.Abs(num5 - num4 - num3 * num6));
						num++;
						num4 += num2 * num6 + SpaceBetweenPages;
					}
				}
			}
			return dictionary;
		}

		protected float GetDesiredScrollRectOffset()
		{
			return GetPageOffset(GetCurrentPage());
		}

		public float GetPageOffset(Page page)
		{
			if (ShowPagePreviews)
			{
				return GetPageOffset_PagePreviews(page);
			}
			float num = 0f;
			int num2 = GetPagePosition(page.PageNumber) - 1;
			Rect rect = sizingTransform.rect;
			if (ScrollRect.horizontal)
			{
				return num - (rect.width + SpaceBetweenPages) * (float)num2;
			}
			return num + (rect.height + SpaceBetweenPages) * (float)num2;
		}

		private void UpdateSeamlessPagePositions()
		{
			if (!Application.isPlaying || !LoopSeamlessly || NumberOfPages <= 3)
			{
				return;
			}
			if (ShowPagePreviews)
			{
				UpdateSeamlessPagePositions_PagePreviews();
				return;
			}
			float num = ScrollRect.GetPageSize();
			float totalSize = ScrollRect.GetTotalSize();
			float offset = ScrollRect.GetOffset();
			if (NumberOfPages > 3)
			{
				num *= 1.5f;
			}
			if (offset <= num)
			{
				MoveLastPageToStart();
			}
			else if (offset >= totalSize - num)
			{
				MoveFirstPageToEnd();
			}
		}

		private void MoveFirstPageToEnd()
		{
			Dictionary<int, float> pageDistancesFromScrollRectCenter = GetPageDistancesFromScrollRectCenter();
			int key = pageDistancesFromScrollRectCenter.First().Key;
			Page pageByNumber = GetPageByNumber(key);
			pageByNumber.transform.SetAsLastSibling();
			AdjustScrollPositionAfterPageMoved(eDirection.Right);
		}

		private void MoveLastPageToStart()
		{
			Dictionary<int, float> pageDistancesFromScrollRectCenter = GetPageDistancesFromScrollRectCenter();
			int key = pageDistancesFromScrollRectCenter.Last().Key;
			Page pageByNumber = GetPageByNumber(key);
			pageByNumber.transform.SetAsFirstSibling();
			AdjustScrollPositionAfterPageMoved(eDirection.Left);
		}

		private void AdjustScrollPositionAfterPageMoved(eDirection directionMoved)
		{
			int num = ((directionMoved != 0) ? 1 : (-1));
			float pageSize = ScrollRect.GetPageSize();
			Vector2 directionVector = ScrollRect.GetDirectionVector();
			Vector2 vector = directionVector * (pageSize + SpaceBetweenPages) * num;
			ScrollRect.ResetDragOffset = true;
			ScrollRect.content.anchoredPosition += vector;
			UpdatePages();
			if (scrollCoroutine != null)
			{
				scrollRectAnimation_InitialPosition += vector;
				scrollRectAnimation_DesiredPosition += vector;
			}
		}

		private void UpdateSeamlessPagePositions_PagePreviews()
		{
			if (!Application.isPlaying || !LoopSeamlessly)
			{
				return;
			}
			bool flag = false;
			int pagePosition = GetPagePosition(CurrentPage);
			float num = 1f;
			float num2 = ((!ScrollRect.horizontal) ? m_otherPageSize.y : m_otherPageSize.x);
			int num3 = ((NumberOfPages < 5) ? 1 : 2);
			if (pagePosition <= num3)
			{
				Page page = Pages.Last();
				page.transform.SetAsFirstSibling();
				num = -1f;
				flag = true;
			}
			else if (NumberOfPages - pagePosition <= num3)
			{
				Page page2 = Pages.First();
				page2.transform.SetAsLastSibling();
				flag = true;
			}
			if (flag)
			{
				ScrollRect.ResetDragOffset = true;
				Vector2 directionVector = ScrollRect.GetDirectionVector();
				Vector2 vector = directionVector * (num2 + SpaceBetweenPages) * num;
				ScrollRect.content.anchoredPosition += vector;
				UpdatePages();
				if (scrollCoroutine != null)
				{
					scrollRectAnimation_InitialPosition += vector;
					scrollRectAnimation_DesiredPosition += vector;
				}
			}
		}

		private void AutomaticallyMoveToNextPage_Seamless()
		{
			int pagePosition = GetPagePosition(CurrentPage);
			if (pagePosition == NumberOfPages)
			{
				MoveFirstPageToEnd();
			}
			if (CurrentPage >= NumberOfPages)
			{
				if (LoopEndlessly)
				{
					Page newPage = Pages.OrderBy((Page p) => p.PageNumber).FirstOrDefault((Page p) => p.PageEnabled && p.ShowOnPagination);
					SetCurrentPage(newPage);
				}
			}
			else
			{
				Page newPage2 = (from p in Pages
					orderby p.PageNumber
					where p.PageNumber > CurrentPage
					select p).FirstOrDefault((Page p) => p.PageEnabled && p.ShowOnPagination);
				SetCurrentPage(newPage2);
			}
		}

		public void SetShowFirstAndLastButtons(bool show)
		{
			ShowFirstAndLastButtons = show;
			ToggleFirstAndLastButtons(show);
		}

		public void SetShowPreviousAndNextButtons(bool show)
		{
			ShowPreviousAndNextButtons = show;
			TogglePreviousAndNextButtons(show);
		}

		public void SetAnimationSpeed(float animationSpeed)
		{
			AnimationSpeed = animationSpeed;
		}

		public void SetAnimationType(string animationType)
		{
			AnimationType = (eAnimationType)Enum.Parse(typeof(eAnimationType), animationType);
			Pages.ForEach(delegate(Page p)
			{
				p.ResetPositionAndAlpha();
			});
		}

		public void SetDelayBetweenPages(float delay)
		{
			DelayBetweenPages = delay;
		}

		public void SetLoopEndlessly(bool loop)
		{
			LoopEndlessly = loop;
		}

		public void SetAutomaticallyMoveToNextPage(bool move)
		{
			AutomaticallyMoveToNextPage = move;
			_timeSinceLastPage = 0f;
		}

		public void SetShowPageNumbersOnButtons(bool show)
		{
			ShowNumbersOnButtons = show;
			UpdatePagination();
		}

		public void SetShowPageTitlesOnButtons(bool show)
		{
			ShowPageTitlesOnButtons = show;
			UpdatePagination();
		}

		public void SetMaximumNumberOfButtonsToShow(int maxNumber)
		{
			MaximumNumberOfButtonsToShow = maxNumber;
			UpdatePagination();
		}

		public void SetMaximumNumberOfButtonsToShow(float maxNumber)
		{
			SetMaximumNumberOfButtonsToShow((int)maxNumber);
		}

		public void SetUseKeyboardInput(bool useInput)
		{
			UseKeyboardInput = useInput;
		}

		public void SetUseSwipeInput(bool useInput)
		{
			if (UsingScrollRect)
			{
				useInput = false;
			}
			MobileInput.enabled = useInput;
		}

		public void SetUseScrollWheelInput(bool useInput)
		{
			UseScrollWheelInput = useInput;
			ScrollWheelInput.enabled = useInput;
		}

		public void SetOnlyUseScrollWheelInputOnlyWhenMouseIsOver(bool onlyWhenMouseIsOver)
		{
			OnlyUseScrollWheelInputWhenMouseIsOver = onlyWhenMouseIsOver;
		}

		public void SetHighlightWhenMouseIsOver(bool highlight)
		{
			HighlightWhenMouseIsOver = highlight;
			if (!highlight)
			{
				ClearHighlight();
			}
			else if (mouseIsOverPagedRect)
			{
				ShowHighlight();
			}
		}

		public void SetSwipeDeltaThreshold(float threshold)
		{
			SwipeDeltaThreshold = threshold;
		}

		public virtual void SetCurrentPage(Page newPage, bool initial = false)
		{
			int num = Pages.IndexOf(newPage);
			if (num == -1)
			{
				throw new UnityException("PagedRect.SetCurrentPag(Page newPage) :: The value provided for 'newPage' is not in the collection of pages!");
			}
			SetCurrentPage(newPage.PageNumber, initial);
		}

		public virtual void SetCurrentPage(int newPage)
		{
			SetCurrentPage(newPage, false);
		}

		public virtual void SetCurrentPage(int newPage, bool initial)
		{
			if (NumberOfPages == 0)
			{
				return;
			}
			if (newPage > NumberOfPages)
			{
				throw new UnityException("PagedRect.SetCurrentPage(int newPage) :: The value provided for 'newPage' is greater than the number of pages.");
			}
			if (newPage <= 0)
			{
				throw new UnityException("PagedRect.SetCurrentPage(int newPage) :: The value provided for 'newPage' is less than zero.");
			}
			_timeSinceLastPage = 0f;
			UpdatePages(false, false, false);
			int currentPage = CurrentPage;
			_timeSinceLastPage = 0f;
			CurrentPage = newPage;
			int num = GetPagePosition(newPage) - 1;
			if (!UsingScrollRect)
			{
				if (initial)
				{
					Pages.ForEach(delegate(Page p)
					{
						p.LegacyReset();
					});
				}
				eDirection eDirection = ((CurrentPage >= currentPage) ? eDirection.Right : eDirection.Left);
				for (int i = 0; i < NumberOfPages; i++)
				{
					Page page = Pages[i];
					if (i == num)
					{
						PageEnterAnimation(page, eDirection, initial);
						if (Application.isPlaying)
						{
							page.OnShow();
						}
					}
					else if (page.gameObject.activeSelf)
					{
						if (Application.isPlaying)
						{
							page.OnHide();
						}
						PageExitAnimation(page, (eDirection == eDirection.Left) ? eDirection.Right : eDirection.Left);
					}
				}
			}
			else
			{
				if (Application.isPlaying)
				{
					for (int j = 0; j < NumberOfPages; j++)
					{
						Page page2 = Pages[j];
						if (j == num)
						{
							page2.OnShow();
						}
						else if (page2.Visible)
						{
							page2.OnHide();
						}
					}
				}
				CenterScrollRectOnCurrentPage(initial);
			}
			UpdatePagination();
			if (PageChangedEvent != null)
			{
				PageChangedEvent.Invoke(GetPageByNumber(CurrentPage), GetPageByNumber(currentPage));
			}
			if (UsingScrollRect && ShowPagePreviews)
			{
				UpdateSeamlessPagePositions_PagePreviews();
			}
			UpdateScrollBarPosition();
		}

		public virtual void NextPage()
		{
			Page page = (from p in Pages
				orderby p.PageNumber
				where p.PageNumber > CurrentPage
				select p).FirstOrDefault((Page p) => p.PageEnabled && p.ShowOnPagination);
			if (page != null)
			{
				SetCurrentPage(page);
			}
			else
			{
				ShowFirstPage();
			}
		}

		public virtual void PreviousPage()
		{
			Page page = (from p in Pages
				orderby p.PageNumber descending
				where p.PageNumber < CurrentPage
				select p).FirstOrDefault((Page p) => p.PageEnabled && p.ShowOnPagination);
			if (page != null)
			{
				SetCurrentPage(page);
			}
			else
			{
				ShowLastPage();
			}
		}

		public virtual void ShowFirstPage()
		{
			Page page = Pages.OrderBy((Page p) => p.PageNumber).FirstOrDefault((Page p) => p.PageEnabled && p.ShowOnPagination);
			if (page != null)
			{
				SetCurrentPage(page);
			}
		}

		public virtual void ShowLastPage()
		{
			Page page = Pages.OrderByDescending((Page p) => p.PageNumber).FirstOrDefault((Page p) => p.PageEnabled && p.ShowOnPagination);
			if (page != null)
			{
				SetCurrentPage(page);
			}
		}

		public IEnumerator DelayedCall(float delay, Action call)
		{
			if (delay == 0f)
			{
				yield return new WaitForEndOfFrame();
			}
			else
			{
				yield return new WaitForSeconds(delay);
			}
			call();
		}

		private void Awake()
		{
			CurrentPage = DefaultPage;
			if (ScrollRect == null)
			{
				ScrollRect = GetComponent<PagedRect_ScrollRect>();
			}
			if (UsingScrollRect)
			{
				ScrollRect.horizontalNormalizedPosition = 0f;
				ScrollRect.verticalNormalizedPosition = 0f;
				ScrollRect.content.anchoredPosition = Vector2.zero;
			}
			if (Application.isPlaying)
			{
				InvalidateButtonPool();
			}
			OptimizePagination();
		}

		private void LateUpdate()
		{
			if (!firstPageSet)
			{
				SetFirstPage();
			}
		}

		private void SetFirstPage()
		{
			if (firstPageSet)
			{
				return;
			}
			firstPageSet = true;
			if (UsingScrollRect)
			{
				CenterScrollRectOnCurrentPage(true);
				PagedRectTimer.DelayedCall(0.01f, delegate
				{
					CenterScrollRectOnCurrentPage(true);
				}, this);
				PagedRectTimer.DelayedCall(0.05f, delegate
				{
					UpdatePages(true);
					UpdateSeamlessPagePositions();
				}, this);
			}
			UpdatePagination();
		}

		private void EditorUpdate()
		{
			if (!Application.isPlaying)
			{
			}
		}

		private void Start()
		{
			GetComponentInChildren<Viewport>().Initialise(this);
			if (UsingScrollRect)
			{
				ScrollRect.onValueChanged.AddListener(ScrollRectValueChanged);
				UpgradeLayoutGroupIfNecessary();
			}
			UpdateDisplay();
			if (Application.isPlaying)
			{
				if (UseSwipeInput && (!UsingScrollRect || ShowPagePreviews))
				{
					MobileInput.enabled = true;
				}
				if (UseScrollWheelInput)
				{
					ScrollWheelInput.enabled = true;
				}
			}
			if (Application.isPlaying)
			{
				SetupMouseEvents();
			}
			if (NumberOfPages != 0 && Application.isPlaying && DefaultPage <= NumberOfPages)
			{
				SetCurrentPage(DefaultPage, true);
			}
		}

		private void OnEnable()
		{
			PagedRectTimer.DelayedCall(0f, ViewportDimensionsChanged, this);
		}

		private void Update()
		{
			MonitorPageCollection();
			if (!Application.isPlaying)
			{
				return;
			}
			if (previousAnimationTypeValue != AnimationType)
			{
				Pages.ForEach(delegate(Page p)
				{
					p.ResetPositionAndAlpha();
				});
			}
			previousAnimationTypeValue = AnimationType;
			if (UseKeyboardInput)
			{
				HandleKeyboardInput();
			}
			_timeSinceLastPage += Time.deltaTime;
			if (AutomaticallyMoveToNextPage && _timeSinceLastPage >= DelayBetweenPages)
			{
				if (UsingScrollRect && LoopSeamlessly)
				{
					AutomaticallyMoveToNextPage_Seamless();
				}
				else
				{
					NextPage();
				}
			}
			if (!mouseIsOverPagedRect && OnlyUseScrollWheelInputWhenMouseIsOver)
			{
				ScrollWheelInput.enabled = false;
			}
			else
			{
				ScrollWheelInput.enabled = UseScrollWheelInput;
			}
			CheckForDeletedPages();
			if (lastEndDragData != null)
			{
				lastEndDragData = null;
			}
		}

		private void OnValidate()
		{
			isDirty = true;
			if (base.gameObject.activeInHierarchy && Application.isPlaying)
			{
				UpdateDisplay();
			}
		}

		public void UpdateDisplay()
		{
			if (!ShowPagination)
			{
				ToggleTemplateButtons(false);
				ToggleFirstAndLastButtons(false);
				TogglePreviousAndNextButtons(false);
			}
			else
			{
				if (Application.isPlaying || !ShowButtonTemplatesInEditor)
				{
					ToggleTemplateButtons(false);
				}
				else if (!Application.isPlaying && ShowButtonTemplatesInEditor)
				{
					ToggleTemplateButtons(true);
				}
				ToggleFirstAndLastButtons(ShowFirstAndLastButtons);
				TogglePreviousAndNextButtons(ShowPreviousAndNextButtons);
			}
			UpdatePages();
			if (UsingScrollRect)
			{
				if (layoutGroup != null)
				{
					layoutGroup.spacing = SpaceBetweenPages;
				}
				UpdateScrollBar();
			}
			ViewportDimensionsChanged();
		}

		public void UpdatePages(bool force = false, bool forceRenewPageNumbers = false, bool updatePagination = true)
		{
			if (this == null)
			{
				return;
			}
			if (force)
			{
				isDirty = true;
			}
			List<Page> list = (from p in Viewport.GetComponentsInChildren<Page>(!UsingScrollRect)
				where p != NewPageTemplate && p.transform.parent == Viewport.transform
				select p).ToList();
			if (!_pages.Any())
			{
				_pages = list;
			}
			else
			{
				isDirty = isDirty || !list.SequenceEqual(_pages);
			}
			Pages = (_pages = list);
			int pageNumber = 1;
			Pages.ForEach(delegate(Page p)
			{
				if (!p.Initialised)
				{
					p.Initialise(this);
				}
				if (p.PageNumber == 0 || forceRenewPageNumbers)
				{
					p.PageNumber = pageNumber;
				}
				if (!ShowPagePreviews)
				{
					bool flag = p.DesiredScale != Vector3.one || p.rectTransform.localScale != Vector3.one;
					p.DesiredScale = Vector3.one;
					p.rectTransform.localScale = Vector3.one;
					if (flag)
					{
						LayoutRebuilder.MarkLayoutForRebuild(p.rectTransform);
					}
				}
				pageNumber++;
			});
			if (isDirty)
			{
				if (CurrentPage > NumberOfPages && NumberOfPages > 0)
				{
					ShowLastPage();
				}
				if (updatePagination)
				{
					UpdatePagination();
				}
			}
		}

		private void PageWasDeleted(int deletedPageNumber)
		{
			Pages.Where((Page p) => p.PageNumber >= deletedPageNumber).ToList().ForEach(delegate(Page p)
			{
				p.PageNumber--;
			});
			UpdatePages();
			if (UsingScrollRect)
			{
				CenterScrollRectOnCurrentPage(true);
			}
		}

		private void CheckForDeletedPages()
		{
			if (_numberOfPages == NumberOfPages)
			{
				return;
			}
			for (int i = 1; i <= _numberOfPages; i++)
			{
				Page pageByNumber = GetPageByNumber(i, false, true);
				if (pageByNumber == null)
				{
					PageWasDeleted(i);
					break;
				}
			}
			_numberOfPages = NumberOfPages;
		}

		public void ViewportDimensionsChanged()
		{
			if (this == null || base.gameObject == null || !base.gameObject.activeInHierarchy)
			{
				return;
			}
			Pages.ForEach(delegate(Page p)
			{
				p.UpdateDimensions();
			});
			if (!UsingScrollRect)
			{
				return;
			}
			if (Application.isPlaying)
			{
				StartCoroutine(DelayedCall(0.05f, delegate
				{
					CenterScrollRectOnCurrentPage(true);
				}));
			}
			else
			{
				SetScrollRectPosition();
			}
		}
	}
}
