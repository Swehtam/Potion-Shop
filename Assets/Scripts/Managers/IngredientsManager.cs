using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngredientsManager : MonoBehaviour
{
	public static IngredientsManager Instance;

	private Bottle _selectedBottle;

	private Dictionary<IngredientsData, int> _selectedIngredients = new();
	public Dictionary<IngredientsData, int> SelectedIngredients => _selectedIngredients;

	private int _goldValue = 0;
	[Serializable]
	public class IngredientsStatus
	{
		public int strValue = 0;
		public int intValue = 0;
		public int agiValue = 0;
	}

	private readonly IngredientsStatus _status = new();
	public IngredientsStatus Status => _status;

	private void OnEnable()
	{
		IngredientsCanvasEvents.OnIngredientAdded += AddIngredient;
		IngredientsCanvasEvents.OnIngredientRemoved += RemoveIngredient;
		IngredientsCanvasEvents.OnBottleSelected += BottleSelected;
	}

	private void OnDisable()
	{
		IngredientsCanvasEvents.OnIngredientAdded -= AddIngredient;
		IngredientsCanvasEvents.OnIngredientRemoved -= RemoveIngredient;
		IngredientsCanvasEvents.OnBottleSelected -= BottleSelected;
	}

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
	}

	public bool CanGoToCauldron()
	{
		if(_selectedBottle.IsUnityNull()) return false;

		if(_selectedIngredients.Count == 0) return false;

		TransferDataToCauldron();

		return true;
	}

	private void AddIngredient(IngredientsData data)
	{
		if (_selectedIngredients.ContainsKey(data))
			_selectedIngredients[data] += 1;
		else
			_selectedIngredients.Add(data, 1);

		_goldValue += data.GoldValue;
		_status.strValue += data.STRValue;
		_status.intValue += data.INTValue;
		_status.agiValue += data.AGIValue;
	}

	private void RemoveIngredient(IngredientsData data)
	{
		if (!_selectedIngredients.ContainsKey(data))
			throw new NullReferenceException($"{data} does not exist in IngredientManager selected ingredient.");

		int quantity = _selectedIngredients[data];
		if(quantity == 1)
			_selectedIngredients.Remove(data);
		else
			_selectedIngredients[data] -= 1;

		_goldValue -= data.GoldValue;
		_status.strValue -= data.STRValue;
		_status.intValue -= data.INTValue;
		_status.agiValue -= data.AGIValue;
	}

	private void BottleSelected(Bottle bottle)
	{
		if (bottle.Type == BottleType.None)
			throw new ArgumentException("Bottle Type cannot be None");

		_selectedBottle = bottle;
	}

	private void TransferDataToCauldron()
	{
		CauldronManager.Instance.ReceiveIngredientsData(_selectedIngredients, _selectedBottle, _goldValue, _status);
	}

	public void ClearData()
	{
		_selectedIngredients.Clear();
		_selectedBottle = null;
		_goldValue = 0;
		_status.strValue = 0;
		_status.intValue = 0;
		_status.agiValue = 0;
	}
}
