using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SofaThrow : Interactable
{
    public override void RunInteraction(GameObject gameObject)
    {
        _animation.Play("SofaThrow");
    }

    public override IEnumerator RunCompensation()
    {
        yield return new WaitForSeconds(20f);
        _animation.Play("SofaRecovery");
    }
}
