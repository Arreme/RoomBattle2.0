using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowLibros : Interactable
{
    public override void RunInteraction(GameObject gameObject)
    {
        switch(Random.Range(1,4))
        {
            case 1:
                _animation.Play("BookAnimation1");
                break;
            case 2:
                _animation.Play("BookAnimation2");
                break;
            case 3:
                _animation.Play("BookAnimation3");
                break;
        }
        
    }
}
