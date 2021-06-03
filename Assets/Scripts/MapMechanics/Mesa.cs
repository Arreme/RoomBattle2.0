using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesa : Interactable
{
    public override void RunInteraction(GameObject gameObject)
    {
        _animation.Play("MesaHit");
    }
}
