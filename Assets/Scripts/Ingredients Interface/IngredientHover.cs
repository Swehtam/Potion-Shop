using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private Ingredient _ingredient;

	[SerializeField]
	private Transform _statusOffset;

	private void Awake()
	{
		_ingredient = GetComponent<Ingredient>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		IngredientsHoverManager.Instance.ShowStatus(_ingredient, _statusOffset.position);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		IngredientsHoverManager.Instance.HideStatus();
	}
}
