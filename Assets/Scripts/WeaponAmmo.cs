using System;
using System.Collections;
using UnityEngine;

public class WeaponAmmo: WeaponComponent
{
    [SerializeField] private int maxAmmo = 24;
    [SerializeField] private int maxAmmoPerClip = 6;
    [SerializeField] private float reloadSpeed = .2f; 
    private int ammoInClip;
    private int ammoRemainingNotInClip;
    
    public event Action OnAmmoChanged = delegate { };
    protected override void Awake()
    {
        ammoInClip = maxAmmoPerClip;
        ammoRemainingNotInClip = maxAmmo - ammoInClip;
        
        base.Awake();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    public bool isAmmoReady()
    {
        return ammoInClip > 0;
    }
    protected override void WeaponFired()
    {
        RemoveAmmo();
    }

    private void RemoveAmmo()
    {
        ammoInClip--;
        ammoRemainingNotInClip++;
        OnAmmoChanged();
    }

    private IEnumerator Reload()
    {
        int ammoMissingFromClip = maxAmmo - ammoInClip;
        int ammoToMove = Math.Min(ammoMissingFromClip, ammoRemainingNotInClip);

        while (ammoToMove > 0)
        {
            yield return new WaitForSeconds(reloadSpeed);
            
            ammoInClip++;
            ammoRemainingNotInClip--;
            OnAmmoChanged();
            ammoToMove--;
        }
    }
    public string GetAmmoText()
    {
        return string.Format("{0}/{1}", ammoInClip, ammoRemainingNotInClip);
    }
}