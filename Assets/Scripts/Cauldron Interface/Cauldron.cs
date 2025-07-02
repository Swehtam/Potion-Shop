using UnityEngine;
using UnityEngine.UI;

public class Cauldron : MonoBehaviour
{
    private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		CauldronCanvasEvents.OnStartMixing += StartMixing;
		CauldronCanvasEvents.OnStopMixing += StopMixing;
	}

	private void OnDisable()
	{
		CauldronCanvasEvents.OnStartMixing -= StartMixing;
		CauldronCanvasEvents.OnStopMixing -= StopMixing;
	}

	private void StartMixing()
    {
		_animator.SetBool("IsMixing", true);
	}

	private void StopMixing()
	{
		_animator.SetBool("IsMixing", false);
	}
}
