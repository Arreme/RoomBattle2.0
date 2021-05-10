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
    private float _currentCDTime;

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
        _currentCDTime = _CDTime;
    }

    public Interactable getInteraction()
    {
        if (_currentCDTime <= 0)
        {
            _currentCDTime = _CDTime;
            return _interaction;
        } else
        {
            return _emptyInteraction;
        }
    }

    private void Update()
    {
        _currentCDTime -= Time.deltaTime;
    }
}
