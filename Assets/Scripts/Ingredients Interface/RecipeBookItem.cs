using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class RecipeBookItem : MonoBehaviour
{
    [SerializeField]
    private Image _potionImage;
    [SerializeField]
    private TMP_Text _potionName;

    [SerializeField]
    private Image _firstIngredientImage;
	[SerializeField]
	private TMP_Text _firstIngredientCount;
	[SerializeField]
	private Image _secondIngredientImage;
	[SerializeField]
	private TMP_Text _secondIngredientCount;
	[SerializeField]
	private Image _thirdIngredientImage;
	[SerializeField]
	private TMP_Text _thirdIngredientCount;

	[SerializeField]
	private TMP_Text _strValueText;
	[SerializeField]
	private TMP_Text _intValueText;
	[SerializeField]
	private TMP_Text _agiValueText;

	[SerializeField]
	private TMP_Text _smallSellText;
	[SerializeField]
	private TMP_Text _mediumSellText;
	[SerializeField]
	private TMP_Text _largeSellText;


	public void AddRecipe(RecipesData recipe)
	{
		_potionImage.sprite = recipe.largePotion;
		_potionName.text = recipe.name;

		if (recipe.Ingredients.Count > 3 || recipe.Ingredients.Count == 0)
			throw new System.Exception($"Recipe's '{recipe.name}' ingredients show have from 1 to 3 ingredients.");

		for(int i = 0; i < recipe.Ingredients.Count; i++)
		{
			string quantity = recipe.Ingredients[i].Quantity.ToString();
			Sprite sprite = recipe.Ingredients[i].Ingredient.Image;
			if (i == 0)
			{
				_firstIngredientCount.text = quantity;
				_firstIngredientImage.sprite = sprite;
			}
			else if(i == 1)
			{
				_secondIngredientCount.text = quantity;
				_secondIngredientImage.sprite = sprite;
			}
			else if(i == 2)
			{
				_thirdIngredientCount.text = quantity;
				_thirdIngredientImage.sprite = sprite;
			}
		}

		_strValueText.text = recipe.STRGoal.ToString();
		_intValueText.text = recipe.INTGoal.ToString();
		_agiValueText.text = recipe.AGIGoal.ToString();

		_smallSellText.text = (Mathf.CeilToInt(recipe.GoldValue * Utils.Instance.SmallPotionMultiplier)).ToString();
		_mediumSellText.text = (Mathf.CeilToInt(recipe.GoldValue * Utils.Instance.MediumPotionMultiplier)).ToString();
		_largeSellText.text = (Mathf.CeilToInt(recipe.GoldValue * Utils.Instance.LargePotionMultiplier)).ToString();
	}
}