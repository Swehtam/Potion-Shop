using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBook : MonoBehaviour
{
    [SerializeField]
    private List<RecipeBookItem> _items = new();

	private void Start()
	{
		InitRecipes();
	}

	private void InitRecipes()
    {
        if (_items.Count != CauldronManager.Instance.Recipes.Count)
            throw new Exception("Number of slots in recipe book is not the same as the available recipes.");

        for(int i = 0; i < _items.Count; i++)
        {
            _items[i].AddRecipe(CauldronManager.Instance.Recipes[i]);
        }
    }
}
