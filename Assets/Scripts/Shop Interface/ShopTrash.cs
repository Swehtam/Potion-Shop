using UnityEngine;
using UnityEngine.EventSystems;

public class ShopTrash : MonoBehaviour, IDropHandler
{
	public void OnDrop(PointerEventData eventData)
	{
		GameObject droppedPotion = eventData.pointerDrag;
		BalconyPotion balPotion = droppedPotion.GetComponent<BalconyPotion>();
		balPotion.ClearPotion();
	}
}
