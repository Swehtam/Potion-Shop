using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverlayCanvas : MonoBehaviour
{
    public static OverlayCanvas Instance;

    [SerializeField]
    private TMP_Text _goldText;

	[SerializeField]
	private Animator _shopButtonAnimator;
	[SerializeField]
	private Animator _ingredientsButtonAnimator;
	[SerializeField]
	private Animator _cauldronButtonAnimator;

	private Canvas _canvas;

	[SerializeField]
	private GameObject _gameLost;

	private int _currentStrikes = 0;
	[SerializeField]
	private Image _firstStrike;
	[SerializeField]
	private Image _secondStrike;
	[SerializeField]
	private Image _thirdStrike;
	[SerializeField]
	private Sprite _strikeSprite;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
			_canvas = GetComponent<Canvas>();
			_gameLost.SetActive(false);
		}
	}

	private void Start()
	{
		Instance.ShopSelected();
	}

	public void UpdateGold(string value)
    {
        _goldText.text = value;
    }

	public void ShopSelected()
	{
		TransitionCanvasScenes.Instance.StartTransitionTo("shop");

		_shopButtonAnimator.SetBool("IsSelected", true);
		_ingredientsButtonAnimator.SetBool("IsSelected", false);
		_cauldronButtonAnimator.SetBool("IsSelected", false);
	}

	public void IngredientsSelected()
	{
		TransitionCanvasScenes.Instance.StartTransitionTo("ingredients");

		_shopButtonAnimator.SetBool("IsSelected", false);
		_ingredientsButtonAnimator.SetBool("IsSelected", true);
		_cauldronButtonAnimator.SetBool("IsSelected", false);
	}

	public void CauldronSelected()
	{
		TransitionCanvasScenes.Instance.StartTransitionTo("cauldron");

		_shopButtonAnimator.SetBool("IsSelected", false);
		_ingredientsButtonAnimator.SetBool("IsSelected", false);
		_cauldronButtonAnimator.SetBool("IsSelected", true);
	}

	public void RecipeBookOpened()
	{
		_canvas.sortingOrder = 0;
	}

	public void RecipeBookClosed()
	{
		_canvas.sortingOrder = 2;
	}

	public void Strike()
	{
		if (_currentStrikes == 3)
			return;

		switch (_currentStrikes)
		{
			case 0:
				_firstStrike.sprite = _strikeSprite;
				break;
			case 1:
				_secondStrike.sprite = _strikeSprite;
				break;
			case 2:
				_thirdStrike.sprite = _strikeSprite;
				GameLost();
				break;
		}

		_currentStrikes += 1;
	}

	public void GameLost()
	{
		Time.timeScale = 0f;
		_gameLost.SetActive(true);
	}

	public void RestartGame()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
