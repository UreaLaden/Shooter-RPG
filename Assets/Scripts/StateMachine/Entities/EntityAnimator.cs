using UnityEngine;

[RequireComponent(typeof(Entity))]
public class EntityAnimator : MonoBehaviour
{
    private Animator _animator;
    private Entity _entity;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _entity = GetComponent<Entity>();
        _entity.OnDied +=  () =>_animator.SetTrigger(Utilities.AnimationTriggers.Die.ToString());
    }
    
}
