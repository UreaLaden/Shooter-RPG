using System;
using TMPro;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 5;
    public Transform currentFocus;
    public Animator _animator;
    public Action OnDied;
    public float lookWeight;
    public Vector3 _lookAtPosition;
    public Vector3 _target;
    public float lookSpeed = 3.0f;
    public float threatRange = 5.0f;
    public float distanceToTaget = Mathf.Infinity;
    public float targetOffset = 1.5f;
    public Transform _resetPosition;
    public float _maxAngle = 75f;
    public int Health { get; private set; }

    private void OnEnable()
    {
        Health = _maxHealth;
        _animator = GetComponent<Animator>();
    }

    public void TakeHit(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Die();
        }
        else
        {
            HandleNonLethalHit();
        }
    }

    private void HandleNonLethalHit()
    {
        Debug.Log("Took non-lethal damage");
    }


    private void Die()
    {
        OnDied?.Invoke();
        Debug.Log("Died");
    }

    [ContextMenu("Take Lethal Damage")]
    private void TakeLethalDamage()
    {
        TakeHit(Health);
    }

    public void FaceTarget()
    {
        Vector3 direction = (_target - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, threatRange);
    }
}