using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Potion))]
public class CauldronPotion : MonoBehaviour
{
    [SerializeField]
    private Image _image;
	[SerializeField]
	private TMP_Text _text;
    [SerializeField]
    private GameObject _textBackground;
    
    private Potion _potion;
    public Potion Potion => _potion;

	private void Awake()
	{
        _potion = GetComponent<Potion>();
        _text.text = "";
		_textBackground.SetActive(false);
	}

	private void Start()
	{
        _image.sprite = Utils.Instance.EmptyBottle;
	}

	public void SetRecipe(RecipesData recipe, BottleType bottleType)
    {
        _potion.SetPotion(recipe, bottleType);
        _image.sprite = recipe.GetBottleSize(bottleType);
        _text.text = recipe.RecipeName;
		_textBackground.SetActive(true);
	}

    public void FailedMixing(Sprite bottleSprite)
    {
        _image.sprite = bottleSprite;
        _text.text = "Failed Mixing";
    }

    public void ClearPotion()
    {
        _potion.ClearData();
		_image.sprite = Utils.Instance.EmptyBottle;
        _textBackground.SetActive(false);
		_text.text = "";
	}
}
