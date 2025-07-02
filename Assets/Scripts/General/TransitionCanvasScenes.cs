using NUnit.Framework;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class TransitionCanvasScenes : MonoBehaviour
{
	public static TransitionCanvasScenes Instance;

    [SerializeField]
    private CanvasGroup _ingredientsCanvas;

	[SerializeField]
	private CanvasGroup _cauldronCanvas;

	[SerializeField]
	private CanvasGroup _shopCanvas;

	private string _canvasName;

	private Animator _animator;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
			_animator = GetComponent<Animator>();
		}
	}

	public void StartTransitionTo(string canvasName)
	{
		var names = new List<string> { "ingredients", "cauldron", "shop" };
		bool contains = names.Contains(canvasName, StringComparer.OrdinalIgnoreCase);
		if (contains)
		{
			_canvasName = canvasName;
		}
		else
		{
			throw new ArgumentException($"Incorrect name for canvas to transition: {canvasName}");
		}

		_animator.SetTrigger("Transition");
	}

	public void ShowNewCanvas()
	{
		HideAllCanvas();

		if (string.Equals(_canvasName, "ingredients", System.StringComparison.OrdinalIgnoreCase))
		{
			_ingredientsCanvas.alpha = 1;
			_ingredientsCanvas.interactable = true;
			_ingredientsCanvas.blocksRaycasts = true;
		}
		else if(string.Equals(_canvasName, "cauldron", System.StringComparison.OrdinalIgnoreCase))
		{
			_cauldronCanvas.alpha = 1;
			_cauldronCanvas.interactable = true;
			_cauldronCanvas.blocksRaycasts = true;
		}
		else if(string.Equals(_canvasName, "shop", System.StringComparison.OrdinalIgnoreCase))
		{
			_shopCanvas.alpha = 1;
			_shopCanvas.interactable = true;
			_shopCanvas.blocksRaycasts = true;
		}
	}

	private void HideAllCanvas()
	{
		_ingredientsCanvas.alpha = 0;
		_ingredientsCanvas.interactable = false;
		_ingredientsCanvas.blocksRaycasts = false;

		_cauldronCanvas.alpha = 0;
		_cauldronCanvas.interactable = false;
		_cauldronCanvas.blocksRaycasts = false;

		_shopCanvas.alpha = 0;
		_shopCanvas.interactable = false;
		_shopCanvas.blocksRaycasts = false;
	}
}