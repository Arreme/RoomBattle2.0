using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : Interactable
{
    private float _throw = 0;

    public override IEnumerator RunCompensation()
    {
        yield return new WaitForSeconds(0);
    }

    public override void RunInteraction(GameObject gameObject)
    {
        float speed = gameObject.GetComponentInParent<Rigidbody>().velocity.magnitude;
        Debug.Log(speed);
        if (speed >= 7)
        {
            _throw += 0.5f;
        }
        else
        {
            _throw += 0.3f;
        }
        _animator.SetFloat("Destroy", _throw);
        if (_throw >= 1f)
        {
            _animator.Play("TelevisionFall", 0);
        }
        else
        {
            _animator.Play("DestroyAction", 0);
            _animator.Play("DestroyAction", 0, 0);
        }

    }

    public override void RunNowCompensation()
    {
        _throw = 0;
        _animator.SetFloat("Destroy", 0);
        _animator.Play("TelevisionRecovery", 0);
        _animator.Play("TelevisionRecovery", 0,0);
        _animator.Play("TelevisionFall", 0, 0);
    }
    
}
