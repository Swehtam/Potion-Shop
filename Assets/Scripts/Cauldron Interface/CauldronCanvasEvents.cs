using System.Collections.Generic;
using UnityEngine;
using static IngredientsManager;

public class CauldronCanvasEvents : MonoBehaviour
{
	public delegate void AddPotionHandler(Dictionary<IngredientsData, int> ingredients, Bottle bottle);
	public static event AddPotionHandler OnAddPotion;

	public delegate void StartMixingHandler();
	public static event StartMixingHandler OnStartMixing;

	public delegate void StopMixingHandler();
	public static event StopMixingHandler OnStopMixing;

	public delegate void MixingCompleteHandler(int gold);
	public static event MixingCompleteHandler OnMixingComplete;

	public static void AddPotion(Dictionary<IngredientsData, int> ingredients, Bottle bottle)
	{
		OnAddPotion?.Invoke(ingredients, bottle);
	}

	public static void StartMixing()
	{
		OnStartMixing?.Invoke();
	}

	public static void StopMixing()
	{
		OnStopMixing?.Invoke();
	}

	public static void MixingComplete(int gold)
	{
		OnMixingComplete?.Invoke(gold);
	}
}
