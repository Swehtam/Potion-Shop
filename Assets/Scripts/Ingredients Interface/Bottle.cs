using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField]
    private BottleType _type;
    public BottleType Type => _type;

	[SerializeField]
	private Sprite _sprite;
    public Sprite Sprite => _sprite;

	public void BottleSelected()
    {
        IngredientsCanvasEvents.BottleSelected(this);
    }
}

public enum BottleType { None, Small, Medium, Large };
