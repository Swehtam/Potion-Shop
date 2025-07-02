using System;
using System.Collections.Generic;
using System.Net.Mail;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CauldronCanvas : MonoBehaviour
{
	[SerializeField]
	private SelectedIngredientsCanvas _selectedIngredientsCanvas;

	[SerializeField]
	private Image _bottleImage;

	[SerializeField]
	private GameObject _mixButton;

	[SerializeField]
	private Slider _timeSlider;

	[SerializeField]
	private GameObject _finishMixButton;

	[SerializeField]
	private CauldronPotion _potion;

	[SerializeField]
	private GameObject _deliverButton;

	[SerializeField]
	private GameObject _trashButton;

	[SerializeField]
	private GameObject _shopFullWarning;

	private bool _isMixing = false;
	private float _mixingTime = Mathf.Infinity;
	private float _currentTime = 0f;

	private void Awake()
	{
		_timeSlider.gameObject.SetActive(false);
		_mixButton.SetActive(false);
		_finishMixButton.SetActive(false);
		_deliverButton.SetActive(false);
		_trashButton.SetActive(false);
		_shopFullWarning.SetActive(false);
	}

	private void Start()
	{
		_bottleImage.sprite = Utils.Instance.EmptyBottle;
		_potion.ClearPotion();
	}

	private void Update()
	{
		if (!_isMixing)
			return;

		_currentTime += Time.deltaTime;

		if (_currentTime < _mixingTime)
			_timeSlider.value = _currentTime;
		else
		{
			_isMixing = false;
			_timeSlider.value = _mixingTime;
			CauldronCanvasEvents.StopMixing();
			_finishMixButton.SetActive(true);
		}
	}

	public void AddPotionComponents(Dictionary<IngredientsData, int> ingredients, Bottle bottle)
	{
		foreach (KeyValuePair<IngredientsData, int> ing in ingredients)
		{
			for (int i = 0; i < ing.Value; i++)
			{
				_selectedIngredientsCanvas.AddIngredient(ing.Key);
			}
		}

		_bottleImage.sprite = bottle.Sprite;

		_mixButton.SetActive(true);
	}

	public void StartMixing()
	{
		_isMixing = true;
		_mixButton.SetActive(false);

		_mixingTime = CauldronManager.Instance.TimeToMix;
		_currentTime = 0f;

		_timeSlider.gameObject.SetActive(true);
		_timeSlider.value = 0f;
		_timeSlider.maxValue = _mixingTime;

		CauldronCanvasEvents.StartMixing();
	}

	public void FinishMixing()
	{
		//REDUZIR O GOLD

		_finishMixButton.SetActive(false);
		_timeSlider.gameObject.SetActive(false);
		_selectedIngredientsCanvas.ClearCanvas();
		_bottleImage.sprite = Utils.Instance.EmptyBottle;

		RecipesData recipe = CauldronManager.Instance.CraftPotion();
		if (recipe.IsUnityNull())
		{
			_potion.FailedMixing(Utils.Instance.GetFailledBottleSize(CauldronManager.Instance.Bottle.Type));
			_trashButton.SetActive(true);
		}
		else
		{
			_potion.SetRecipe(recipe, CauldronManager.Instance.Bottle.Type);
			_deliverButton.SetActive(true);
		}
	}

	public void DeliverPotion()
	{
		if (CauldronManager.Instance.DeliverPotion(_potion.Potion))
		{
			OverlayCanvas.Instance.ShopSelected();
			ClearCanvas();
			CauldronManager.Instance.ClearData();
		}
		else
		{
			_shopFullWarning.SetActive(true);
		}
	}

	public void TrashButton()
	{
		ClearCanvas();
		CauldronManager.Instance.ClearData();
	}

	public void ClearCanvas()
	{
		_selectedIngredientsCanvas.ClearCanvas();
		_bottleImage.sprite = Utils.Instance.EmptyBottle;
		_timeSlider.gameObject.SetActive(false);
		_mixButton.SetActive(false);
		_finishMixButton.SetActive(false);
		_potion.ClearPotion();
		_deliverButton.SetActive(false);
		_trashButton.SetActive(false);
		_shopFullWarning.SetActive(false);
	}
}