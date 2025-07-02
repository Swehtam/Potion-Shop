using System;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private RecipesData _recipe;
    public RecipesData Recipe => _recipe;
    private BottleType _bottleType;
    public BottleType BottleType => _bottleType;

    public Sprite GetPotionSprite()
    {
        return _recipe.GetBottleSize(_bottleType);
	}

    public int GetSellingValue()
    {
        int goldValue = _recipe.GoldValue;

		switch (_bottleType)
		{
			case BottleType.None:
				throw new Exception("Selling Potion cannot be BottleType.None");
			case BottleType.Small:
				goldValue = Mathf.CeilToInt(goldValue * Utils.Instance.SmallPotionMultiplier);
				break;
			case BottleType.Medium:
				goldValue = Mathf.CeilToInt(goldValue * Utils.Instance.MediumPotionMultiplier);
				break;
			case BottleType.Large:
				goldValue = Mathf.CeilToInt(goldValue * Utils.Instance.LargePotionMultiplier);
				break;
		}

        return goldValue;
	}

    public void SetPotion(RecipesData recipe, BottleType bottle)
    {
        _recipe = recipe;
        _bottleType = bottle;
    }

    public void ClearData()
    {
        _recipe = null;
        _bottleType = BottleType.None;
    }

    public bool ComparePotions(Potion potion)
    {
        if (_recipe != potion.Recipe)
            return false;

        if(_bottleType != potion.BottleType) return false;

        return true;
    }
}
