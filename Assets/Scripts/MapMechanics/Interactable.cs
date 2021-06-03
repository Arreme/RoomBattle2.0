using UnityEngine;
using System.Collections;

public abstract class Interactable
{
    protected InteractionManager _parent;

    protected Animation _animation;
    protected Animator _animator;
    public void needParent(InteractionManager parent)
    {
        _parent = parent;
    }

    public void hasAnimation(Animator animator, Animation animation)
    {
        _animation = animation;
        _animator = animator;
    }
    public abstract void RunInteraction(GameObject gameObject);

    public abstract IEnumerator RunCompensation();

    public virtual void RunNowCompensation()
    {

    }
}
