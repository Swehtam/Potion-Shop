using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopOrder : MonoBehaviour
{
    [SerializeField]
    private Image _potionImage;
    [SerializeField]
    private TMP_Text _potionName;
    [SerializeField]
    private TMP_Text _potionValue;

	[SerializeField]
	private Slider _timeSlider;

	private CanvasGroup _canvasGroup;

	private void Awake()
	{
		_canvasGroup = GetComponent<CanvasGroup>();
	}

	public void SetOrder(Potion potion)
    {
        _potionImage.sprite = potion.GetPotionSprite();
        _potionName.text = potion.Recipe.name;
        _potionValue.text = potion.GetSellingValue().ToString();

		_canvasGroup.alpha = 1;
		_canvasGroup.interactable = true;
		_canvasGroup.blocksRaycasts = true;

		_timeSlider.maxValue = 60f;
	}

    public void ClearOrder()
    {
		_canvasGroup.alpha = 0;
		_canvasGroup.interactable = false;
		_canvasGroup.blocksRaycasts = false;
	}

	public void UpdateTimer(float currentTime)
	{
		_timeSlider.value = currentTime;
	}
}
