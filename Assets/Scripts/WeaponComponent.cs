using UnityEngine;

public abstract class WeaponComponent : MonoBehaviour
{
    private Weapons weapon;
    protected abstract void WeaponFired();
    private void Awake()
    {
        weapon = GetComponent<Weapons>();
        weapon.OnFire += WeaponFired;
    }
 
    private void OnDestroy()
    {
        weapon.OnFire -= WeaponFired;
    }
}