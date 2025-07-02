using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedIngredientElement : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private TMP_Text _text;

    private IngredientsData _data;

    public void InitElement(IngredientsData ingredient)
    {
        _data = ingredient;
        _image.sprite = ingredient.Image;
        _text.text = "1";
    }

	public void UpdateElement(int quantity)
	{
		_text.text = $"{quantity}";
	}

    public void RemoveIngredient()
    {
        IngredientsCanvasEvents.IngredientRemoved(_data);
    }
}
