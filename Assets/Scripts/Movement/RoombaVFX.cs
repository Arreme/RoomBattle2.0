using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class RoombaVFX : MonoBehaviour
{
    public VisualEffect _stunned;
    public VisualEffect _run;
    public Light _powerUpLight;
    public VisualEffect _boost;

    public IEnumerator activateStunned(float time)
    {
        _stunned.SendEvent("OnStunned");
        yield return new WaitForSeconds(time);
        _stunned.SendEvent("OnStopStunned");
    }

    public IEnumerator checkForRunVFX(Rigidbody _rb)
    {
        bool playing = false;
        for (; ; )
        {
            if (!playing && _rb.velocity.magnitude >= 2)
            {
                _run.Play();
                playing = true;
            } else if (playing && _rb.velocity.magnitude < 2)
            {
                _run.Stop();
                playing = false;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void actiavteLights(bool activate)
    {
        _powerUpLight.enabled = activate;
    }
}
