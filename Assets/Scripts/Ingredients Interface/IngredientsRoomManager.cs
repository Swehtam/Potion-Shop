using System;
using UnityEngine;
using UnityEngine.UI;

public class IngredientsRoomManager : MonoBehaviour
{
	[SerializeField]
	private Image _bottleImage;

	[SerializeField]
	private GameObject _mixButton;

	[SerializeField]
	private GameObject _seletBottleWarning;

	[SerializeField]
	private SelectedIngredientsCanvas _selectedIngredientsCanvas;

	[SerializeField]
	private CanvasGroup _recipesBook;

	private void Start()
	{
		_bottleImage.sprite = Utils.Instance.EmptyBottle;
		_mixButton.SetActive(false);
		_seletBottleWarning.SetActive(false);
		
		_recipesBook.alpha = 0;
		_recipesBook.interactable = false;
		_recipesBook.blocksRaycasts = false;
	}

	private void OnEnable()
	{
		IngredientsCanvasEvents.OnIngredientAdded += ShowMixButton;
		IngredientsCanvasEvents.OnBottleSelected += BottleSelected;
		IngredientsCanvasEvents.OnIngredientsEmpty += HideMixButton;
	}

	private void OnDisable()
	{
		IngredientsCanvasEvents.OnIngredientAdded -= ShowMixButton;
		IngredientsCanvasEvents.OnBottleSelected -= BottleSelected;
		IngredientsCanvasEvents.OnIngredientsEmpty -= HideMixButton;
	}
	
	public void MixButtonSelected()
	{
		if (!IngredientsManager.Instance.CanGoToCauldron())
		{
			_seletBottleWarning.SetActive(true);
			return;
		}

		OverlayCanvas.Instance.CauldronSelected();
		ClearCanvas();
		IngredientsManager.Instance.ClearData();
	}

	public void ShowBookRecipe()
	{
		_recipesBook.alpha = 1;
		_recipesBook.interactable = true;
		_recipesBook.blocksRaycasts = true;

		OverlayCanvas.Instance.RecipeBookOpened();
	}

	public void HideBookRecipe()
	{
		_recipesBook.alpha = 0;
		_recipesBook.interactable = false;
		_recipesBook.blocksRaycasts = false;

		OverlayCanvas.Instance.RecipeBookClosed();
	}

	private void BottleSelected(Bottle bottle)
	{
		if (bottle.Type == BottleType.None)
			throw new ArgumentException("Bottle Type cannot be None.");

		_bottleImage.sprite = bottle.Sprite;
	}

	private void ShowMixButton(IngredientsData ingredient)
	{
		if (_mixButton.activeSelf)
			return; 

		_mixButton.SetActive(true);
	}

	private void HideMixButton()
	{
		_mixButton.SetActive(false);
	}

	private void ClearCanvas()
	{
		_mixButton.SetActive(false);
		_bottleImage.sprite = Utils.Instance.EmptyBottle;
		_selectedIngredientsCanvas.ClearCanvas();
		_seletBottleWarning.SetActive(false);
	}
}
