using UnityEngine;

[CreateAssetMenu(fileName = "IngredientsData", menuName = "Scriptable Objects/IngredientsData")]
public class IngredientsData : ScriptableObject
{
    public Sprite Image;
    [Range(0, 200)]
    public int GoldValue;
    [Range(-2, 2)]
    public int STRValue;
	[Range(-2, 2)]
	public int INTValue;
	[Range(-2, 2)]
	public int AGIValue;
}
