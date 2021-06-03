using System;
using System.Collections;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public Interactable _interaction;
    [SerializeField] public float _CDTime;
    public bool ready;
    private void Start()
    {
        _interaction = (Interactable)Activator.CreateInstance(Type.GetType(gameObject.name));
        _interaction.hasAnimation(GetComponent<Animator>(),GetComponent<Animation>());
        _interaction.needParent(this);
        ready = true;
    }

    public Interactable getInteraction()
    {
        if (ready)
        {
            StartCoroutine(_interaction.RunCompensation(_CDTime));
            return _interaction;
        } else
        {
            return null;
        }
    }
}
