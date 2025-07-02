using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Potion))]
public class Client : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private Image _image;
	[SerializeField]
	private Image _potionImage;
	[SerializeField]
	private TMP_Text _potionName;

	[SerializeField]
	private ShopOrder _shopOrder;
	private Potion _wantPotion;
    private ClientsManager _clientsManager;

	private CanvasGroup _canvasGroup;

	[SerializeField]
	private Slider _timeSlider;
	private bool _isWaiting = false;
	private float _waitingTime = 60f;
	private float _currentTime = 0f;

	private void Awake()
	{
		_wantPotion = GetComponent<Potion>();
		_canvasGroup = GetComponent<CanvasGroup>();
	}

	private void Start()
	{
		_clientsManager = GetComponentInParent<ClientsManager>();
		ClearData();
	}

	private void Update()
	{
		if (!_isWaiting)
			return;

		_currentTime += Time.deltaTime;

		if (_currentTime >= _waitingTime)
		{
			_isWaiting = false;
			_clientsManager.ClientLeft(this, true);
			_currentTime = _waitingTime;
			_timeSlider.value = _waitingTime;
			ClearData();
		}

		_timeSlider.value = _currentTime;
		_shopOrder.UpdateTimer(_currentTime);
	}

	public void SetRandomOrder()
    {
		_wantPotion.SetPotion(ShopManager.Instance.GetRandomRecipe(), ShopManager.Instance.GetRandomBottleType());
		_potionImage.sprite = _wantPotion.GetPotionSprite();
		_potionName.text = _wantPotion.Recipe.name;

		_shopOrder.SetOrder(_wantPotion);

		_canvasGroup.alpha = 1;
		_canvasGroup.interactable = true;
		_canvasGroup.blocksRaycasts = true;

		_currentTime = 0f;
		_timeSlider.maxValue = _waitingTime;
		_isWaiting = true;
	}

	public void OnDrop(PointerEventData eventData)
	{
		GameObject droppedPotion = eventData.pointerDrag;
		Potion potion = droppedPotion.GetComponent<Potion>();

		if (_wantPotion.ComparePotions(potion))
		{
			_isWaiting = false;

			BalconyPotion balPotion = droppedPotion.GetComponent<BalconyPotion>();
			ShopManager.Instance.SellPotion(potion.GetSellingValue());
			_clientsManager.ClientLeft(this);
			balPotion.ClearPotion();
			ClearData();
		}
	}

	private void ClearData()
	{
		_wantPotion.ClearData();
		_canvasGroup.alpha = 0;
		_canvasGroup.interactable = false;
		_canvasGroup.blocksRaycasts = false;

		_shopOrder.ClearOrder();
	}
}
