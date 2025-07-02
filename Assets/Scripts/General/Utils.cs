using System;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Utils Instance;

	[SerializeField]
	private Sprite _emptyBottle;
	public Sprite EmptyBottle => _emptyBottle;

	[SerializeField]
	private Sprite _failedSmallBottle;
	[SerializeField]
	private Sprite _failedMediumBottle;
	[SerializeField]
	private Sprite _failedLargeBottle;

	[SerializeField]
	private float _smallPotionMultiplier = 1f;
	public float SmallPotionMultiplier => _smallPotionMultiplier;
	[SerializeField]
	private float _mediumPotionMultiplier = 1.25f;
	public float MediumPotionMultiplier => _mediumPotionMultiplier;
	[SerializeField]
	private float _largePotionMultiplier = 1.5f;
	public float LargePotionMultiplier => _largePotionMultiplier;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
	}

	public Sprite GetFailledBottleSize(BottleType bottleType)
	{
		return bottleType switch
		{
			BottleType.Small => _failedSmallBottle,
			BottleType.Medium => _failedMediumBottle,
			BottleType.Large => _failedLargeBottle,
			_ => throw new ArgumentException("Failed Bottle Type was not defined."),
		};
	}
}