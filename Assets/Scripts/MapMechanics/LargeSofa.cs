using System.Collections;
using UnityEngine;

public class LargeSofa : Interactable
{
    override public void RunInteraction(GameObject gameObject)
    {
        _animation.Play("SofaHit");
    }
}
