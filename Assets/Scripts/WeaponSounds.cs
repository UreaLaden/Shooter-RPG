using UnityEngine;

[RequireComponent(typeof(Weapons))]
[RequireComponent(typeof(AudioSource))]
public class WeaponSounds : WeaponComponent
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    protected override void WeaponFired()
    {
        if (Input.GetMouseButton(1))
        {
            audioSource.Play();
        }
    }

}
