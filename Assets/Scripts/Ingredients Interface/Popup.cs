using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Popup : MonoBehaviour
{
    private Animator _animator;

	private bool _isVisible = false;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public void PopUpSelected()
	{
		_isVisible = !_isVisible;

		_animator.SetBool("Show", _isVisible);
	}
}
