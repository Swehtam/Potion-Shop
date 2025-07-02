using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof(IngredientHover))]
public class Ingredient : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private IngredientsData _ingredientData;
    public IngredientsData IngredientData => _ingredientData;

    [SerializeField]
    private Image _image;
	[SerializeField]
	private TMP_Text _goldValue;

	private void Awake()
	{
        _image.sprite = _ingredientData.Image;
		_goldValue.text = _ingredientData.GoldValue.ToString();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		IngredientsCanvasEvents.IngredientAdded(_ingredientData);
	}
}
