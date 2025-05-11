using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource ShootingChannel;

    public AudioClip M1911Shot;
    public AudioClip AK74Shot;

    public AudioSource reloadingSoundAK74;
    public AudioSource reloadingSoundM1911;

    public AudioSource emptyMagazineSoundM1911;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.M1911:
                ShootingChannel.PlayOneShot(M1911Shot);
                break;
            case WeaponModel.AK74:
                ShootingChannel.PlayOneShot(AK74Shot);
                //play AK74 sound
                break;

        }
    }


    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        { 
            case WeaponModel.M1911:
                reloadingSoundM1911.Play();
                break;
            case WeaponModel.AK74:
                reloadingSoundAK74.Play();
                break;

        }
    }
}
