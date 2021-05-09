using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Interaction _interaction;
    private enum Interactions
    {
        Lamp,
        Other
    }

    [SerializeField] private Interactions _typeOfInteraction;

    private void Start()
    {
        switch (_typeOfInteraction)
        {
            case Interactions.Lamp:
                _interaction = new HitLamp(gameObject.GetComponent<Animator>());
                break;
            default:
                break;
        }
    }

    public Interaction getInteraction()
    {
        return _interaction;
    }
}
