using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : Interactable
{
    public float life = 50;    

    public override void RunInteraction(GameObject gameObject)
    {
        float speed = gameObject.GetComponentInParent<Rigidbody>().velocity.magnitude;
        life = life - speed;
        if (life <= 0)
        {
            _parent.gameObject.SetActive(false);
            _parent.GetComponentInParent<InteractionManager>()._interaction.RunNowCompensation();
            life = 50;
        }
    }
}
