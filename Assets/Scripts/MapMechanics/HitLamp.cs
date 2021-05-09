using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitLamp : Interaction
{
    Animator _animator;
    public void runInteraction(GameObject gameObject)
    {
        Debug.Log("Hey!");
        _animator.Play("LampThrowUp", 0);
    }

    public HitLamp(Animator anim)
    {
        _animator = anim;
    }
}
