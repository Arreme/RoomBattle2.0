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

    public virtual IEnumerator RunCompensation(float _cdtime)
    {
        _parent.ready = false;
        yield return new WaitForSeconds(_cdtime);
        _parent.ready = true;
    }

    public virtual void RunNowCompensation()
    {

    }
}
