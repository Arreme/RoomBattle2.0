using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampInteraction : Interactable
{
    Animator _animator;
    private float _throw = 0;
    public void runInteraction(GameObject gameObject)
    {
        
        if (gameObject.GetComponentInParent<Rigidbody>().velocity.magnitude >= 7)
        {
            _throw += 0.5f;
        } else
        {
            _throw += 0.3f;
        }
        _animator.SetFloat("Throw", _throw);
        Debug.Log(_throw);
        if (_throw >= 1f)
        {
            _animator.Play("LampThrowUp", 0);
        } else
        {
            _animator.Play("Destroying", 0);
            _animator.Play("Destroying", 0, 0);
        }
        
    }

    public LampInteraction(Animator anim)
    {
        _animator = anim;
    }
}
