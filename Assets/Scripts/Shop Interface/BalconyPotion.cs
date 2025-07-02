using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof(Potion))]
public class BalconyPotion : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private BalconyManager _manager;
    private Potion _potion;
	[SerializeField]
	private Image _image;

	private Vector2 _initialPosition;

	private bool _available = true;

	private void Awake()
	{
		_potion = GetComponent<Potion>();
		_initialPosition = transform.position;
	}

	private void Start()
	{
		_manager = GetComponentInParent<BalconyManager>();
		ClearPotion();
	}

	public bool IsAvailable()
	{
		return _available;
	}

	public void SetPotion(Potion potion)
	{
		_potion.SetPotion(potion.Recipe, potion.BottleType);
		_image.sprite = potion.Recipe.GetBottleSize(potion.BottleType);
		_available = false;
	}

	public void ClearPotion()
	{
		_available = true;
		_image.sprite = Utils.Instance.EmptyBottle;
		_potion.ClearData();
		_manager.PotionRemoved();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		_image.raycastTarget = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		_image.raycastTarget = true;
		transform.position = _initialPosition;
	}
}
