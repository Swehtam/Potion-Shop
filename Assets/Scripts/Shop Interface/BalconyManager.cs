using System.Collections.Generic;
using UnityEngine;

public class BalconyManager : MonoBehaviour
{
    [SerializeField]
    private List<BalconyPotion> _potions = new();
	private int _maxPotions;
	private int _currentPotions = 0;

	private void Start()
	{
		_maxPotions = _potions.Count;
	}

	public bool IsBalconyFull()
	{
		return _currentPotions == _maxPotions;
	}

	public void AddPotion(Potion potion)
    {
		foreach (BalconyPotion p in _potions)
		{
			if (p.IsAvailable())
			{
				p.SetPotion(potion);
				_currentPotions++;
				return;
			}
		}
    }

	public void PotionRemoved()
	{
		_currentPotions--;
	}
}
