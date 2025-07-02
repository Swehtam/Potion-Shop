using UnityEngine;

public class IngredientsCanvasEvents : MonoBehaviour
{
	public delegate void IngredientAddedHandler(IngredientsData ingredient);
	public static event IngredientAddedHandler OnIngredientAdded;

	public delegate void IngredientRemovedHandler(IngredientsData ingredient);
	public static event IngredientRemovedHandler OnIngredientRemoved;

	public delegate void IngredientsEmptyHandler();
	public static event IngredientsEmptyHandler OnIngredientsEmpty;

	public delegate void BottleSelectedHandler(Bottle bottle);
	public static event BottleSelectedHandler OnBottleSelected;

	public static void IngredientAdded(IngredientsData ingredient)
	{
		OnIngredientAdded?.Invoke(ingredient);
	}

	public static void IngredientRemoved(IngredientsData ingredient)
	{
		OnIngredientRemoved?.Invoke(ingredient);
	}

	public static void IngredientsEmpty()
	{
		OnIngredientsEmpty?.Invoke();
	}

	public static void BottleSelected(Bottle bottle)
	{
		OnBottleSelected?.Invoke(bottle);
	}
}
