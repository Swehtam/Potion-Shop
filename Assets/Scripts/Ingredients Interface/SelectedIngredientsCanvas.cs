using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class SelectedIngredientsCanvas : MonoBehaviour
{
	[SerializeField]
	private GameObject _ingredientPrefab;

	[SerializeField]
	private Transform _ingredientsGrid;

	[SerializeField]
	private TMP_Text _goldText;
	[SerializeField]
	private TMP_Text _strText;
	[SerializeField]
	private TMP_Text _intText;
	[SerializeField]
	private TMP_Text _agiText;

	private Dictionary<IngredientsData, int> _selectedIngredients = new();
	private Dictionary<IngredientsData, SelectedIngredientElement> _gridElements = new();

	private int _goldValue = 0;
	private int _strValue = 0;
	private int _intValue = 0;
	private int _agiValue = 0;

	protected virtual void OnEnable()
	{
		IngredientsCanvasEvents.OnIngredientAdded += AddIngredient;
		IngredientsCanvasEvents.OnIngredientRemoved += RemoveIngredient;
	}

	protected virtual void OnDisable()
	{
		IngredientsCanvasEvents.OnIngredientAdded -= AddIngredient;
		IngredientsCanvasEvents.OnIngredientRemoved -= RemoveIngredient;
	}

	private void Awake()
	{
		_goldText.text = "0";
		_strText.text = "0";
		_intText.text = "0";
		_agiText.text = "0";
	}

	public void AddIngredient(IngredientsData data)
	{
		if (_selectedIngredients.ContainsKey(data))
		{
			_selectedIngredients[data] += 1;
			_gridElements[data].UpdateElement(_selectedIngredients[data]);
		}
		else
		{
			_selectedIngredients.Add(data, 1);
			
			SelectedIngredientElement element = Instantiate(_ingredientPrefab, _ingredientsGrid).GetComponent<SelectedIngredientElement>();
			element.InitElement(data);
			_gridElements.Add(data, element);
		}

		_goldValue += data.GoldValue;
		_strValue += data.STRValue;
		_intValue += data.INTValue;
		_agiValue += data.AGIValue;

		UpdateCanvas();
	}

	private void RemoveIngredient(IngredientsData data)
	{
		if (!_selectedIngredients.ContainsKey(data))
			throw new NullReferenceException($"{data} does not exist in IngredientManager selected ingredient.");

		int quantity = _selectedIngredients[data];
		if (quantity == 1)
		{
			_selectedIngredients.Remove(data);
			
			GameObject element = _gridElements[data].gameObject;
			_gridElements.Remove(data);
			Destroy(element);
		}
		else
		{
			_selectedIngredients[data] -= 1;
			_gridElements[data].UpdateElement(_selectedIngredients[data]);
		}

		if(_selectedIngredients.Count == 0)
			IngredientsCanvasEvents.IngredientsEmpty();

		_goldValue -= data.GoldValue;
		_strValue -= data.STRValue;
		_intValue -= data.INTValue;
		_agiValue -= data.AGIValue;

		UpdateCanvas();
	}

	private void UpdateCanvas()
	{
		_goldText.text = $"{_goldValue}";
		_strText.text = $"{_strValue}";
		_intText.text = $"{_intValue}";
		_agiText.text = $"{_agiValue}";
	}

	public virtual void ClearCanvas()
	{
		_selectedIngredients.Clear();

		foreach (SelectedIngredientElement element in _gridElements.Values)
		{
			Destroy(element.gameObject);
		}

		_gridElements.Clear();

		_goldText.text = "0";
		_strText.text = "0";
		_intText.text = "0";
		_agiText.text = "0";
		
		_goldValue = 0;
		_strValue = 0;
		_intValue = 0;
		_agiValue = 0;
	}
}
