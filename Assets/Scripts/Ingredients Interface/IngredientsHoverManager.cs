using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IngredientsHoverManager : MonoBehaviour
{
	public static IngredientsHoverManager Instance { get; private set; }

	[SerializeField]
	private TMP_Text _firstStatusValue;
	[SerializeField]
	private TMP_Text _firstStatusName;
	[SerializeField]
	private Image _firstStatusImage;

	[SerializeField]
	private TMP_Text _secondStatusValue;
	[SerializeField]
	private TMP_Text _secondStatusName;
	[SerializeField]
	private Image _secondStatusImage;

	[SerializeField] private Sprite _strSprite;
	[SerializeField] private Sprite _intSprite;
	[SerializeField] private Sprite _agiSprite;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
			Instance.gameObject.SetActive(false);
		}
	}

	public void ShowStatus(Ingredient ingredient, Vector2 position)
	{
		gameObject.SetActive(true);
		transform.position = position;
		
		if(ingredient.IngredientData.STRValue != 0)
		{
			int value = ingredient.IngredientData.STRValue;

			_firstStatusImage.sprite = _strSprite;
			_firstStatusName.text = "STR";
			string positive = value > 0 ? "+" : "";
			_firstStatusValue.text = $"{positive}{value}";

			if(ingredient.IngredientData.INTValue != 0)
			{
				value = ingredient.IngredientData.INTValue;

				_secondStatusImage.sprite = _intSprite;
				_secondStatusName.text = "INT";
				positive = value > 0 ? "+" : "";
				_secondStatusValue.text = $"{positive}{value}";
			}
			else
			{
				value = ingredient.IngredientData.AGIValue;

				_secondStatusImage.sprite = _agiSprite;
				_secondStatusName.text = "AGI";
				positive = value > 0 ? "+" : "";
				_secondStatusValue.text = $"{positive}{value}";
			}
		}
		else
		{
			int value = ingredient.IngredientData.INTValue;

			_firstStatusImage.sprite = _intSprite;
			_firstStatusName.text = "INT";
			string positive = value > 0 ? "+" : "";
			_firstStatusValue.text = $"{positive}{value}";

			value = ingredient.IngredientData.AGIValue;

			_secondStatusImage.sprite = _agiSprite;
			_secondStatusName.text = "AGI";
			positive = value > 0 ? "+" : "";
			_secondStatusValue.text = $"{positive}{value}";
		}
	}

	public void HideStatus()
	{
		gameObject.SetActive(false);
	}
}

