using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalaixInteraction : Interactable
{
    private Animator _anim;
    private bool open;
    public CalaixInteraction(Animator anim)
    {
        _anim = anim;
        open = true;
    }
    public void runInteraction(GameObject gameObject)
    {
        if (open)
        {
            _anim.Play("MesitaAnimation",0);
            _anim.Play("MesitaAnimation", 0,0);
            open = false;
        } else
        {
            _anim.Play("CloseMesita",0);
            _anim.Play("CloseMesita", 0,0);
            open = true;
        }
        
    }
}
