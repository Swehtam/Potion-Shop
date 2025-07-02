using System.Collections.Generic;
using UnityEngine;
using static IngredientsManager;

public class CauldronManager : MonoBehaviour
{
	public static CauldronManager Instance;

    [SerializeField]
    private List<RecipesData> _recipes = new();
	public List<RecipesData> Recipes => _recipes;

	[SerializeField]
	private CauldronCanvas _cauldronCanvas;

	private Dictionary<IngredientsData, int> _selectedIngredients = new();

	private Bottle _selectedBottle;
	public Bottle Bottle => _selectedBottle;
	private int _goldValue = 0;
	private IngredientsStatus _status = new();

	private float _timeToMix = 0f;
	public float TimeToMix => _timeToMix;

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

	public void ReceiveIngredientsData(Dictionary<IngredientsData, int> ingredients, Bottle bottle, int goldSpent, IngredientsStatus status)
	{
		foreach (KeyValuePair<IngredientsData, int> ing in ingredients)
		{
			_selectedIngredients.Add(ing.Key, ing.Value);
			_timeToMix += ing.Value;
		}

		_selectedBottle = bottle;
		_goldValue = goldSpent;

		_status.strValue = status.strValue;
		_status.intValue = status.intValue;
		_status.agiValue = status.agiValue;

		_cauldronCanvas.AddPotionComponents(_selectedIngredients, _selectedBottle);
	}

	public bool DeliverPotion(Potion potion)
	{
		if (ShopManager.Instance.ReceivePotion(potion))
			return true;

		return false;
	}

	public RecipesData CraftPotion()
	{
		ShopManager.Instance.CraftPotion(_goldValue);

		foreach (RecipesData recipe in _recipes)
		{
			if (recipe.CheckRequirements(_selectedIngredients, _status))
				return recipe;
		}

		return null;
	}

	public void ClearData()
	{
		_selectedIngredients.Clear();
		_selectedBottle = null;
		_goldValue = 0;
		_status.strValue = 0;
		_status.intValue = 0;
		_status.agiValue = 0;
		_timeToMix = 0f;
	}
}
