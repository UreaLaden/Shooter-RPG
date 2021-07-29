using UnityEngine;

[RequireComponent(typeof(Weapons))]
[RequireComponent(typeof(AudioSource))]
public class WeaponSounds : MonoBehaviour
{
    private Weapons weapon;
    private AudioSource audioSource;

    private void Awake()
    {
        weapon = GetComponent<Weapons>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        weapon.OnFire += HandleWeaponFire;
    }

    private void HandleWeaponFire()
    {
        if (Input.GetMouseButton(1))
        {
            audioSource.Play();
        }
    }

    private void OnDestroy()
    {
        weapon.OnFire -= HandleWeaponFire;
    }
}
