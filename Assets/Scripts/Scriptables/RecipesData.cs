using System;
using System.Collections.Generic;
using UnityEngine;
using static IngredientsManager;

[CreateAssetMenu(fileName = "RecipesData", menuName = "Scriptable Objects/RecipesData")]
public class RecipesData : ScriptableObject
{
    public string RecipeName;

    public int GoldValue;

    public Sprite smallPotion;
	public Sprite mediumPotion;
	public Sprite largePotion;

	[Serializable]
    public class NecessaryIngredients
    {
        public IngredientsData Ingredient;

        public int Quantity;
    }

    public List<NecessaryIngredients> Ingredients = new();

    [Range(-5, 5)]
    public int STRGoal = 0;

	[Range(-5, 5)]
	public int INTGoal = 0;
	
    [Range(-5, 5)]
	public int AGIGoal = 0;

    public bool CheckRequirements(Dictionary<IngredientsData, int> selectedIngredients, IngredientsStatus selectedStatus)
    {
        foreach(NecessaryIngredients ing in Ingredients)
        {
            if (!selectedIngredients.ContainsKey(ing.Ingredient))
                return false;

            if (selectedIngredients[ing.Ingredient] < ing.Quantity)
                return false;
        }

        if(selectedStatus.strValue != STRGoal) return false;
        if(selectedStatus.intValue != INTGoal) return false;
        if(selectedStatus.agiValue != AGIGoal) return false;

        return true;
    }

    public Sprite GetBottleSize(BottleType bottle)
    {
        switch (bottle)
        {
            case BottleType.Small:
                return smallPotion;
            case BottleType.Medium:
                return mediumPotion;
            case BottleType.Large:
                return largePotion;
            default:
                throw new ArgumentException("Bottle Type was not defined");
        }
    }
}
