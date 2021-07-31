using UnityEngine;

public abstract class WeaponComponent : MonoBehaviour
{
    private Weapon _weapon;
    protected abstract void WeaponFired();
    protected virtual void Awake()
    {
        _weapon = GetComponent<Weapon>();
        _weapon.OnFire += WeaponFired;
    }
 
    private void OnDestroy()
    {
        _weapon.OnFire -= WeaponFired;
    }
}