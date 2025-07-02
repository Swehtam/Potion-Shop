using System;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
	public static ShopManager Instance;

	[SerializeField]
	private BalconyManager _balconyManager;

	private int _playerGold = 1000;

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

	private void Start()
	{
		UpdateCanvas();
	}

	public bool ReceivePotion(Potion potion)
	{
		if (_balconyManager.IsBalconyFull())
			return false;

		_balconyManager.AddPotion(potion);

		return true;
	}

	public void SellPotion(int goldValue)
	{
		_playerGold += goldValue;
		UpdateCanvas();
	}

	public void CraftPotion(int goldCost)
	{
		_playerGold -= goldCost;

		UpdateCanvas();
		if (_playerGold < 0)
			OverlayCanvas.Instance.GameLost();
	}

	public void UpdateCanvas()
	{
		OverlayCanvas.Instance.UpdateGold(_playerGold.ToString());
	}

	public RecipesData GetRandomRecipe()
	{
		int recipesTotal = CauldronManager.Instance.Recipes.Count;
		int randomRecipe = UnityEngine.Random.Range(0, recipesTotal);

		return CauldronManager.Instance.Recipes[randomRecipe];
	}

	public BottleType GetRandomBottleType()
	{
		int randomBottle = UnityEngine.Random.Range(1, 4);

		return randomBottle switch
		{
			1 => BottleType.Small,
			2 => BottleType.Medium,
			3 => BottleType.Large,
			_ => throw new ArgumentException("Random Bottle Type number out of scope."),
		};
	}
}
