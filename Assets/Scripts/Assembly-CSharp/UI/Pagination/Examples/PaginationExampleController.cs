using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Pagination.Examples
{
	internal class PaginationExampleController : MonoBehaviour
	{
		[Header("Horizontal Example")]
		public PagedRect HorizontalPaginationExample;

		public InputField HorizontalAnimationSpeedTextField;

		public InputField HorizontalDelayTextField;

		public List<Button> HorizontalAnimationTypeButtons;

		public InputField HorizontalMaximumNumberOfButtonsToShowField;

		[Header("Vertical Example")]
		public PagedRect VerticalPaginationExample;

		public InputField VerticalAnimationSpeedTextField;

		public InputField VerticalDelayTextField;

		public List<Button> VerticalAnimationTypeButtons;

		public InputField VerticalMaximumNumberOfButtonsToShowField;

		[Header("Dynamic Pages Example")]
		public PagedRect DynamicPagesExample;

		public Button ToggleLastPageButton;

		[Header("Slider Example")]
		public PagedRect SliderExample;

		[Header("Character Creation Example")]
		public PagedRect CharacterCreationExample;

		public InputField CharacterCreationNameField;

		public List<Button> CharacterCreationClassButtons;

		public Color CharacterCreationButtonNormalColor;

		public Color CharacterCreationButtonHighlightedColor;

		public List<PagedRect> CharacterCreation_StatInputList;

		public InputField CharacterCreation_UnallocatedStatPointsInput;

		public Page CharacterCreation_StatsPage;

		public Page CharacterCreation_CreatePage;

		protected string characterCreation_SelectedClass;

		[Header("Slider ScrollRect Example")]
		public PagedRect SliderScrollRectExample;

		[Header("Page Previews Example")]
		public PagedRect PagePreviewsHorizontalExample;

		public PagedRect PagePreviewsVerticalExample;

		[Header("Nested ScrollRect Example")]
		public PagedRect NestedScrollRectExample;

		[Header("Tabs Example")]
		public PagedRect TabsHorizontalScrollRectExample;

		[Header("Theme")]
		public Color ExampleButtonNormalColor;

		public Color ExampleButtonHighlightedColor;

		[Header("Controls")]
		public List<Button> ControlButtons;

		private List<PagedRect> examples = new List<PagedRect>();

		private void Start()
		{
			examples = new PagedRect[10] { HorizontalPaginationExample, VerticalPaginationExample, DynamicPagesExample, SliderExample, CharacterCreationExample, SliderScrollRectExample, PagePreviewsHorizontalExample, PagePreviewsVerticalExample, NestedScrollRectExample, TabsHorizontalScrollRectExample }.Where((PagedRect e) => e != null).ToList();
		}

		public void ResetExample()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		public void SwitchExample(string newExample)
		{
			examples.ForEach(delegate(PagedRect e)
			{
				e.gameObject.SetActive(false);
			});
			PagedRect pagedRect = null;
			switch (newExample)
			{
			case "HorizontalPaginationExample":
				pagedRect = HorizontalPaginationExample;
				break;
			case "VerticalPaginationExample":
				pagedRect = VerticalPaginationExample;
				break;
			case "DynamicPagesExample":
				pagedRect = DynamicPagesExample;
				break;
			case "SliderExample":
				pagedRect = SliderExample;
				break;
			case "CharacterCreationExample":
				pagedRect = CharacterCreationExample;
				break;
			case "SliderScrollRectExample":
				pagedRect = SliderScrollRectExample;
				break;
			case "PagePreviewsHorizontalExample":
				pagedRect = PagePreviewsHorizontalExample;
				break;
			case "PagePreviewsVerticalExample":
				pagedRect = PagePreviewsVerticalExample;
				break;
			case "NestedScrollRectExample":
				pagedRect = NestedScrollRectExample;
				break;
			case "TabsHorizontalScrollRectExample":
				pagedRect = TabsHorizontalScrollRectExample;
				break;
			}
			pagedRect.gameObject.SetActive(true);
			pagedRect.SetCurrentPage(1, true);
		}

		public void HighlightExampleButton(Button selectedButton)
		{
			ControlButtons.ForEach(delegate(Button b)
			{
				ColorBlock colors2 = b.colors;
				colors2.normalColor = ExampleButtonNormalColor;
				b.colors = colors2;
				b.image.color = ExampleButtonNormalColor;
			});
			ColorBlock colors = selectedButton.colors;
			colors.normalColor = ExampleButtonHighlightedColor;
			selectedButton.colors = colors;
			selectedButton.image.color = ExampleButtonHighlightedColor;
		}

		public void SetHorizontalAnimationSpeedText(float animationSpeed)
		{
			HorizontalAnimationSpeedTextField.text = animationSpeed.ToString();
		}

		public void HighlightHorizontalAnimationTypeButton(Button selectedButton)
		{
			HorizontalAnimationTypeButtons.ForEach(delegate(Button b)
			{
				ColorBlock colors2 = b.colors;
				colors2.normalColor = ExampleButtonNormalColor;
				b.colors = colors2;
				b.image.color = ExampleButtonNormalColor;
			});
			ColorBlock colors = selectedButton.colors;
			colors.normalColor = ExampleButtonHighlightedColor;
			selectedButton.colors = colors;
			selectedButton.image.color = ExampleButtonHighlightedColor;
		}

		public void SetHorizontalDelayText(float delay)
		{
			HorizontalDelayTextField.text = delay + "s";
		}

		public void SetHorizontalMaximumNumberOfButtonsToShowText(float maxNumber)
		{
			HorizontalMaximumNumberOfButtonsToShowField.text = maxNumber.ToString();
		}

		public void SetVerticalAnimationSpeedText(float animationSpeed)
		{
			VerticalAnimationSpeedTextField.text = animationSpeed.ToString();
		}

		public void HighlightVerticalAnimationTypeButton(Button selectedButton)
		{
			VerticalAnimationTypeButtons.ForEach(delegate(Button b)
			{
				ColorBlock colors2 = b.colors;
				colors2.normalColor = ExampleButtonNormalColor;
				b.colors = colors2;
				b.image.color = ExampleButtonNormalColor;
			});
			ColorBlock colors = selectedButton.colors;
			colors.normalColor = ExampleButtonHighlightedColor;
			selectedButton.colors = colors;
			selectedButton.image.color = ExampleButtonHighlightedColor;
		}

		public void SetVerticalDelayText(float delay)
		{
			VerticalDelayTextField.text = delay + "s";
		}

		public void SetVerticalMaximumNumberOfButtonsToShowText(float maxNumber)
		{
			VerticalMaximumNumberOfButtonsToShowField.text = maxNumber.ToString();
		}

		public void DynamicPageExample_AddPage()
		{
			Page page = DynamicPagesExample.AddPageUsingTemplate();
			page.PageTitle = "Page " + DynamicPagesExample.NumberOfPages;
			DynamicPagesExample.UpdatePagination();
			Text text = page.GetComponentsInChildren<Text>().FirstOrDefault((Text t) => t.name == "Text");
			if (text != null)
			{
				text.text = "This is a dynamically added page.\r\nIts page number is " + DynamicPagesExample.NumberOfPages + ".";
			}
			ToggleLastPageButton.interactable = true;
		}

		public void DynamicPageExample_RemoveLastPage()
		{
			Page page = DynamicPagesExample.Pages.LastOrDefault((Page l) => l.PageTitle != "Main Page");
			if (page != null)
			{
				DynamicPagesExample.RemovePage(page, true);
				if (DynamicPagesExample.NumberOfPages <= 1)
				{
					ToggleLastPageButton.interactable = false;
				}
			}
		}

		public void DynamicPageExample_ToggleLastPage()
		{
			Page page = DynamicPagesExample.Pages.LastOrDefault((Page l) => l.PageTitle != "Main Page");
			if (page != null)
			{
				if (page.PageEnabled)
				{
					page.DisablePage();
				}
				else
				{
					page.EnablePage();
				}
			}
		}

		public void CharacterCreation_CheckIfStatsPageShouldBeEnabled()
		{
			bool flag = !string.IsNullOrEmpty(CharacterCreationNameField.text);
			bool flag2 = !string.IsNullOrEmpty(characterCreation_SelectedClass);
			bool pageEnabled = CharacterCreation_StatsPage.PageEnabled;
			if (flag && flag2)
			{
				CharacterCreation_StatsPage.PageEnabled = true;
			}
			else
			{
				CharacterCreation_StatsPage.PageEnabled = false;
			}
			if (pageEnabled != CharacterCreation_StatsPage.PageEnabled)
			{
				CharacterCreationExample.UpdatePagination();
			}
		}

		public void CharacterCreation_HighlightClassButton(Button selectedButton)
		{
			CharacterCreationClassButtons.ForEach(delegate(Button b)
			{
				ColorBlock colors2 = b.colors;
				colors2.normalColor = CharacterCreationButtonNormalColor;
				b.colors = colors2;
				b.image.color = CharacterCreationButtonNormalColor;
			});
			ColorBlock colors = selectedButton.colors;
			colors.normalColor = CharacterCreationButtonHighlightedColor;
			selectedButton.colors = colors;
			selectedButton.image.color = CharacterCreationButtonHighlightedColor;
		}

		public void CharacterCreation_SetClass(string _class)
		{
			characterCreation_SelectedClass = _class;
			CharacterCreation_CheckIfStatsPageShouldBeEnabled();
		}

		public void CharacterCreation_StatsUpdate(Page NewPage, Page OldPage)
		{
			int num = 35;
			foreach (PagedRect characterCreation_StatInput in CharacterCreation_StatInputList)
			{
				int currentPage = characterCreation_StatInput.CurrentPage;
				num -= currentPage;
			}
			bool disableNextButtons = num > 0;
			CharacterCreation_StatInputList.ForEach(delegate(PagedRect i)
			{
				i.Button_NextPage.Button.interactable = disableNextButtons;
			});
			CharacterCreation_UnallocatedStatPointsInput.text = num.ToString();
			bool pageEnabled = CharacterCreation_CreatePage.PageEnabled;
			CharacterCreation_CreatePage.PageEnabled = num <= 0;
			if (pageEnabled != CharacterCreation_CreatePage.PageEnabled)
			{
				CharacterCreationExample.UpdatePagination();
			}
		}

		public IEnumerator DelayedCall(float delay, Action call)
		{
			yield return new WaitForSeconds(delay);
			call();
		}
	}
}
