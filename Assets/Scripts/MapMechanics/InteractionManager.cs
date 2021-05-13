using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private Interactable _interaction;
    private enum Interactions
    {
        Lamp,
        Hazard,
        Other
    }

    [SerializeField] private Interactions _typeOfInteraction;
    [SerializeField] private float _CDTime;
    [SerializeField] private Interactable _emptyInteraction;
    private bool ready;
    private void Start()
    {
        switch (_typeOfInteraction)
        {
            case Interactions.Lamp:
                _interaction = new LampInteraction(gameObject.GetComponent<Animator>());
                break;
            case Interactions.Hazard:
                _interaction = new HazardInteraction(gameObject);
                break;
            default:
                break;
        }
        _emptyInteraction = new EmptyInteraction();
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
            return _emptyInteraction;
        }
    }

    private IEnumerator coolDown()
    {
        ready = false;
        yield return new WaitForSeconds(_CDTime);
        ready = true;
    }
}
