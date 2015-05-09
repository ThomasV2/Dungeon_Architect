using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class KnightAnimationBehavior : MonoBehaviour
{
    private Animator _animator;

    public float bpm
    {
        set
        {
            _animator.speed = value / 60f;
        }
    }

	void Start()
    {
        _animator = GetComponent<Animator>();


        bpm = 120f;
	}
}
