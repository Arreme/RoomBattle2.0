using System.Collections;
using UnityEngine;

public class Calaix : Interactable
{
    private bool opened = false;
    override public void RunInteraction(GameObject gameObject)
    {
        if (!opened)
        {
            _animation.Play("CalaixOpen");
            opened = true;
        } else
        {
            _animation.Play("CalaixClose");
            opened = false;
        }
    }
}
