using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowLibros : Interactable
{
    public override void RunInteraction(GameObject gameObject)
    {
        _animation.Play("BookAnimation1");
    }
}
