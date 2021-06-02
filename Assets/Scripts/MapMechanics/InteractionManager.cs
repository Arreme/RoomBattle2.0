using System;
using System.Collections;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private Interactable _interaction;
    [SerializeField] private float _CDTime;
    private bool ready;
    private void Start()
    {
        _interaction = (Interactable)Activator.CreateInstance(Type.GetType(gameObject.name));
        _interaction.hasAnimation(GetComponent<Animator>(),GetComponent<Animation>());
        ready = true;
    }

    public Interactable getInteraction()
    {
        if (ready)
        {
            StartCoroutine(coolDown());
            return _interaction;
        } else
        {
            return null;
        }
    }

    private IEnumerator coolDown()
    {
        ready = false;
        StartCoroutine(_interaction.RunCompensation());
        yield return new WaitForSeconds(_CDTime);
        ready = true;
    }
}
